#ifndef UNIVERSAL_TERRAIN_LIT_PASSES_INCLUDED
#define UNIVERSAL_TERRAIN_LIT_PASSES_INCLUDED

    #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Lighting.hlsl"

    #if defined(UNITY_INSTANCING_ENABLED) && defined(_TERRAIN_INSTANCED_PERPIXEL_NORMAL)
        #define ENABLE_TERRAIN_PERPIXEL_NORMAL
    #endif

    #ifdef UNITY_INSTANCING_ENABLED
        TEXTURE2D(_TerrainHeightmapTexture);
        TEXTURE2D(_TerrainNormalmapTexture);
        SAMPLER(sampler_TerrainNormalmapTexture);
        float4 _TerrainHeightmapRecipSize;   // float4(1.0f/width, 1.0f/height, 1.0f/(width-1), 1.0f/(height-1))
        float4 _TerrainHeightmapScale;       // float4(hmScale.x, hmScale.y / (float)(kMaxHeight), hmScale.z, 0.0f)
    #endif


    UNITY_INSTANCING_BUFFER_START(Terrain)
        UNITY_DEFINE_INSTANCED_PROP(float4, _TerrainPatchInstanceData)  // float4(xBase, yBase, skipScale, ~)
    UNITY_INSTANCING_BUFFER_END(Terrain)

    struct Attributes
    {
        float4 positionOS : POSITION;
        float3 normalOS : NORMAL;
        float2 texcoord : TEXCOORD0;
        UNITY_VERTEX_INPUT_INSTANCE_ID
    };

    struct Varyings
    {
        float4 uvMainAndLM              : TEXCOORD0; // xy: control, zw: lightmap
    #ifndef TERRAIN_SPLAT_BASEPASS
        float4 uvSplat01                : TEXCOORD1; // xy: splat0, zw: splat1
        float4 uvSplat23                : TEXCOORD2; // xy: splat2, zw: splat3
    #endif

    #if ( defined(_NORMALMAP) || defined(_PARALLAX) ) && !defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
        float4 normal                   : TEXCOORD3;    // xyz: normal, w: viewDir.x
        float4 tangent                  : TEXCOORD4;    // xyz: tangent, w: viewDir.y
        float4 bitangent                : TEXCOORD5;    // xyz: bitangent, w: viewDir.z
    #else
        float3 normal                   : TEXCOORD3;
        float3 viewDir                  : TEXCOORD4;
        half3 vertexSH                  : TEXCOORD5; // SH
    #endif

        half4 fogFactorAndVertexLight   : TEXCOORD6; // x: fogFactor, yzw: vertex light
        float3 positionWS               : TEXCOORD7;
        float4 shadowCoord              : TEXCOORD8;
        float4 clipPos                  : SV_POSITION;
    };


    // ---------------------------

    void InitializeInputData(Varyings IN, half3 normalTS, half3x3 tangentSpaceRotation, half3 viewDirWS, out InputData input)
    {
        input = (InputData)0;

        input.positionWS = IN.positionWS;
        half3 SH = half3(0, 0, 0);

    //  Most of this is passed in
        /*
            #if ( defined(_NORMALMAP) || defined (_PARALLAX) ) && !defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
                half3 viewDirWS = half3(IN.normal.w, IN.tangent.w, IN.bitangent.w);
                input.normalWS = TransformTangentToWorld(normalTS, half3x3(IN.tangent.xyz, IN.bitangent.xyz, IN.normal.xyz));
                SH = SampleSH(input.normalWS.xyz);
            
            #elif defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
                half3 viewDirWS = IN.viewDir;
                float2 sampleCoords = (IN.uvMainAndLM.xy / _TerrainHeightmapRecipSize.zw + 0.5f) * _TerrainHeightmapRecipSize.xy;
                half3 normalWS = TransformObjectToWorldNormal(normalize(SAMPLE_TEXTURE2D(_TerrainNormalmapTexture, sampler_TerrainNormalmapTexture, sampleCoords).rgb * 2 - 1));
                
            //  fix orientation
                half3 tangentWS = cross(GetObjectToWorldMatrix()._13_23_33, normalWS) * -1;
                input.normalWS = TransformTangentToWorld(normalTS, half3x3(tangentWS, -cross(normalWS, tangentWS), normalWS));    

                SH = SampleSH(input.normalWS.xyz);
            #else
                half3 viewDirWS = IN.viewDir;
                input.normalWS = IN.normal;
                SH = IN.vertexSH;
            #endif
            #if SHADER_HINT_NICE_QUALITY
                viewDirWS = SafeNormalize(viewDirWS);
            #endif
        */
        
    //  So this is all that has to be done
        #if defined(_NORMALMAP) || defined (_PARALLAX)              || defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
            input.normalWS = TransformTangentToWorld(normalTS, tangentSpaceRotation);
            SH = SampleSH(input.normalWS.xyz);
        #else
            input.normalWS = IN.normal;
            SH = IN.vertexSH;
        #endif

        input.normalWS = NormalizeNormalPerPixel(input.normalWS);

        input.viewDirectionWS = viewDirWS;
        #ifdef _MAIN_LIGHT_SHADOWS
            input.shadowCoord = IN.shadowCoord;
        #else
            input.shadowCoord = float4(0, 0, 0, 0);
        #endif
        input.fogCoord = IN.fogFactorAndVertexLight.x;
        input.vertexLighting = IN.fogFactorAndVertexLight.yzw;
        input.bakedGI = SAMPLE_GI(IN.uvMainAndLM.zw, SH, input.normalWS);
    }


    #ifdef _TERRAIN_BLEND_HEIGHT
        void HeightBasedSplatModifyCombined(inout half4 splatControl, in half4 heights, inout half height) {
        #ifndef TERRAIN_SPLAT_ADDPASS   // disable for multi-pass
            half4 defaultHeight = heights;
            half4 mweights = splatControl * max(defaultHeight, 1e-5);
        //  Go parallel
            half maxWeight = max( max(mweights.x, mweights.y), max(mweights.z, mweights.w) );
            half mtransition = max(_HeightTransition * maxWeight, 1e-5);
            half mthreshold = maxWeight - mtransition;
            half mscale = 1.0h / mtransition;
            mweights = saturate((mweights - mthreshold ) / mtransition); //  * mscale  );
            half sumHeight = mweights.x + mweights.y + mweights.z + mweights.w;
            half sumSplat = splatControl.x+splatControl.y+splatControl.z+splatControl.w;

            splatControl = mweights / sumHeight;
        //  Must not get more than before...
            splatControl *= sumSplat;
            height = maxWeight;
        #endif
        }
    #endif


    //  Splatting ----------------------------------------------

    #ifndef TERRAIN_SPLAT_BASEPASS

    void SplatmapMix(float4 uvMainAndLM, float4 uvSplat01, float4 uvSplat23, inout half4 splatControl, out half weight, out half4 mixedDiffuse, out half4 defaultSmoothness, inout half3 mixedNormal)
    {

    //  Sample albedo and smoothness
        half4 diffAlbedo[4];
        // sampleProcedural(uvSplat01.xy, _Splat0, sampler_Splat0, diffAlbedo[0]); 
        diffAlbedo[0] = SAMPLE_TEXTURE2D(_Splat0, sampler_Splat0, uvSplat01.xy);
        diffAlbedo[1] = SAMPLE_TEXTURE2D(_Splat1, sampler_Splat0, uvSplat01.zw);
        diffAlbedo[2] = SAMPLE_TEXTURE2D(_Splat2, sampler_Splat0, uvSplat23.xy);
        diffAlbedo[3] = SAMPLE_TEXTURE2D(_Splat3, sampler_Splat0, uvSplat23.zw);

        defaultSmoothness = half4(diffAlbedo[0].a, diffAlbedo[1].a, diffAlbedo[2].a, diffAlbedo[3].a);
        defaultSmoothness *= half4(_Smoothness0, _Smoothness1, _Smoothness2, _Smoothness3);

        // Now that splatControl has changed, we can compute the final weight and normalize
        weight = dot(splatControl, 1.0h);

        #ifdef TERRAIN_SPLAT_ADDPASS
            clip(weight <= 0.005h ? -1.0h : 1.0h);
        #endif

        #ifndef _TERRAIN_BASEMAP_GEN
            // Normalize weights before lighting and restore weights in final modifier functions so that the overal
            // lighting result can be correctly weighted.
            splatControl /= (weight + HALF_MIN);
        #endif

        mixedDiffuse = 0.0h;
        mixedDiffuse += diffAlbedo[0] * half4(_DiffuseRemapScale0.rgb * splatControl.rrr, 1.0h);
        mixedDiffuse += diffAlbedo[1] * half4(_DiffuseRemapScale1.rgb * splatControl.ggg, 1.0h);
        mixedDiffuse += diffAlbedo[2] * half4(_DiffuseRemapScale2.rgb * splatControl.bbb, 1.0h);
        mixedDiffuse += diffAlbedo[3] * half4(_DiffuseRemapScale3.rgb * splatControl.aaa, 1.0h);

        #ifdef _NORMALMAP
            half4 normalSamples[4];
            normalSamples[0] = SAMPLE_TEXTURE2D(_Normal0, sampler_Normal0, uvSplat01.xy);
            //sampleProcedural(uvSplat01.xy, _Normal0, sampler_Normal0, normalSamples[0]); 
            normalSamples[1] = SAMPLE_TEXTURE2D(_Normal1, sampler_Normal0, uvSplat01.zw);
            normalSamples[2] = SAMPLE_TEXTURE2D(_Normal2, sampler_Normal0, uvSplat23.xy);
            normalSamples[3] = SAMPLE_TEXTURE2D(_Normal3, sampler_Normal0, uvSplat23.zw);
            
            half4 normalSample = 0;
            normalSample =  splatControl.r * normalSamples[0];
            normalSample += splatControl.g * normalSamples[1];
            normalSample += splatControl.b * normalSamples[2];
            normalSample += splatControl.a * normalSamples[3];

            half3 nrm = 0.0f;
            half normalScale = dot(half4(_NormalScale0, _NormalScale1, _NormalScale2, _NormalScale3), splatControl);
            nrm = UnpackNormalScale(normalSample, normalScale);
            // avoid risk of NaN when normalizing.
        #if HAS_HALF
            nrm.z += 0.01h;     
        #else
            nrm.z += 1e-5f;
        #endif
            mixedNormal = normalize(nrm.xyz);
        #endif

        //mixedDiffuse = height.xxxx;

    }
    #endif


    void SplatmapFinalColor(inout half4 color, half fogCoord)
    {
        color.rgb *= color.a;
        #ifdef TERRAIN_SPLAT_ADDPASS
            color.rgb = MixFogColor(color.rgb, half3(0,0,0), fogCoord);
        #else
            color.rgb = MixFog(color.rgb, fogCoord);
        #endif
    }

    void TerrainInstancing(inout float4 positionOS, inout float3 normal, inout float2 uv)
    {
    #ifdef UNITY_INSTANCING_ENABLED
        float2 patchVertex = positionOS.xy;
        float4 instanceData = UNITY_ACCESS_INSTANCED_PROP(Terrain, _TerrainPatchInstanceData);

        float2 sampleCoords = (patchVertex.xy + instanceData.xy) * instanceData.z;
        float height = UnpackHeightmap(_TerrainHeightmapTexture.Load(int3(sampleCoords, 0)));

        positionOS.xz = sampleCoords * _TerrainHeightmapScale.xz;
        positionOS.y = height * _TerrainHeightmapScale.y;

        #ifdef ENABLE_TERRAIN_PERPIXEL_NORMAL
            normal = float3(0, 1, 0);
        #else
            normal = _TerrainNormalmapTexture.Load(int3(sampleCoords, 0)).rgb * 2 - 1;
        #endif
        uv = sampleCoords * _TerrainHeightmapRecipSize.zw;
    #endif
    }

    void TerrainInstancing(inout float4 positionOS, inout float3 normal)
    {
        float2 uv = { 0, 0 };
        TerrainInstancing(positionOS, normal, uv);
    }

    ///////////////////////////////////////////////////////////////////////////////
    //                  Vertex and Fragment functions                            //
    ///////////////////////////////////////////////////////////////////////////////

    // Used in Standard Terrain shader
    Varyings SplatmapVert(Attributes v)
    {
        Varyings o = (Varyings)0;
        UNITY_SETUP_INSTANCE_ID(v);
        //UNITY_TRANSFER_INSTANCE_ID(v, o);
        //UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); // Not supported in LWRP

        TerrainInstancing(v.positionOS, v.normalOS, v.texcoord);
        VertexPositionInputs Attributes = GetVertexPositionInputs(v.positionOS.xyz);

        o.uvMainAndLM.xy = v.texcoord;
        o.uvMainAndLM.zw = v.texcoord * unity_LightmapST.xy + unity_LightmapST.zw;
    #ifndef TERRAIN_SPLAT_BASEPASS
        o.uvSplat01.xy = TRANSFORM_TEX(v.texcoord, _Splat0);
        o.uvSplat01.zw = TRANSFORM_TEX(v.texcoord, _Splat1);
        o.uvSplat23.xy = TRANSFORM_TEX(v.texcoord, _Splat2);
        o.uvSplat23.zw = TRANSFORM_TEX(v.texcoord, _Splat3);
    #endif

        half3 viewDirWS = GetCameraPositionWS() - Attributes.positionWS;
    #if !SHADER_HINT_NICE_QUALITY
        viewDirWS = SafeNormalize(viewDirWS);
    #endif

        #if ( defined(_NORMALMAP) || defined(_PARALLAX) ) && !defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
            float4 vertexTangent = float4(cross(float3(0, 0, 1), v.normalOS), 1.0);
            VertexNormalInputs normalInput = GetVertexNormalInputs(v.normalOS, vertexTangent);
        //  fix orientation
            normalInput.tangentWS *= -1;

            o.normal = half4(normalInput.normalWS, viewDirWS.x);
            o.tangent = half4(normalInput.tangentWS, viewDirWS.y);
            o.bitangent = half4(normalInput.bitangentWS, viewDirWS.z);
        #else
            o.normal = TransformObjectToWorldNormal(v.normalOS);
            o.viewDir = viewDirWS;
            o.vertexSH = SampleSH(o.normal);
        #endif

        o.fogFactorAndVertexLight.x = ComputeFogFactor(Attributes.positionCS.z);
        o.fogFactorAndVertexLight.yzw = VertexLighting(Attributes.positionWS, o.normal.xyz);
        o.positionWS = Attributes.positionWS;
        o.clipPos = Attributes.positionCS;

    #ifdef _MAIN_LIGHT_SHADOWS
        o.shadowCoord = GetShadowCoord(Attributes);
    #endif

        return o;
    }

    // Used in Standard Terrain shader
    half4 SplatmapFragment(Varyings IN) : SV_TARGET {
        //UNITY_SETUP_INSTANCE_ID(IN);
        //UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN); // Not supported in LWRP

        #ifdef _ALPHATEST_ON
            half hole = SAMPLE_TEXTURE2D(_TerrainHolesTexture, sampler_TerrainHolesTexture, IN.uvMainAndLM.xy).r;
            clip(hole == 0.0h ? -1 : 1);
        #endif

        half3 normalTS = half3(0.0h, 0.0h, 1.0h);

        half3x3 tangentSpaceRotation = 0;
        half3 viewDirectionWS = 0;

        #if ( defined(_NORMALMAP) || defined (_PARALLAX) ) && !defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
            viewDirectionWS = SafeNormalize( half3(IN.normal.w, IN.tangent.w, IN.bitangent.w) ); 
        #else
            viewDirectionWS = SafeNormalize( IN.viewDir );
        #endif

        #if defined(_NORMALMAP) || defined(_PARALLAX) || defined(TERRAIN_SPLAT_BASEPASS)
            #if !defined(ENABLE_TERRAIN_PERPIXEL_NORMAL) && ( defined(_NORMALMAP) || defined(_PARALLAX) )
            //  Same matrix we need to transfer the normalTS
                tangentSpaceRotation =  half3x3(IN.tangent.xyz, IN.bitangent.xyz, IN.normal.xyz);
                half3 tangentWS = IN.tangent.xyz;
                half3 bitangentWS = IN.bitangent.xyz;
                half3 viewDirTS = normalize( mul(tangentSpaceRotation, viewDirectionWS ) );

            #elif defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
                float2 sampleCoords = (IN.uvMainAndLM.xy / _TerrainHeightmapRecipSize.zw + 0.5f) * _TerrainHeightmapRecipSize.xy;
                half3 normalWS = TransformObjectToWorldNormal(normalize(SAMPLE_TEXTURE2D(_TerrainNormalmapTexture, sampler_TerrainNormalmapTexture, sampleCoords).rgb * 2 - 1));
            //  fix orientation
                half3 tangentWS = cross( /*GetObjectToWorldMatrix()._13_23_33*/ half3(0, 0, 1), normalWS) * -1;
            //  Ups: * -1?
                half3 bitangentWS = cross(normalWS, tangentWS) * -1;
                tangentSpaceRotation =  half3x3(tangentWS, bitangentWS, normalWS);
                half3 viewDirTS = normalize( mul(tangentSpaceRotation, viewDirectionWS) );
            #endif
        #endif
        
        #ifdef TERRAIN_SPLAT_BASEPASS
            half3 albedo = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uvMainAndLM.xy).rgb;
            half smoothness = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uvMainAndLM.xy).a;
