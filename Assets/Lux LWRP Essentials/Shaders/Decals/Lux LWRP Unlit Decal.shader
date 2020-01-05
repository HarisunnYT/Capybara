Shader "Lux LWRP/Projection/Decal Unlit"
{
    Properties
    {
        [Header(Surface Options)]
        [Space(5)]
        [Toggle(ORTHO_SUPPORT)]
        _OrthoSpport                                    ("Enable Orthographic Support", Float) = 0

        [Header(Surface Inputs)]
        [Space(5)]
        [HDR]_Color                                     ("Color", Color) = (1,1,1,1)
        [NoScaleOffset] _BaseMap                        ("Albedo (RGB) Alpha (A)", 2D) = "white" {}

        [Header(Distance Fading)]
        [Space(5)]
        [LuxLWRPDistanceFadeDrawer]
        _DistanceFade                                   ("Distance Fade Params", Vector) = (2500, 0.001, 0, 0)

        [Header(Stencil)]
        [Space(5)]
        [IntRange] _StencilRef                          ("Stencil Reference", Range (0, 255)) = 0
        [IntRange] _ReadMask                            ("    Read Mask", Range (0, 255)) = 255
        [IntRange] _WriteMask                           ("    Write Mask", Range (0, 255)) = 255
        [Enum(UnityEngine.Rendering.CompareFunction)]
        _StencilCompare                                 ("Stencil Comparison", Int) = 6

        [Header(Advanced)]
        [Space(5)]
        [Toggle(_APPLYFOG)] _ApplyFog("Enable Fog", Float) = 0.0
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline"="LightweightPipeline"
            "RenderType"="Opaque"
            "Queue"= "Transparent" // +59 smalltest to get drawn on top of transparents
        }
        Pass
        {
            Name "StandardUnlit"
            Tags{"LightMode" = "LightweightForward"}

            Stencil {
                Ref  [_StencilRef]
                ReadMask [_ReadMask]
                WriteMask [_WriteMask]
                Comp [_StencilCompare]
            }


            Blend SrcAlpha OneMinusSrcAlpha

        //  We draw backfaces to prevent clipping
            Cull Front
        //  So we have to set ZTest to always
            ZTest Always
        //  It is a decal!
            ZWrite Off

            HLSLPROGRAM
            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0

            #pragma shader_feature_local ORTHO_SUPPORT

            #pragma shader_feature_local _APPLYFOG

            // -------------------------------------
            // Lightweight Pipeline keywords

            // -------------------------------------
            // Unity defined keywords
            #pragma multi_compile_fog

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            
            #pragma vertex vert
            #pragma fragment frag

            // Lighting include is needed because of GI
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/Shaders/UnlitInput.hlsl"

            CBUFFER_START(UnityPerMaterial)
                half4 _Color;
                float2 _DistanceFade;
            CBUFFER_END

            TEXTURE2D(_CameraDepthTexture); SAMPLER(sampler_CameraDepthTexture);
            //TEXTURE2D(_BaseMap); SAMPLER(sampler_BaseMap);

            struct VertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };


            struct VertexOutput
            {
                float4 positionCS : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO

                float4  rayBlend : TEXCOORD0;
                half4   screenUV : TEXCOORD1;

                #if defined(_APPLYFOG)
                    half fogCoord : TEXCOORD2;
                #endif

                half fade : TEXCOORD3;

            };

            VertexOutput vert (VertexInput v)
            {
                VertexOutput output = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                output.positionCS = TransformObjectToHClip(v.vertex.xyz);
                output.rayBlend.xyz = mul(UNITY_MATRIX_MV, float4(v.vertex.xyz, 1)).xyz * float3(-1, -1, 1);
                output.screenUV = ComputeScreenPos(output.positionCS);

                #if defined(_APPLYFOG)
                    output.fogCoord = ComputeFogFactor(output.positionCS.z);
                #endif

            //  Set distance fade value
                float3 worldInstancePos = UNITY_MATRIX_M._m03_m13_m23;
                float3 diff = (_WorldSpaceCameraPos - worldInstancePos);
                float dist = dot(diff, diff);
                output.fade = saturate( (_DistanceFade.x - dist) * _DistanceFade.y );

                return output;
            }

            half4 frag (VertexOutput input ) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

            //  Prepare and unpack data
                float3 ray = input.rayBlend.xyz * (_ProjectionParams.z / input.rayBlend.z);
                #if defined(ORTHO_SUPPORT)
                    //  https://github.com/keijiro/DepthInverseProjection/blob/master/Assets/InverseProjection/Resources/InverseProjection.shader
                    float2 rayOrtho = float2( unity_OrthoParams.xy * ( input.screenUV.xy - 0.5) * 2 /* to clip space */);
                    rayOrtho *= float2(-1, -1);
                #endif
                
                float2 uv = input.screenUV.xy / input.screenUV.w;
            //  Fix screenUV for Single Pass Stereo Rendering
                #if defined(UNITY_SINGLE_PASS_STEREO)
                    uv.x = uv.x * 0.5f + (float)unity_StereoEyeIndex * 0.5f;
                #endif 


            //  Read depth and reconstruct world position
                float depth = SAMPLE_DEPTH_TEXTURE_LOD(_CameraDepthTexture, sampler_CameraDepthTexture, uv, 0);
                #if defined(ORTHO_SUPPORT)
                    float depthOrtho = depth;
                    depth = Linear01Depth(depth, _ZBufferParams);
                    #if defined(UNITY_REVERSED_Z)
                    //  Needed to handle openGL
                        #if UNITY_REVERSED_Z == 1
                            depthOrtho = 1.0f - depthOrtho;
                        #endif
                    #endif
                    float4 vpos = float4(ray * depth, 1);
                    float4 vposOrtho = float4(rayOrtho, -depthOrtho * _ProjectionParams.z , 1);
                    vpos = lerp(vpos, vposOrtho, unity_OrthoParams.w);
                #else
                    depth = Linear01Depth(depth, _ZBufferParams);
                    float4 vpos = float4(ray * depth, 1);
                #endif
                
                float3 wpos = mul(unity_CameraToWorld, vpos).xyz;
                #if defined(ORTHO_SUPPORT)
                    wpos -= _WorldSpaceCameraPos * 2 * unity_OrthoParams.w; // TODO: Why * 2 ????
                    wpos.xzy *= 1 - 2 * unity_OrthoParams.w;
                #endif

                float3 opos = mul( GetWorldToObjectMatrix(), float4(wpos, 1)).xyz;
            //  Clip decal to volume
                clip(float3(0.5, 0.5, 0.5) - abs(opos.xyz));

                float2 texUV = opos.xz + float2(0.5, 0.5);

                half4 col = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, texUV) * _Color;
            //  Distance Fade
                #if defined(ORTHO_SUPPORT)
                    col.a *= ((unity_OrthoParams.w == 1.0h) ? 1.0h : input.fade);
                #else
                    col.a *= input.fade;
                #endif

                #if defined(_APPLYFOG)
                    col.rgb = MixFog(col.rgb, input.fogCoord);
                #endif

                return half4(col);
            }
            ENDHLSL
        }
    }
    FallBack "Hidden/InternalErrorShader"
}

