Shader "EscapeTheOffice/CollectibleOutlineComposite"
{
    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" }
        Pass
        {
            Cull Off ZWrite Off ZTest Always
            Blend SrcAlpha OneMinusSrcAlpha
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
            TEXTURE2D_X_FLOAT(_OutlineSceneDepth);
            int _OutlineWidth;
            float4 _OutlineColor;
            float4 Frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                int2 pixel = int2(input.positionCS.xy);
                int2 size = int2(_ScaledScreenParams.xy);
                float center = LOAD_TEXTURE2D_X(_BlitTexture, pixel).r;
                if (center > 0) return 0;
                float sceneDepth = LinearEyeDepth(LOAD_TEXTURE2D_X(_OutlineSceneDepth, pixel).r, _ZBufferParams);
                float found = 0;
                for (int y = -_OutlineWidth; y <= _OutlineWidth; y++)
                for (int x = -_OutlineWidth; x <= _OutlineWidth; x++)
                {
                    if (x*x + y*y > _OutlineWidth*_OutlineWidth) continue;
                    int2 p = pixel + int2(x,y);
                    if (any(p < 0) || any(p >= size)) continue;
                    float itemDepth = LOAD_TEXTURE2D_X(_BlitTexture, p).r;
                    // Do not dilate the visible mask across a nearer opaque surface.
                    if (itemDepth > 0 && itemDepth <= sceneDepth) found = 1;
                }
                return float4(_OutlineColor.rgb, found);
            }
            ENDHLSL
        }
    }
}