// Unity 2019.1
            half metallic = 0; //SAMPLE_TEXTURE2D(_MetallicTex, sampler_MetallicTex, IN.uvMainAndLM.xy).r;
            half alpha = 1;
            half occlusion = 1;
        
        #else
            half4 splatControl;
            half weight;
            half4 mixedDiffuse;
            half4 defaultSmoothness;

            float2 splatUV = (IN.uvMainAndLM.xy * (_Control_TexelSize.zw - 1.0f) + 0.5f) * _Control_TexelSize.xy;
            splatControl = SAMPLE_TEXTURE2D(_Control, sampler_Control, splatUV);

        //  Sample heights
            #ifdef _TERRAIN_BLEND_HEIGHT
                half4 heights;

                heights.x = SAMPLE_TEXTURE2D(_HeightMaps, sampler_Splat0, IN.uvSplat01.xy).r;
                heights.y = SAMPLE_TEXTURE2D(_HeightMaps, sampler_Splat0, IN.uvSplat01.zw).g;
                heights.z = SAMPLE_TEXTURE2D(_HeightMaps, sampler_Splat0, IN.uvSplat23.xy).b;
                heights.w = SAMPLE_TEXTURE2D(_HeightMaps, sampler_Splat0, IN.uvSplat23.zw).a;

                half height;
            //  Adjust spaltControl and calculate 1st height
                HeightBasedSplatModifyCombined(splatControl, heights, height);

            //  Parallax Extrusion
                #if defined(_PARALLAX)
                    float3 v = viewDirTS;
                    v.z += 0.42;
                    v.xy /= v.z;
                    half halfParallax = _Parallax * 0.5h;
                    
                    half parallax = height * _Parallax - halfParallax;
                    float2 offset1 =  parallax * v.xy;

                    float4 splatUV1 = IN.uvSplat01 + offset1.xyxy;
                    float4 splatUV2 = IN.uvSplat23 + offset1.xyxy;

                    heights.x = SAMPLE_TEXTURE2D(_HeightMaps, sampler_Splat0, splatUV1.xy).r;
                    heights.y = SAMPLE_TEXTURE2D(_HeightMaps, sampler_Splat0, splatUV1.zw).g;
                    heights.z = SAMPLE_TEXTURE2D(_HeightMaps, sampler_Splat0, splatUV2.xy).b;
                    heights.w = SAMPLE_TEXTURE2D(_HeightMaps, sampler_Splat0, splatUV2.zw).a;

                //  Calculate 2nd height
                    half height1 = max( max(heights.x, heights.y), max(heights.z, heights.w) );
                    parallax = height1 * _Parallax - halfParallax;
                    float2 offset2 =  parallax * v.xy;
                    
                    offset1 = (offset1 + offset2) * 0.5;
                    IN.uvSplat01 = IN.uvSplat01 + offset1.xyxy * float4(1,1,1,1);
                    IN.uvSplat23 = IN.uvSplat23 + offset1.xyxy * float4(1,1,1,1);

                #endif
            #endif

            SplatmapMix(IN.uvMainAndLM, IN.uvSplat01, IN.uvSplat23, splatControl, weight, mixedDiffuse, defaultSmoothness, normalTS);
        
            half3 albedo = mixedDiffuse.rgb;
