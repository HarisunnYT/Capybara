Shader "Lux LWRP/Projection/Decal Lit"
{
    Properties
    {
        [Header(Surface Options)]
        [Space(5)]
        [ToggleOff(_RECEIVE_SHADOWS_OFF)]
        _ReceiveShadows                                 ("Receive Shadows", Float) = 1.0
        [Toggle(ORTHO_SUPPORT)]
        _OrthoSpport                                    ("Enable Orthographic Support", Float) = 0


        [Header(Surface Inputs)]
        [Space(5)]
        [HDR]_Color                                     ("Color", Color) = (1,1,1,1)
        [NoScaleOffset] _BaseMap                        ("Albedo (RGB) Alpha (A)", 2D) = "white" {}
        _Smoothness                                     ("Smoothness", Range (0, 1)) = 0.1
        _SpecColor                                      ("Specular", Color) = (0.2, 0.2, 0.2)

        [Space(10)]
        [Toggle(_NORMALMAP)] _ApplyNormal               ("Enable Normal Map", Float) = 1.0
        [NoScaleOffset] _BumpMap                        ("    Normal Map", 2D) = "bump" {}
        _BumpScale                                      ("    Normal Scale", Float) = 1.0

        [Header(Mask Map)]
        [Space(5)]
        [Toggle(_COMBINEDTEXTURE)] _CombinedTexture     ("Enable Mask Map", Float) = 0.0
        [NoScaleOffset] _MaskMap                        ("    Metallness (R) Occlusion (G) Emission (B) Smoothness (A) ", 2D) = "bump" {}
        [HDR]_EmissionColor                             ("    Emission Color", Color) = (0,0,0,0)
        _Occlusion                                      ("    Occlusion", Range(0.0, 1.0)) = 1.0

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
        _StencilCompare                                 ("Stencil Comparison", Int) = 8 // always

        [Header(Advanced)]
        [Space(5)]
        [ToggleOff]
        _SpecularHighlights                             ("Enable Specular Highlights", Float) = 1.0
        [ToggleOff]
        _EnvironmentReflections                         ("Environment Reflections", Float) = 1.0
        
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

            // -------------------------------------
            // Material Keywords
            // _NORMALMAP must NOT be shader_feature_local – otherwise fade fails?! Na, it just fails. Toggling "Mask Map" may help bringing back the decal.
            #pragma shader_feature _NORMALMAP
            #pragma shader_feature_local _COMBINEDTEXTURE

            #pragma shader_feature _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature _ENVIRONMENTREFLECTIONS_OFF
            #pragma shader_feature _RECEIVE_SHADOWS_OFF

            #pragma shader_feature_local ORTHO_SUPPORT

            #define _SPECULAR_SETUP 1

            // -------------------------------------
            // Lightweight Pipeline keywords
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            // #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE

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
                half4   _Color;
                half    _Smoothness;
                half3   _SpecColor;

                float2  _DistanceFade;

                #if defined(_NORMALMAP)
                    half    _BumpScale;
                #endif

                #if defined(_COMBINEDTEXTURE)
                    half3 _EmissionColor;
                    half _Occlusion;
                #endif

            CBUFFER_END

            TEXTURE2D(_CameraDepthTexture); SAMPLER(sampler_CameraDepthTexture);
            //TEXTURE2D(_BaseMap); SAMPLER(sampler_BaseMap);
            #if defined(_COMBINEDTEXTURE)
                TEXTURE2D(_MaskMap); SAMPLER(sampler_MaskMap);
            #endif

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

                half2   fogCoord : TEXCOORD2;   // x fog, y fade
                
                #if defined(_NORMALMAP)
                    half3 normalWS              : TEXCOORD3; 
                    half3 tangentWS             : TEXCOORD4;    
                    half3 bitangentWS           : TEXCOORD5;
                #endif   
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

                output.fogCoord.x = ComputeFogFactor(output.positionCS.z);
            //  Set distance fade value
                float3 worldInstancePos = UNITY_MATRIX_M._m03_m13_m23;
                float3 diff = (_WorldSpaceCameraPos - worldInstancePos);
                float dist = dot(diff, diff);
                output.fogCoord.y = saturate( (_DistanceFade.x - dist) * _DistanceFade.y );

                #if defined(_NORMALMAP)
                    output.normalWS = TransformObjectToWorldNormal(half3(0.0h, 1.0h, 0.0h));
                    output.tangentWS = TransformObjectToWorldDir(half3(1.0h, 0.0h, 0.0h));
                    half tangentSign = (-1.0) * unity_WorldTransformParams.w;
                    output.bitangentWS = cross(output.normalWS, output.tangentWS) * tangentSign;
                #endif

                return output;
            }

        //  https://www.gamedev.net/forums/topic/678043-how-to-blend-world-space-normals/
        //  same as in: ScriptableRenderPipeline/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl
            
            half3 ReorientNormalInWorldSpace(in half3 u, in half3 t, in half3 s) {
            //  Build the shortest-arc quaternion
                half dotSTplusOne = dot(s, t) + 1.0h;
                half4 q = half4(cross(s, t), dotSTplusOne ) / sqrt(2.0h * ( dotSTplusOne ));
            //  Rotate the normal
                return u * (q.w * q.w - dot(q.xyz, q.xyz)) + 2.0h * q.xyz * dot(q.xyz, u) + 2.0h * q.w * cross(q.xyz, u);
            }


            #define oneMinusDielectricSpecConst half(1.0 - 0.04)

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
                clip(float3(0.5f, 0.5f, 0.5f) - abs(opos.xyz));

                float2 texUV = opos.xz + float2(0.5f, 0.5f);
                half4 col = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, texUV) * _Color;

            //  Distance Fade
                #if defined(ORTHO_SUPPORT)
                    half alpha = col.a * ((unity_OrthoParams.w == 1.0h) ? 1.0h : input.fogCoord.y);
                #else
                    half alpha = col.a * input.fogCoord.y;
                #endif

                #if defined(_COMBINEDTEXTURE)
                    half4 combinedTextureSample = SAMPLE_TEXTURE2D(_MaskMap, sampler_MaskMap, texUV);
                    half3 specular = lerp(_SpecColor, col.rgb, combinedTextureSample.rrr);
                //  Remap albedo
                    col.rgb *= oneMinusDielectricSpecConst - combinedTextureSample.rrr * oneMinusDielectricSpecConst;
                    half smoothness = combinedTextureSample.a;
                    half occlusion = lerp(1.0h, combinedTextureSample.g, _Occlusion);
                    half3 emission = _EmissionColor * combinedTextureSample.b;
                #else
                    half3 specular = _SpecColor;
                    half smoothness = _Smoothness;
                    half occlusion = 1.0h;
                    half3 emission = 0;
                #endif

            //  Prepare inputs for the lighting function and get normals
                InputData inputData;
                inputData.positionWS = wpos;
                
                inputData.normalWS = /*normalize*/( cross( ddy(wpos), ddx(wpos) ) ); // normalize does not add any improvement

                #if defined(_NORMALMAP)
                    half3 normalTS = UnpackNormalScale( SAMPLE_TEXTURE2D(_BumpMap, sampler_BumpMap, texUV), _BumpScale);
                    half3 normalWS = TransformTangentToWorld(normalTS, half3x3(input.tangentWS.xyz, input.bitangentWS.xyz, input.normalWS.xyz));
                    inputData.normalWS = ReorientNormalInWorldSpace(inputData.normalWS, normalWS, input.normalWS.xyz);
                #endif
                    
                inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);
                inputData.viewDirectionWS = SafeNormalize( _WorldSpaceCameraPos - wpos);
                #if SHADOWS_SCREEN
                    inputData.shadowCoord = input.screenUV; //ComputeScreenPos(vertexInput.positionCS);
                #else
                    inputData.shadowCoord = TransformWorldToShadowCoord(wpos); //vertexInput.positionWS);
                #endif
                inputData.fogCoord = 0;
             // We can't calculate per vertex lighting
                inputData.vertexLighting = 0;
            //  So we have to sample SH fully per pixel
                inputData.bakedGI = SampleSH(inputData.normalWS);

                col = LightweightFragmentPBR(
                    inputData, 
                    col.rgb, 
                    0, //surfaceData.metallic, 
                    specular, 
                    smoothness,
                    occlusion,
                    emission,
                    alpha);

                col.rgb = MixFog(col.rgb, input.fogCoord.x);
                return half4(col.rgb, alpha);
            }
            ENDHLSL
        }
    }
    FallBack "Hidden/InternalErrorShader"
    CustomEditor "LuxLWRPUniversalCustomShaderGUI"
}