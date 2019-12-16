///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 04/03/2019
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias Soriano
/// Description: Flat Lighting/Four Lights Blend shader that applies four lights based on the normals of the mesh.
///

// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D

Shader "CG Framework/Flat Lighting/Four Lights"
{
	Properties
	{
		
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5	

		_Color("Color (RGBA)", Color) = (1,1,0,1)
		_FrontLightLevel("Front Light Level", Range( 0 , 1)) = 1
		_RightLightLevel("Right Light Level", Range( 0 , 1)) = 1
		_TopLightLevel("Top Light Level", Range( 0 , 1)) = 1
		_RimLightLevel("Rim Light Level", Range( 0 , 1)) = 1
		_FrontOpacityLevel("Front Opacity Level", Range( 0 , 1)) = 1
		_RightOpacityLevel("Right Opacity Level", Range( 0 , 1)) = 1
		_TopOpacityLevel("Top Opacity Level", Range( 0 , 1)) = 1
		_RimOpacityLevel("Rim Opacity Level", Range( 0 , 1)) = 1
		_MainTex("Main Texture (RGBA)", 2D) = "white" {}
		_FrontTexture("Front Texture (RGBA)", 2D) = "white" {}
		_RightTexture("Right Texture (RGBA)", 2D) = "white" {}
		_TopTexture("Top Texture (RGBA)", 2D) = "white" {}
		_RimTexture("Rim Texture (RGBA)", 2D) = "white" {}
		_MainTextureLevel("Main Texture Level", Range( 0 , 1)) = 1
		_FrontTextureLevel("Front Texture Level", Range( 0 , 1)) = 1
		_RightTextureLevel("Right Texture Level", Range( 0 , 1)) = 1
		_TopTextureLevel("Top Texture Level", Range( 0 , 1)) = 1
		_RimTextureLevel("Rim Texture Level", Range( 0 , 1)) = 1
		[Toggle(_GRADIENT_ON)] _Gradient("Gradient", Float) = 0
		_GradientTopColor("Gradient Top Color (RGBA)", Color) = (0,0,0,1)
		_GradientCenter("Gradient Center", Range( 0 , 1)) = 0.5
		_GradientWidth("Gradient Width", Float) = 0.5
		[Toggle][Toggle]_GradientRevert("Gradient Revert", Float) = 0
		[Toggle][Toggle]_GradientChangeDirection("Gradient Change Direction", Float) = 1
		_GradientRotation("Gradient Rotation", Range( 0 , 360)) = 0
		[Toggle]_ViewDirection("View Direction", Float) = 0
		[Toggle(_HEIGHTFOG_ON)] _HeightFog("Height Fog", Float) = 0
		_HeightFogColor("Height Fog Color (RGB)", Color) = (1,0,0,1)
		_HeightFogStartPosition("Height Fog Start Position", Float) = 0
		_FogHeight("Fog Height", Float) = 0.6
		_HeightFogDensity("Height Fog Density", Range( 0 , 1)) = 1
		[Toggle]_UseAlphaValue("Use Alpha Value", Float) = 0
		[Toggle]_LocalHeightFog("Local Height Fog", Float) = 0
		[Toggle(_DISTANCEFOG_ON)] _DistanceFog("Distance Fog", Float) = 0
		_DistanceFogColor("Distance Fog Color (RGB)", Color) = (0.5019608,0.5019608,0.5019608,1)
		_DistanceFogStartPosition("Distance Fog Start Position", Float) = 10
		_DistanceFogLength("Distance Fog Length", Float) = 0
		_DistanceFogDensity("Distance Fog Density", Range( 0 , 1)) = 1
		[Toggle]_UseAlpha("Use Alpha", Float) = 0
		[Toggle]_WorldDistanceFog("World Distance Fog", Float) = 0
		_WorldDistanceFogPosition("World Distance Fog Position", Vector) = (0,0,0,0)
		[Toggle(_LIGHT_ON)] _Light("Light", Float) = 0
		[Toggle]_DirectionalLight("Directional Light", Float) = 0
		[Toggle]_Ambient("Ambient", Float) = 0
		[Toggle]_SimulatedLight("Simulated Light", Float) = 0
		_SimulatedLightRampTexture("Simulated Light Ramp Texture", 2D) = "white" {}
		[HideInInspector]_GradientTextureWhite("Gradient Texture White", 2D) = "white" {}
		_SimulatedLightLevel("Simulated Light Level", Range( 0 , 1)) = 1
		_SimulatedLightPosition("Simulated Light Position", Vector) = (0,0,-1,0)
		_SimulatedLightDistance("Simulated Light Distance", Float) = 10
		[Toggle]_GradientRamp("Gradient Ramp", Float) = 0
		_CenterColor("Center Color (RGB)", Color) = (1,1,1,1)
		[Toggle]_UseExternalColor("Use External Color", Float) = 0
		_ExternalColor("External Color (RGB)", Color) = (1,0,0,1)
		[Toggle]_AdditiveSimulatedLight("Additive Simulated Light", Float) = 0
		_AdditiveSimulatedLightLevel("Additive Simulated Light Level", Range( 0 , 1)) = 1
		[Toggle]_Posterize("Posterize", Float) = 0
		_Steps("Steps", Range( 2 , 20)) = 2
		[Toggle(_LIGHTMAP_ON)] _Lightmap("Lightmap", Float) = 0
		_LightmapColor("Lightmap Color (RGB)", Color) = (0,0,0,1)
		_LightmapLevel("Lightmap Level", Range( 0 , 20)) = 5
		_ShadowLevel("Shadow Level", Range( -1 , 1)) = 0
		[Toggle]_MultiplyLightmap("Multiply Lightmap", Float) = 0
		[Toggle]_DesaturateLightColor("Desaturate Light Color", Float) = 1
		[HideInInspector]unity_Lightmap("unity_Lightmap", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

		[HideInInspector]_RenderMode("Render Mode", Float) = 0.0
		[HideInInspector]_SrcBlend ("SrcBlend", Float) = 1.0
        [HideInInspector]_DstBlend ("DstBlend", Float) = 0.0
        [HideInInspector]_ZWrite ("ZWrite", Float) = 1.0
		[HideInInspector]_Cull ("Cull", Float) = 0.0
	}
	
	SubShader
	{
		Tags { "Queue"="Geometry" "RenderType"="Opaque" "LightMode"="ForwardBase" }
		LOD 100
		Cull [_Cull]
		ZWrite [_ZWrite]
		

		Pass
		{

			Blend [_SrcBlend] [_DstBlend]
            ZWrite [_ZWrite]

			CGPROGRAM
			#pragma target 3.0 
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#pragma multi_compile __ _ALPHATEST_ON
			#include "Lighting.cginc"
			#include "UnityShaderVariables.cginc"
			#pragma shader_feature _DISTANCEFOG_ON
			#pragma shader_feature _HEIGHTFOG_ON
			#pragma shader_feature _LIGHT_ON
			#pragma shader_feature _LIGHTMAP_ON
			#pragma shader_feature _GRADIENT_ON



			struct appdata
			{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float3 ase_normal : NORMAL;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
			};

			uniform fixed _Cutoff;
			uniform half _SimulatedLight;
			uniform float4 _Color;
			uniform float4 _GradientTopColor;
			uniform half _GradientCenter;
			uniform half _GradientWidth;
			uniform half _GradientRevert;
			uniform half _GradientChangeDirection;
			uniform half _GradientRotation;
			uniform half _ViewDirection;
			uniform half _FrontLightLevel;
			uniform sampler2D _FrontTexture;
			uniform float4 _FrontTexture_ST;
			uniform half _FrontTextureLevel;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform half _MainTextureLevel;
			uniform half _RightLightLevel;
			uniform sampler2D _RightTexture;
			uniform float4 _RightTexture_ST;
			uniform half _RightTextureLevel;
			uniform half _TopLightLevel;
			uniform sampler2D _TopTexture;
			uniform float4 _TopTexture_ST;
			uniform half _TopTextureLevel;
			uniform half _RimLightLevel;
			uniform sampler2D _RimTexture;
			uniform float4 _RimTexture_ST;
			uniform half _RimTextureLevel;
			uniform half _FrontOpacityLevel;
			uniform half _RightOpacityLevel;
			uniform half _TopOpacityLevel;
			uniform half _RimOpacityLevel;
			uniform float _MultiplyLightmap;
			uniform float4 _LightmapColor;
			uniform float _DesaturateLightColor;
			// uniform sampler2D unity_Lightmap;
			// uniform float4 unity_LightmapST;
			uniform float _ShadowLevel;
			uniform float _LightmapLevel;
			uniform float _DirectionalLight;
			uniform float _Ambient;
			uniform half _AdditiveSimulatedLight;
			uniform half _GradientRamp;
			uniform sampler2D _SimulatedLightRampTexture;
			uniform float3 _SimulatedLightPosition;
			uniform float _SimulatedLightDistance;
			uniform sampler2D _GradientTextureWhite;
			uniform half _Posterize;
			uniform float4 _CenterColor;
			uniform float _UseExternalColor;
			uniform float4 _ExternalColor;
			uniform half _Steps;
			uniform half _AdditiveSimulatedLightLevel;
			uniform float _SimulatedLightLevel;
			uniform half4 _HeightFogColor;
			uniform half _LocalHeightFog;
			uniform float _HeightFogStartPosition;
			uniform float _FogHeight;
			uniform float _HeightFogDensity;
			uniform float _UseAlphaValue;
			uniform float4 _DistanceFogColor;
			uniform float _DistanceFogLength;
			uniform half _WorldDistanceFog;
			uniform half3 _WorldDistanceFogPosition;
			uniform float _DistanceFogDensity;
			uniform float _DistanceFogStartPosition;
			uniform half _UseAlpha;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.texcoord.xy = v.texcoord.xy;
				o.texcoord.zw = v.texcoord1.xy;
				
				float3 ase_worldNormal = UnityObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord1.xyz = ase_worldNormal;
				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.ase_texcoord2.xyz = ase_worldPos;
				
				o.ase_texcoord3 = v.vertex;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.w = 0;
				o.ase_texcoord2.w = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				fixed4 myColorVar;
				float2 uv0131_g373 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float cos130_g373 = cos( radians( _GradientRotation ) );
				float sin130_g373 = sin( radians( _GradientRotation ) );
				float2 rotator130_g373 = mul( uv0131_g373 - float2( 0.5,0.5 ) , float2x2( cos130_g373 , -sin130_g373 , sin130_g373 , cos130_g373 )) + float2( 0.5,0.5 );
				float2 break129_g373 = rotator130_g373;
				float clampResult116_g373 = clamp( ( lerp(lerp(break129_g373.x,break129_g373.y,_GradientChangeDirection),( 1.0 - lerp(break129_g373.x,break129_g373.y,_GradientChangeDirection) ),_GradientRevert) + ( ( _GradientCenter * 2.0 ) - 1.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult144_g373 = smoothstep( ( _GradientCenter - _GradientWidth ) , ( _GradientCenter + _GradientWidth ) , clampResult116_g373);
				float3 lerpResult124_g373 = lerp( (_Color).rgb , (_GradientTopColor).rgb , smoothstepResult144_g373);
				float lerpResult114_g373 = lerp( (_Color).a , _GradientTopColor.a , smoothstepResult144_g373);
				float4 appendResult118_g373 = (float4(lerpResult124_g373 , lerpResult114_g373));
				#ifdef _GRADIENT_ON
				float4 staticSwitch125_g373 = appendResult118_g373;
				#else
				float4 staticSwitch125_g373 = _Color;
				#endif
				float3 temp_output_128_0_g373 = (staticSwitch125_g373).rgb;
				float3 ase_worldNormal = i.ase_texcoord1.xyz;
				float3 normalizeResult4_g373 = normalize( ase_worldNormal );
				float3 ase_worldPos = i.ase_texcoord2.xyz;
				float3 ase_worldViewDir = UnityWorldSpaceViewDir(ase_worldPos);
				ase_worldViewDir = normalize(ase_worldViewDir);
				float temp_output_8_0_g373 = (lerp(normalizeResult4_g373,( ase_worldViewDir * normalizeResult4_g373 ),_ViewDirection)).z;
				float3 appendResult14_g373 = (float3(0.0 , 0.0 , temp_output_8_0_g373));
				float dotResult17_g373 = dot( float3( 0,0,-1 ) , appendResult14_g373 );
				float clampResult21_g373 = clamp( dotResult17_g373 , -1.0 , 1.0 );
				float ifLocalVar34_g373 = 0;
				if( temp_output_8_0_g373 >= 0.0 )
				ifLocalVar34_g373 = (float)0;
				else
				ifLocalVar34_g373 = (float)1;
				float lerpResult40_g373 = lerp( 0.0 , ( 1.0 - ( degrees( acos( clampResult21_g373 ) ) / 90.0 ) ) , ifLocalVar34_g373);
				float2 uv_FrontTexture = i.texcoord.xy * _FrontTexture_ST.xy + _FrontTexture_ST.zw;
				float4 lerpResult91_g373 = lerp( float4( 1,1,1,1 ) , tex2D( _FrontTexture, uv_FrontTexture ) , _FrontTextureLevel);
				float2 uv_MainTex = i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 lerpResult136_g373 = lerp( float4( 1,1,1,1 ) , tex2D( _MainTex, uv_MainTex ) , _MainTextureLevel);
				float3 temp_output_140_0_g373 = (lerpResult136_g373).rgb;
				float temp_output_9_0_g373 = (lerp(normalizeResult4_g373,( ase_worldViewDir * normalizeResult4_g373 ),_ViewDirection)).x;
				float3 appendResult15_g373 = (float3(temp_output_9_0_g373 , 0.0 , 0.0));
				float dotResult16_g373 = dot( float3( 1,0,0 ) , appendResult15_g373 );
				float clampResult19_g373 = clamp( dotResult16_g373 , -1.0 , 1.0 );
				float ifLocalVar37_g373 = 0;
				if( temp_output_9_0_g373 <= 0.0 )
				ifLocalVar37_g373 = (float)0;
				else
				ifLocalVar37_g373 = (float)1;
				float lerpResult41_g373 = lerp( 0.0 , ( 1.0 - ( degrees( acos( clampResult19_g373 ) ) / 90.0 ) ) , ifLocalVar37_g373);
				float2 uv_RightTexture = i.texcoord.xy * _RightTexture_ST.xy + _RightTexture_ST.zw;
				float4 lerpResult98_g373 = lerp( float4( 1,1,1,1 ) , tex2D( _RightTexture, uv_RightTexture ) , _RightTextureLevel);
				float temp_output_7_0_g373 = (lerp(normalizeResult4_g373,( ase_worldViewDir * normalizeResult4_g373 ),_ViewDirection)).y;
				float3 appendResult13_g373 = (float3(0.0 , temp_output_7_0_g373 , 0.0));
				float dotResult18_g373 = dot( float3( 0,1,0 ) , appendResult13_g373 );
				float clampResult20_g373 = clamp( dotResult18_g373 , -1.0 , 1.0 );
				float ifLocalVar39_g373 = 0;
				if( temp_output_7_0_g373 <= 0.0 )
				ifLocalVar39_g373 = (float)0;
				else
				ifLocalVar39_g373 = (float)1;
				float lerpResult42_g373 = lerp( 0.0 , ( 1.0 - ( degrees( acos( clampResult20_g373 ) ) / 90.0 ) ) , ifLocalVar39_g373);
				float2 uv_TopTexture = i.texcoord.xy * _TopTexture_ST.xy + _TopTexture_ST.zw;
				float4 lerpResult103_g373 = lerp( float4( 1,1,1,1 ) , tex2D( _TopTexture, uv_TopTexture ) , _TopTextureLevel);
				float temp_output_48_0_g373 = ( 1.0 - ( lerpResult40_g373 + lerpResult41_g373 + lerpResult42_g373 ) );
				float2 uv_RimTexture = i.texcoord.xy * _RimTexture_ST.xy + _RimTexture_ST.zw;
				float4 lerpResult108_g373 = lerp( float4( 1,1,1,1 ) , tex2D( _RimTexture, uv_RimTexture ) , _RimTextureLevel);
				float temp_output_110_0_g373 = (staticSwitch125_g373).a;
				float temp_output_139_0_g373 = (lerpResult136_g373).a;
				float4 appendResult71_g373 = (float4(( ( temp_output_128_0_g373 * lerpResult40_g373 * _FrontLightLevel * (lerpResult91_g373).rgb * temp_output_140_0_g373 ) + ( temp_output_128_0_g373 * lerpResult41_g373 * _RightLightLevel * (lerpResult98_g373).rgb * temp_output_140_0_g373 ) + ( temp_output_128_0_g373 * lerpResult42_g373 * _TopLightLevel * (lerpResult103_g373).rgb * temp_output_140_0_g373 ) + ( temp_output_128_0_g373 * temp_output_48_0_g373 * _RimLightLevel * (lerpResult108_g373).rgb * temp_output_140_0_g373 ) ) , ( ( lerpResult40_g373 * _FrontOpacityLevel * temp_output_110_0_g373 * (lerpResult91_g373).a * temp_output_139_0_g373 ) + ( lerpResult41_g373 * _RightOpacityLevel * temp_output_110_0_g373 * (lerpResult98_g373).a * temp_output_139_0_g373 ) + ( lerpResult42_g373 * _TopOpacityLevel * temp_output_110_0_g373 * (lerpResult103_g373).a * temp_output_139_0_g373 ) + ( temp_output_110_0_g373 * temp_output_48_0_g373 * _RimOpacityLevel * (lerpResult108_g373).a * temp_output_139_0_g373 ) )));
				float4 temp_output_1_0_g374 = appendResult71_g373;
				float4 tex2DNode7_g374 = UNITY_SAMPLE_TEX2D( unity_Lightmap, (i.texcoord.zw*(unity_LightmapST).xy + (unity_LightmapST).zw) );
				float3 desaturateInitialColor8_g374 = tex2DNode7_g374.rgb;
				float desaturateDot8_g374 = dot( desaturateInitialColor8_g374, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar8_g374 = lerp( desaturateInitialColor8_g374, desaturateDot8_g374.xxx, 1.0 );
				float3 clampResult11_g374 = clamp( ( lerp((tex2DNode7_g374).rgb,desaturateVar8_g374,_DesaturateLightColor) + _ShadowLevel ) , float3( 0,0,0 ) , float3( 1,1,1 ) );
				float4 appendResult12_g374 = (float4(clampResult11_g374 , 1.0));
				float4 lerpResult14_g374 = lerp( float4( 1,1,1,1 ) , appendResult12_g374 , _LightmapLevel);
				float4 lerpResult16_g374 = lerp( _LightmapColor , float4( 1,1,1,1 ) , lerpResult14_g374);
				float4 appendResult29_g374 = (float4(desaturateVar8_g374 , 1.0));
				float4 lerpResult28_g374 = lerp( lerpResult16_g374 , temp_output_1_0_g374 , appendResult29_g374);
				#ifdef _LIGHTMAP_ON
				float4 staticSwitch32_g374 = lerp(lerpResult28_g374,( lerpResult16_g374 * temp_output_1_0_g374 ),_MultiplyLightmap);
				#else
				float4 staticSwitch32_g374 = temp_output_1_0_g374;
				#endif
				float4 temp_output_9_0_g375 = staticSwitch32_g374;
				float3 temp_output_15_0_g375 = (temp_output_9_0_g375).rgb;
				#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
				float4 ase_lightColor = 0;
				#else //aselc
				float4 ase_lightColor = _LightColor0;
				#endif //aselc
				float3 clampResult6_g375 = clamp( ( lerp(temp_output_15_0_g375,( temp_output_15_0_g375 * ase_lightColor.rgb ),_DirectionalLight) + lerp(float3( 0,0,0 ),(UNITY_LIGHTMODEL_AMBIENT).rgb,_Ambient) ) , float3( 0,0,0 ) , float3( 1,1,1 ) );
				float4 appendResult18_g375 = (float4(clampResult6_g375 , (temp_output_9_0_g375).a));
				#ifdef _LIGHT_ON
				float4 staticSwitch10_g375 = appendResult18_g375;
				#else
				float4 staticSwitch10_g375 = temp_output_9_0_g375;
				#endif
				float4 temp_output_14_0_g376 = staticSwitch10_g375;
				float temp_output_3_0_g376 = length( ( _SimulatedLightPosition - ase_worldPos ) );
				float clampResult8_g376 = clamp( ( min( temp_output_3_0_g376 , _SimulatedLightDistance ) / _SimulatedLightDistance ) , 0.0 , 1.0 );
				float2 appendResult10_g376 = (float2(0.0 , ( 1.0 - clampResult8_g376 )));
				float4 tex2DNode11_g376 = tex2D( _SimulatedLightRampTexture, appendResult10_g376 );
				float clampResult105_g376 = clamp( ( min( temp_output_3_0_g376 , _SimulatedLightDistance ) / _SimulatedLightDistance ) , 0.0 , 1.0 );
				float2 appendResult40_g376 = (float2(0.0 , clampResult105_g376));
				float4 lerpResult17_g376 = lerp( _CenterColor , lerp(temp_output_14_0_g376,_ExternalColor,_UseExternalColor) , clampResult105_g376);
				float4 temp_output_35_0_g376 = ( temp_output_14_0_g376 + lerp(tex2DNode11_g376,( tex2D( _GradientTextureWhite, appendResult40_g376 ) * lerp(lerpResult17_g376,( floor( ( lerpResult17_g376 * _Steps ) ) / ( _Steps - 1.0 ) ),_Posterize) ),_GradientRamp) );
				float4 lerpResult88_g376 = lerp( temp_output_14_0_g376 , temp_output_35_0_g376 , _AdditiveSimulatedLightLevel);
				float temp_output_84_0_g376 = (temp_output_14_0_g376).a;
				float4 appendResult85_g376 = (float4((lerpResult88_g376).rgb , temp_output_84_0_g376));
				float4 clampResult58_g376 = clamp( appendResult85_g376 , float4( 0,0,0,0 ) , float4( 1,1,1,1 ) );
				float4 appendResult83_g376 = (float4(lerp(( temp_output_14_0_g376 * lerp(tex2DNode11_g376,( tex2D( _GradientTextureWhite, appendResult40_g376 ) * lerp(lerpResult17_g376,( floor( ( lerpResult17_g376 * _Steps ) ) / ( _Steps - 1.0 ) ),_Posterize) ),_GradientRamp) ),clampResult58_g376,_AdditiveSimulatedLight).rgb , temp_output_84_0_g376));
				float4 lerpResult62_g376 = lerp( temp_output_14_0_g376 , appendResult83_g376 , _SimulatedLightLevel);
				float4 temp_output_20_0_g377 = lerp(temp_output_14_0_g376,lerpResult62_g376,_SimulatedLight);
				float3 temp_output_26_0_g377 = (temp_output_20_0_g377).rgb;
				float clampResult19_g377 = clamp( ( lerp(( ase_worldPos.y - _HeightFogStartPosition ),( i.ase_texcoord3.xyz.y - _HeightFogStartPosition ),_LocalHeightFog) / _FogHeight ) , 0.0 , 1.0 );
				float3 lerpResult21_g377 = lerp( (_HeightFogColor).rgb , temp_output_26_0_g377 , clampResult19_g377);
				float3 lerpResult23_g377 = lerp( temp_output_26_0_g377 , lerpResult21_g377 , _HeightFogDensity);
				float temp_output_29_0_g377 = (temp_output_20_0_g377).a;
				float lerpResult31_g377 = lerp( temp_output_29_0_g377 , _HeightFogDensity , ( 1.0 - clampResult19_g377 ));
				float4 appendResult28_g377 = (float4(lerpResult23_g377 , lerp(lerpResult31_g377,temp_output_29_0_g377,_UseAlphaValue)));
				#ifdef _HEIGHTFOG_ON
				float4 staticSwitch6_g377 = appendResult28_g377;
				#else
				float4 staticSwitch6_g377 = temp_output_20_0_g377;
				#endif
				float4 temp_output_1_0_g378 = staticSwitch6_g377;
				float temp_output_12_0_g378 = max( (float)0 , min( (float)1 , ( ( _DistanceFogLength - ( length( ( lerp(_WorldSpaceCameraPos,_WorldDistanceFogPosition,_WorldDistanceFog) - ase_worldPos ) ) * _DistanceFogDensity ) ) / ( _DistanceFogLength - _DistanceFogStartPosition ) ) ) );
				float3 lerpResult20_g378 = lerp( (temp_output_1_0_g378).rgb , (_DistanceFogColor).rgb , temp_output_12_0_g378);
				float temp_output_24_0_g378 = (temp_output_1_0_g378).a;
				float clampResult28_g378 = clamp( ( temp_output_24_0_g378 + temp_output_12_0_g378 ) , 0.0 , 1.0 );
				float lerpResult26_g378 = lerp( temp_output_24_0_g378 , clampResult28_g378 , temp_output_12_0_g378);
				float4 appendResult27_g378 = (float4(lerpResult20_g378 , lerp(lerpResult26_g378,temp_output_24_0_g378,_UseAlpha)));
				#ifdef _DISTANCEFOG_ON
				float4 staticSwitch29_g378 = appendResult27_g378;
				#else
				float4 staticSwitch29_g378 = temp_output_1_0_g378;
				#endif
				
				
				myColorVar = staticSwitch29_g378;

				#if defined(_ALPHATEST_ON)
					clip (myColorVar.a - _Cutoff);
				#endif

				return myColorVar;
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "CGFFlatLightingFourLightsMaterialEditor"
	
}