// Looks broken...
            defaultSmoothness *= dot(half4(_Smoothness0, _Smoothness1, _Smoothness2, _Smoothness3), splatControl);
            half smoothness = dot(defaultSmoothness, splatControl);


            half metallic = dot(half4(_Metallic0, _Metallic1, _Metallic2, _Metallic3), splatControl);
            half occlusion = 1;
            half alpha = weight;
        #endif

        InputData inputData;
        InitializeInputData(IN, normalTS, tangentSpaceRotation, viewDirectionWS, inputData);

        half4 color = LightweightFragmentPBR(inputData, albedo, metallic, /* specular */ half3(0.0h, 0.0h, 0.0h), smoothness, occlusion, /* emission */ half3(0, 0, 0), alpha);

        SplatmapFinalColor(color, inputData.fogCoord);
        return half4(color.rgb, 1.0h);
    }


    // -----------------------------------------------------------------------------
    // Shadow pass

    // x: global clip space bias, y: normal world space bias
    float3 _LightDirection;

    struct AttributesLean {
        float4 positionOS     : POSITION;
        float3 normalOS       : NORMAL;
        UNITY_VERTEX_INPUT_INSTANCE_ID
    };

    #ifdef _ALPHATEST_ON
        Varyings ShadowPassVertex (Attributes v)
        {
            Varyings o = (Varyings)0;
            UNITY_SETUP_INSTANCE_ID(v);
            TerrainInstancing(v.positionOS, v.normalOS, v.texcoord);
            o.uvMainAndLM.xy = v.texcoord;
            float3 positionWS = TransformObjectToWorld(v.positionOS.xyz);
            float3 normalWS = TransformObjectToWorldNormal(v.normalOS);
            float4 clipPos = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, _LightDirection));
            #if UNITY_REVERSED_Z
                clipPos.z = min(clipPos.z, clipPos.w * UNITY_NEAR_CLIP_VALUE);
            #else
                clipPos.z = max(clipPos.z, clipPos.w * UNITY_NEAR_CLIP_VALUE);
            #endif
            o.clipPos = clipPos;
            return o;
        }
        half4 ShadowPassFragment(Varyings IN) : SV_TARGET {
            //ClipHoles(IN.tc.xy);
            half hole = SAMPLE_TEXTURE2D(_TerrainHolesTexture, sampler_TerrainHolesTexture, IN.uvMainAndLM.xy).r;
            clip(hole == 0.0h ? -1 : 1);
            return 0;
        }
    #else
        float4 ShadowPassVertex(AttributesLean v) : SV_POSITION {
            Varyings o;
            UNITY_SETUP_INSTANCE_ID(v);
            TerrainInstancing(v.positionOS, v.normalOS);
            float3 positionWS = TransformObjectToWorld(v.positionOS.xyz);
            float3 normalWS = TransformObjectToWorldNormal(v.normalOS);
            float4 clipPos = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, _LightDirection));
            #if UNITY_REVERSED_Z
                clipPos.z = min(clipPos.z, clipPos.w * UNITY_NEAR_CLIP_VALUE);
            #else
                clipPos.z = max(clipPos.z, clipPos.w * UNITY_NEAR_CLIP_VALUE);
            #endif
            return clipPos;
        }
        half4 ShadowPassFragment() : SV_TARGET {
            return 0;
        }
    #endif



    // -----------------------------------------------------------------------------
    // Depth pass

    //
    #ifdef _ALPHATEST_ON
        Varyings DepthOnlyVertex(Attributes v) {
            Varyings o = (Varyings)0;
            UNITY_SETUP_INSTANCE_ID(v);
            TerrainInstancing(v.positionOS, v.normalOS, v.texcoord);
            o.uvMainAndLM.xy = v.texcoord;
            o.clipPos = TransformObjectToHClip(v.positionOS.xyz);
            return o;
        }

        half4 DepthOnlyFragment(Varyings IN) : SV_TARGET {
            //ClipHoles(IN.tc.xy);
            half hole = SAMPLE_TEXTURE2D(_TerrainHolesTexture, sampler_TerrainHolesTexture, IN.uvMainAndLM.xy).r;
            clip(hole == 0.0h ? -1 : 1);
            return 0;
        }

    #else
        //float4 DepthOnlyVertex(AttributesLean v) : SV_POSITION {
        Varyings DepthOnlyVertex(AttributesLean v) {
            Varyings o = (Varyings)0;
            UNITY_SETUP_INSTANCE_ID(v);
            //UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); // Not supportd in LWRP
            TerrainInstancing(v.positionOS, v.normalOS);
            //return TransformObjectToHClip(v.positionOS.xyz);
            o.clipPos = TransformObjectToHClip(v.positionOS.xyz);
            return o;
        }

        half4 DepthOnlyFragment() : SV_TARGET {
            //UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN); // Not supportd in LWRP
            return 0;
        }
    #endif

#endif