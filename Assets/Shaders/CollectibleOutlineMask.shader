Shader "EscapeTheOffice/CollectibleOutlineMask"
{
    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" }
        Pass
        {
            Cull Back ZWrite Off ZTest Always
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            TEXTURE2D_X_FLOAT(_OutlineSceneDepth);
            struct Attributes { float4 positionOS : POSITION; UNITY_VERTEX_INPUT_INSTANCE_ID };
            struct Varyings { float4 positionCS : SV_POSITION; float eyeDepth : TEXCOORD0; UNITY_VERTEX_OUTPUT_STEREO };
            Varyings Vert(Attributes input)
            {
                Varyings output;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
                float3 world = TransformObjectToWorld(input.positionOS.xyz);
                output.positionCS = TransformWorldToHClip(world);
                output.eyeDepth = -TransformWorldToView(world).z;
                return output;
            }
            float Frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                float raw = LOAD_TEXTURE2D_X(_OutlineSceneDepth, uint2(input.positionCS.xy)).r;
                float sceneDepth = LinearEyeDepth(raw, _ZBufferParams);
                // Only original mesh pixels passing opaque visibility may seed an outline.
                clip(sceneDepth + 0.0001 - input.eyeDepth);
                return input.eyeDepth;
            }
            ENDHLSL
        }
    }
}
