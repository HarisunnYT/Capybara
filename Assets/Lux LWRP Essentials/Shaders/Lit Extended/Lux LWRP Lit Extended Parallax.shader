Shader "Lux LWRP/Lit Extended Parallax"
{
    Properties
    {
        [Header(Surface Options)]
        [Space(5)]

        [Enum(UnityEngine.Rendering.CompareFunction)]
        _ZTest                      ("ZTest", Int) = 4
        [Enum(UnityEngine.Rendering.CullMode)]
        _Cull                       ("Culling", Float) = 2
        [Toggle(_ALPHATEST_ON)]
        _AlphaClip                  ("Alpha Clipping", Float) = 0.0
        _Cutoff                     ("    Threshold", Range(0.0, 1.0)) = 0.5
        [Toggle(_RECEIVE_SHADOWS_OFF)]
        _Shadows                    ("Disable Receive Shadows", Float) = 0.0

       
        [Header(Surface Inputs)]
        [Space(5)]
        [MainColor] _BaseColor("Color", Color) = (0.5,0.5,0.5,1)
        [MainTexture] _BaseMap("Albedo", 2D) = "white" {}
        [Space(5)]
        _Parallax                           ("Extrusion", Range (0.0, 0.08)) = 0.02
        [NoScaleOffset] _HeightMap          ("Height Map (G)", 2D) = "black" {}

    //  The shader always expects a normal map
        [Space(5)]
        [NoScaleOffset] _BumpMap            ("Normal Map", 2D) = "bump" {}
        _BumpScale                          ("Normal Scale", Float) = 1.0

        [Space(5)]
        [Toggle(_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A)]
        _SmoothnessTextureChannel           ("Sample Smoothness from Albedo Alpha", Float) = 0
        _Smoothness                         ("Smoothness", Range(0.0, 1.0)) = 0.5

        [Header(Work Flow)]

        [Space(5)]
        _SpecColor                          ("Specular Color", Color) = (0.2, 0.2, 0.2)
        [NoScaleOffset] _SpecGlossMap       ("Specular Map", 2D) = "white" {}

        [ToggleOff(_SPECULAR_SETUP)]  
        _WorkflowMode                       ("Metal Workflow", Float) = 1.0
        [Space(5)]
        [Gamma] _Metallic                   ("    Metallic", Range(0.0, 1.0)) = 0.0
        [NoScaleOffset] _MetallicGlossMap   ("    Metallic Map", 2D) = "white" {}

        [Space(5)]
        [Toggle(_METALLICSPECGLOSSMAP)] 
        _EnableMetalSpec                    ("Enable Spec/Metal Map", Float) = 0.0

        [Header(Additional Maps)]
        [Space(10)]
        [Toggle(_OCCLUSIONMAP)] 
        _EnableOcclusion                    ("Enable Occlusion", Float) = 0.0
        [NoScaleOffset] _OcclusionMap       ("    Occlusion Map", 2D) = "white" {}
        _OcclusionStrength                  ("    Occlusion Strength", Range(0.0, 1.0)) = 1.0

        [Space(5)]
        [Toggle(_EMISSION)] 
        _Emission                           ("Enable Emission", Float) = 0.0
        [HDR]_EmissionColor                 ("    Color", Color) = (0,0,0)
        [NoScaleOffset] _EmissionMap        ("    Emission", 2D) = "white" {}


        [Header(Rim Lighting)]
        [Space(5)]
        [Toggle(_RIMLIGHTING)]
        _Rim                                ("Enable Rim Lighting", Float) = 0
        [HDR] _RimColor                     ("Rim Color", Color) = (0.5,0.5,0.5,1)
        _RimPower                           ("Rim Power", Float) = 2
        _RimFrequency                       ("Rim Frequency", Float) = 0
        _RimMinPower                        ("    Rim Min Power", Float) = 1
        _RimPerPositionFrequency            ("    Rim Per Position Frequency", Range(0.0, 1.0)) = 1

        
        [Header(Stencil)]
        [Space(5)]
        [IntRange] _Stencil         ("Stencil Reference", Range (0, 255)) = 0
        [IntRange] _ReadMask        ("    Read Mask", Range (0, 255)) = 255
        [IntRange] _WriteMask       ("    Write Mask", Range (0, 255)) = 255
        [Enum(UnityEngine.Rendering.CompareFunction)]
        _StencilComp                ("Stencil Comparison", Int) = 8     // always – terrain should be the first thing being rendered anyway
        [Enum(UnityEngine.Rendering.StencilOp)]
        _StencilOp                  ("Stencil Operation", Int) = 0      // 0 = keep, 2 = replace
        [Enum(UnityEngine.Rendering.StencilOp)]
        _StencilFail                ("Stencil Fail Op", Int) = 0           // 0 = keep
        [Enum(UnityEngine.Rendering.StencilOp)] 
        _StencilZFail               ("Stencil ZFail Op", Int) = 0          // 0 = keep


        [Header(Advanced)]
        [Space(5)]
        [ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
        [ToggleOff] _EnvironmentReflections("Environment Reflections", Float) = 1.0
        
        // Blending state
        [HideInInspector] _Surface("__surface", Float) = 0.0
        [HideInInspector] _Blend("__blend", Float) = 0.0
//[HideInInspector] _AlphaClip("__clip", Float) = 0.0
        [HideInInspector] _SrcBlend("__src", Float) = 1.0
        [HideInInspector] _DstBlend("__dst", Float) = 0.0
        [HideInInspector] _ZWrite("__zw", Float) = 1.0
//[HideInInspector] _Cull("__cull", Float) = 2.0

// _ReceiveShadows("Receive Shadows", Float) = 1.0        
        // Editmode props
        [HideInInspector] _QueueOffset("Queue offset", Float) = 0.0
        
        // ObsoleteProperties
        [HideInInspector] _MainTex("BaseMap", 2D) = "white" {}
        [HideInInspector] _Color("Base Color", Color) = (0.5, 0.5, 0.5, 1)
        [HideInInspector] _GlossMapScale("Smoothness", Float) = 0.0
        [HideInInspector] _Glossiness("Smoothness", Float) = 0.0
        [HideInInspector] _GlossyReflections("EnvironmentReflections", Float) = 0.0
    }

    SubShader
    {
        // Lightweight Pipeline tag is required. If Lightweight render pipeline is not set in the graphics settings
        // this Subshader will fail. One can add a subshader below or fallback to Standard built-in to make this
        // material work with both Lightweight Render Pipeline and Builtin Unity Pipeline
        Tags{"RenderType" = "Opaque" "RenderPipeline" = "LightweightPipeline" "IgnoreProjector" = "True"}
        LOD 300

        // ------------------------------------------------------------------
        //  Forward pass. Shades all light in a single pass. GI + emission + Fog
        Pass
        {
            // Lightmode matches the ShaderPassName set in LightweightRenderPipeline.cs. SRPDefaultUnlit and passes with
            // no LightMode tag are also rendered by Lightweight Render Pipeline
            Name "ForwardLit"
            Tags{"LightMode" = "LightweightForward"}

            Stencil {
                Ref   [_Stencil]
                ReadMask [_ReadMask]
                WriteMask [_WriteMask]
                Comp  [_StencilComp]
                Pass  [_StencilOp]
                Fail  [_StencilFail]
                ZFail [_StencilZFail]
                //replace
            }

            Blend[_SrcBlend][_DstBlend]
            ZTest [_ZTest]
            ZWrite[_ZWrite]
            Cull[_Cull]

            HLSLPROGRAM
            // Required to compile gles 2.0 with standard SRP library
            // All shaders must be compiled with HLSLcc and currently only gles is not using HLSLcc by default
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0

            // -------------------------------------
            // Material Keywords
//#pragma shader_feature _NORMALMAP
            #pragma shader_feature _ALPHATEST_ON
            #pragma shader_feature _ALPHAPREMULTIPLY_ON
            #pragma shader_feature _EMISSION            
#pragma shader_feature _METALLICSPECGLOSSMAP
            #pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature _OCCLUSIONMAP

            #pragma shader_feature _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature _ENVIRONMENTREFLECTIONS_OFF
#pragma shader_feature _SPECULAR_SETUP
#pragma shader_feature _RECEIVE_SHADOWS_OFF

            #pragma shader_feature_local _RIMLIGHTING

#define _PARALLAX
#define _NORMALMAP

#if defined(_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A)
    #undef _ALPHATEST_ON
#endif

            // -------------------------------------
            // Lightweight Pipeline keywords
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE

            // -------------------------------------
            // Unity defined keywords
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile_fog

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            
            #pragma vertex LitPassVertex
            #pragma fragment LitPassFragmentRim

            #include "Includes/Lux LWRP Lit Extended Inputs.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/Shaders/LitForwardPass.hlsl"

            half4 LitPassFragmentRim(Varyings input) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                half3x3 tangentSpaceRotation =  half3x3(input.tangentWS.xyz, input.bitangentWS.xyz, input.normalWS.xyz);
                half3 viewDirWS = half3(input.normalWS.w, input.tangentWS.w, input.bitangentWS.w);
                half3 viewDirTS = SafeNormalize( mul(tangentSpaceRotation, viewDirWS) );

                SurfaceData surfaceData;
                InitializeStandardLitSurfaceDataParallax(input.uv, viewDirTS, surfaceData);

                InputData inputData;
                InitializeInputData(input, surfaceData.normalTS, inputData);

                #if defined(_RIMLIGHTING)
                    half rim = saturate(1.0h - saturate( dot(inputData.normalWS, normalize(viewDirWS)) ) );
                    half power = _RimPower;
                    UNITY_BRANCH if(_RimFrequency > 0 ) {
                        half perPosition = lerp(0.0h, 1.0h, dot(1.0h, frac(UNITY_MATRIX_M._m03_m13_m23) * 2.0h - 1.0h ) * _RimPerPositionFrequency ) * 3.1416h;
                        power = lerp(power, _RimMinPower, (1.0h + sin(_Time.y * _RimFrequency + perPosition) ) * 0.5h );
                    }
                    surfaceData.emission += pow(rim, power) * _RimColor.rgb * _RimColor.a;
                #endif

                half4 color = LightweightFragmentPBR(inputData, surfaceData.albedo, surfaceData.metallic, surfaceData.specular, surfaceData.smoothness, surfaceData.occlusion, surfaceData.emission, surfaceData.alpha);
                color.rgb = MixFog(color.rgb, inputData.fogCoord);

                return color;
            }
            ENDHLSL
        }

        Pass
        {
            Name "ShadowCaster"
            Tags{"LightMode" = "ShadowCaster"}

            ZWrite On
            //ZTest LEqual
            ZTest [_ZTest]
            Cull[_Cull]

            HLSLPROGRAM
            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature _ALPHATEST_ON
            #pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

#if defined(_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A)
    #undef _ALPHATEST_ON
#endif

            #define _PARALLAX

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing

            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment

            #include "Includes/Lux LWRP Lit Extended Inputs.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Shadows.hlsl"
            //#include "Packages/com.unity.render-pipelines.lightweight/Shaders/ShadowCasterPass.hlsl"
            
            float3 _LightDirection;

            VertexOutput ShadowPassVertex(VertexInput input)
            {
                VertexOutput output = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);

                float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
                //float3 normalWS = TransformObjectToWorldDir(input.normalOS);

                VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);

                #if defined(_ALPHATEST_ON)
                    output.uv = TRANSFORM_TEX(input.texcoord, _BaseMap);
                    //half3x3 tangentSpaceRotation =  half3x3(normalInput.tangentWS, normalInput.bitangentWS, normalInput.normalWS);
                    half3 viewDirWS = GetCameraPositionWS() - positionWS;
                    //output.viewDirTS = SafeNormalize( mul(tangentSpaceRotation, viewDirWS) );
                    output.normalWS = half4(normalInput.normalWS, viewDirWS.x);
                    output.tangentWS = half4(normalInput.tangentWS, viewDirWS.y);
                    output.bitangentWS = half4(normalInput.bitangentWS, viewDirWS.z);
                #endif

                output.positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalInput.normalWS, _LightDirection));
                #if UNITY_REVERSED_Z
                    output.positionCS.z = min(output.positionCS.z, output.positionCS.w * UNITY_NEAR_CLIP_VALUE);
                #else
                    output.positionCS.z = max(output.positionCS.z, output.positionCS.w * UNITY_NEAR_CLIP_VALUE);
                #endif
                return output;
            }

            half4 ShadowPassFragment(VertexOutput input) : SV_TARGET
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                #if defined(_ALPHATEST_ON)

                //  Parallax
                    half3x3 tangentSpaceRotation =  half3x3(input.tangentWS.xyz, input.bitangentWS.xyz, input.normalWS.xyz);
                    half3 viewDirWS = half3(input.normalWS.w, input.tangentWS.w, input.bitangentWS.w);
                    half3 viewDirTS = SafeNormalize( mul(tangentSpaceRotation, viewDirWS) );

                    float2 uv = input.uv;
                    float3 v = SafeNormalize(viewDirTS); //input.viewDirTS);
                    v.z += 0.42;
                    v.xy /= v.z;
                    float halfParallax = _Parallax * 0.5f;
                    float parallax = SAMPLE_TEXTURE2D(_HeightMap, sampler_HeightMap, uv).g * _Parallax - halfParallax;
                    float2 offset1 = parallax * v.xy;
                //  Calculate 2nd height
                    parallax = SAMPLE_TEXTURE2D(_HeightMap, sampler_HeightMap, uv + offset1).g * _Parallax - halfParallax;
                    float2 offset2 = parallax * v.xy;
                //  Final UVs
                    uv += (offset1 + offset2) * 0.5f;

                    half alpha = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv).a;
                    clip (alpha - _Cutoff);
                #endif
                return 0;
            }

            ENDHLSL
        }

        Pass
        {
            Name "DepthOnly"
            Tags{"LightMode" = "DepthOnly"}

            ZWrite On
            ZTest [_ZTest]
            ColorMask 0
            Cull[_Cull]

            HLSLPROGRAM
            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0

            #pragma vertex DepthOnlyVertex
            #pragma fragment DepthOnlyFragment

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature _ALPHATEST_ON
            #pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

#if defined(_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A)
    #undef _ALPHATEST_ON
#endif

#define _PARALLAX

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing

            #include "Includes/Lux LWRP Lit Extended Inputs.hlsl"
            //#include "Packages/com.unity.render-pipelines.lightweight/Shaders/DepthOnlyPass.hlsl"

            VertexOutput DepthOnlyVertex(VertexInput input)
            {
                VertexOutput output = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);

                #if defined(_ALPHATEST_ON)
                    output.uv = TRANSFORM_TEX(input.texcoord, _BaseMap);
                    VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);
                    //half3x3 tangentSpaceRotation =  half3x3(normalInput.tangentWS, normalInput.bitangentWS, normalInput.normalWS);
                //  was half3 - but output is float?! lets add normalize here - not: it breks the regular pass...
                    half3 viewDirWS = GetCameraPositionWS() - positionWS;
                    //output.viewDirTS = SafeNormalize( mul(tangentSpaceRotation, viewDirWS) );
                    output.normalWS = half4(normalInput.normalWS, viewDirWS.x);
                    output.tangentWS = half4(normalInput.tangentWS, viewDirWS.y);
                    output.bitangentWS = half4(normalInput.bitangentWS, viewDirWS.z);
                #endif

                output.positionCS = TransformWorldToHClip(positionWS);
                return output;
            }

            half4 DepthOnlyFragment(VertexOutput input) : SV_TARGET
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                #if defined(_ALPHATEST_ON)
                
                //  Parallax
                    half3x3 tangentSpaceRotation =  half3x3(input.tangentWS.xyz, input.bitangentWS.xyz, input.normalWS.xyz);
                    half3 viewDirWS = half3(input.normalWS.w, input.tangentWS.w, input.bitangentWS.w);
                    half3 viewDirTS = SafeNormalize( mul(tangentSpaceRotation, viewDirWS) );

                    float2 uv = input.uv;
                    float3 v = SafeNormalize(viewDirTS);
                    v.z += 0.42;
                    v.xy /= v.z;
                    float halfParallax = _Parallax * 0.5f;
                    float parallax = SAMPLE_TEXTURE2D(_HeightMap, sampler_HeightMap, uv).g * _Parallax - halfParallax;
                    float2 offset1 = parallax * v.xy;
                //  Calculate 2nd height
                    parallax = SAMPLE_TEXTURE2D(_HeightMap, sampler_HeightMap, uv + offset1).g * _Parallax - halfParallax;
                    float2 offset2 = parallax * v.xy;
                //  Final UVs
                    uv += (offset1 + offset2) * 0.5f;

                    half alpha = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv).a;
                    clip (alpha - _Cutoff);
                #endif

                return 0;
            }

            ENDHLSL
        }

        // This pass it not used during regular rendering, only for lightmap baking.
        Pass
        {
            Name "Meta"
            Tags{"LightMode" = "Meta"}

            Cull Off

            HLSLPROGRAM
            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            #pragma vertex LightweightVertexMeta
            #pragma fragment LightweightFragmentMeta

#define _PARALLAX

            #pragma shader_feature _SPECULAR_SETUP
            #pragma shader_feature _EMISSION
            #pragma shader_feature _METALLICSPECGLOSSMAP
            #pragma shader_feature _ALPHATEST_ON
            #pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            #pragma shader_feature _SPECGLOSSMAP

            #include "Includes/Lux LWRP Lit Extended Inputs.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/Shaders/LitMetaPass.hlsl"

            ENDHLSL
        }

    }
    FallBack "Hidden/InternalErrorShader"
    CustomEditor "LuxLWRPUniversalCustomShaderGUI"
    //CustomEditor "UnityEditor.Rendering.LWRP.ShaderGUI.LitExtendedShader"
}
