using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

public sealed class CollectibleOutlineRendererFeature : ScriptableRendererFeature
{
    [SerializeField] private Material maskMaterial;
    [SerializeField] private Material compositeMaterial;
    [SerializeField, Range(1, 4)] private int widthPixels = 2;
    [SerializeField] private Color outlineColor = Color.cyan;
    private OutlinePass pass;

    public override void Create()
    {
        pass = new OutlinePass { renderPassEvent = RenderPassEvent.BeforeRenderingTransparents };
        pass.ConfigureInput(ScriptableRenderPassInput.Depth);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        // The two existing Game cameras are preserved. Never affect editor/preview/reflection cameras.
        if (renderingData.cameraData.cameraType != CameraType.Game ||
            maskMaterial == null || compositeMaterial == null || CollectibleOutlineTarget.Active.Count == 0)
            return;
        pass.Setup(maskMaterial, compositeMaterial, Mathf.Clamp(widthPixels, 1, 4), outlineColor);
        renderer.EnqueuePass(pass);
    }

    private sealed class OutlinePass : ScriptableRenderPass
    {
        private Material mask, composite;
        private int width;
        private Color color;
        private static readonly int DepthId = Shader.PropertyToID("_OutlineSceneDepth");
        private static readonly int WidthId = Shader.PropertyToID("_OutlineWidth");
        private static readonly int ColorId = Shader.PropertyToID("_OutlineColor");

        public void Setup(Material maskMaterial, Material compositeMaterial, int pixels, Color cyan)
        {
            mask = maskMaterial;
            composite = compositeMaterial;
            width = pixels;
            color = cyan;
        }

        private class MaskData
        {
            public Material material;
            public TextureHandle depth;
            public MeshRenderer[] renderers;
        }

        private class CompositeData
        {
            public Material material;
            public TextureHandle mask, depth;
            public int width;
            public Color color;
        }

        public override void RecordRenderGraph(RenderGraph graph, ContextContainer frameData)
        {
            var camera = frameData.Get<UniversalCameraData>();
            if (camera.cameraType != CameraType.Game) return;
            var resources = frameData.Get<UniversalResourceData>();
            if (!resources.cameraDepthTexture.IsValid()) return;

            var visible = new List<MeshRenderer>();
            var planes = GeometryUtility.CalculateFrustumPlanes(camera.camera);
            foreach (var target in CollectibleOutlineTarget.Active)
            {
                if (target == null || !target.isActiveAndEnabled) continue;
                var r = target.TargetRenderer;
                if (r == null || !r.enabled || r.forceRenderingOff || !r.gameObject.activeInHierarchy ||
                    (camera.camera.cullingMask & (1 << r.gameObject.layer)) == 0 ||
                    !GeometryUtility.TestPlanesAABB(planes, r.bounds)) continue;
                visible.Add(r);
            }
            if (visible.Count == 0) return;

            // Single-channel full-resolution eye depth: zero means no visible collectible.
            var desc = camera.cameraTargetDescriptor;
            desc.graphicsFormat = GraphicsFormat.R32_SFloat;
            desc.depthBufferBits = 0;
            desc.msaaSamples = 1;
            desc.bindMS = false;
            var maskTexture = UniversalRenderer.CreateRenderGraphTexture(graph, desc, "Visible collectible depth", true, FilterMode.Point);
            using (var builder = graph.AddRasterRenderPass<MaskData>("Collectible visible mask", out var data))
            {
                data.material = mask;
                data.depth = resources.cameraDepthTexture;
                data.renderers = visible.ToArray();
                builder.UseTexture(data.depth, AccessFlags.Read);
                builder.SetRenderAttachment(maskTexture, 0, AccessFlags.Write);
                builder.AllowGlobalStateModification(true);
                builder.SetRenderFunc((MaskData d, RasterGraphContext context) =>
                {
                    context.cmd.SetGlobalTexture(DepthId, d.depth);
                    foreach (var r in d.renderers)
                    {
                        var filter = r.GetComponent<MeshFilter>();
                        if (filter == null || filter.sharedMesh == null) continue;
                        for (int sub = 0; sub < filter.sharedMesh.subMeshCount; sub++)
                            context.cmd.DrawRenderer(r, d.material, sub, 0);
                    }
                });
            }
            using (var builder = graph.AddRasterRenderPass<CompositeData>("Collectible cyan outline", out var data))
            {
                data.material = composite;
                data.mask = maskTexture;
                data.depth = resources.cameraDepthTexture;
                data.width = width;
                data.color = color;
                builder.UseTexture(data.mask, AccessFlags.Read);
                builder.UseTexture(data.depth, AccessFlags.Read);
                // Blend into existing color without sampling it or replacing the camera target.
                builder.SetRenderAttachment(resources.activeColorTexture, 0, AccessFlags.ReadWrite);
                builder.AllowGlobalStateModification(true);
                builder.SetRenderFunc((CompositeData d, RasterGraphContext context) =>
                {
                    context.cmd.SetGlobalTexture(DepthId, d.depth);
                    context.cmd.SetGlobalInt(WidthId, d.width);
                    context.cmd.SetGlobalColor(ColorId, d.color);
                    Blitter.BlitTexture(context.cmd, d.mask, new Vector4(1, 1, 0, 0), d.material, 0);
                });
            }
        }
    }
}
