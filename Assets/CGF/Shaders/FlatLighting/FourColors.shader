///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 04/03/2019
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias Soriano
/// Description: Flat Lighting/Four Colors shader that applies four colors based on the normals of the mesh.
///

// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D

Shader "CG Framework/Flat Lighting/Four Colors"
{
	Properties
	{
		
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5	

		_FrontColor("Front Color (RGBA)", Color) = (1,0,0,1)
		_RightColor("Right Color (RGBA)", Color) = (0,1,0,1)
		_TopColor("Top Color (RGBA)", Color) = (0,0,1,1)
		_RimColor("Rim Color (RGBA)", Color) = (1,0,1,1)
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
		[Toggle(_FRONTGRADIENT_ON)] _FrontGradient("Front Gradient", Float) = 0
		_FrontTopColor("Front Top Color (RGBA)", Color) = (0,0,0,1)
		_FrontGradientCenter("Front Gradient Center", Range( 0 , 1)) = 0.5
		_FrontGradientWidth("Front Gradient Width", Float) = 0.5
		[Toggle][Toggle]_FrontGradientRevert("Front Gradient Revert", Float) = 0
		[Toggle][Toggle]_FrontGradientChangeDirection("Front Gradient Change Direction", Float) = 1
		_FrontGradientRotation("Front Gradient Rotation", Range( 0 , 360)) = 0
		[Toggle(_RIGHTGRADIENT_ON)] _RightGradient("Right Gradient", Float) = 0
		_RightTopColor("Right Top Color (RGBA)", Color) = (0,0,0,1)
		_RightGradientCenter("Right Gradient Center", Range( 0 , 1)) = 0.5
		_RightGradientWidth("Right Gradient Width", Float) = 0.5
		[Toggle][Toggle]_RightGradientRevert("Right Gradient Revert", Float) = 0
		[Toggle][Toggle]_RightGradientChangeDirection("Right Gradient Change Direction", Float) = 1
		_RightGradientRotation("Right Gradient Rotation", Range( 0 , 360)) = 0
		[Toggle(_TOPGRADIENT_ON)] _TopGradient("Top Gradient", Float) = 0
		_TopTopColor("Top Top Color (RGBA)", Color) = (0,0,0,1)
		_TopGradientCenter("Top Gradient Center", Range( 0 , 1)) = 0.5
		_TopGradientWidth("Top Gradient Width", Float) = 0.5
		[Toggle][Toggle]_TopGradientRevert("Top Gradient Revert", Float) = 0
		[Toggle][Toggle]_TopGradientChangeDirection("Top Gradient Change Direction", Float) = 1
		_TopGradientRotation("Top Gradient Rotation", Range( 0 , 360)) = 0
		[Toggle(_RIMGRADIENT_ON)] _RimGradient("Rim Gradient", Float) = 0
		_RimTopColor("Rim Top Color (RGBA)", Color) = (0,0,0,1)
		_RimGradientCenter("Rim Gradient Center", Range( 0 , 1)) = 0.5
		_RimGradientWidth("Rim Gradient Width", Float) = 0.5
		[Toggle][Toggle]_RimGradientRevert("Rim Gradient Revert", Float) = 0
		[Toggle][Toggle]_RimGradientChangeDirection("Rim Gradient Change Direction", Float) = 1
		_RimGradientRotation("Rim Gradient Rotation", Range( 0 , 360)) = 0
		_OpacityLevel("Opacity Level", Range( 0 , 1)) = 1
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
			#pragma shader_feature _FRONTGRADIENT_ON
			#pragma shader_feature _RIGHTGRADIENT_ON
			#pragma shader_feature _TOPGRADIENT_ON
			#pragma shader_feature _RIMGRADIENT_ON



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
			uniform float4 _FrontColor;
			uniform float4 _FrontTopColor;
			uniform half _FrontGradientCenter;
			uniform half _FrontGradientWidth;
			uniform half _FrontGradientRevert;
			uniform half _FrontGradientChangeDirection;
			uniform half _FrontGradientRotation;
			uniform half _ViewDirection;
			uniform sampler2D _FrontTexture;
			uniform float4 _FrontTexture_ST;
			uniform half _FrontTextureLevel;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float _MainTextureLevel;
			uniform float4 _RightColor;
			uniform float4 _RightTopColor;
			uniform half _RightGradientCenter;
			uniform half _RightGradientWidth;
			uniform half _RightGradientRevert;
			uniform half _RightGradientChangeDirection;
			uniform half _RightGradientRotation;
			uniform sampler2D _RightTexture;
			uniform float4 _RightTexture_ST;
			uniform half _RightTextureLevel;
			uniform float4 _TopColor;
			uniform float4 _TopTopColor;
			uniform half _TopGradientCenter;
			uniform half _TopGradientWidth;
			uniform half _TopGradientRevert;
			uniform half _TopGradientChangeDirection;
			uniform half _TopGradientRotation;
			uniform sampler2D _TopTexture;
			uniform float4 _TopTexture_ST;
			uniform half _TopTextureLevel;
			uniform float4 _RimColor;
			uniform float4 _RimTopColor;
			uniform half _RimGradientCenter;
			uniform half _RimGradientWidth;
			uniform half _RimGradientRevert;
			uniform half _RimGradientChangeDirection;
			uniform half _RimGradientRotation;
			uniform sampler2D _RimTexture;
			uniform float4 _RimTexture_ST;
			uniform half _RimTextureLevel;
			uniform half _OpacityLevel;
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
				float2 uv0161_g370 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float cos155_g370 = cos( radians( _FrontGradientRotation ) );
				float sin155_g370 = sin( radians( _FrontGradientRotation ) );
				float2 rotator155_g370 = mul( uv0161_g370 - float2( 0.5,0.5 ) , float2x2( cos155_g370 , -sin155_g370 , sin155_g370 , cos155_g370 )) + float2( 0.5,0.5 );
				float2 break156_g370 = rotator155_g370;
				float clampResult73_g370 = clamp( ( lerp(lerp(break156_g370.x,break156_g370.y,_FrontGradientChangeDirection),( 1.0 - lerp(break156_g370.x,break156_g370.y,_FrontGradientChangeDirection) ),_FrontGradientRevert) + ( ( _FrontGradientCenter * 2.0 ) - 1.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult179_g370 = smoothstep( ( _FrontGradientCenter - _FrontGradientWidth ) , ( _FrontGradientCenter + _FrontGradientWidth ) , clampResult73_g370);
				float3 lerpResult82_g370 = lerp( (_FrontColor).rgb , (_FrontTopColor).rgb , smoothstepResult179_g370);
				float lerpResult74_g370 = lerp( (_FrontColor).a , _FrontTopColor.a , smoothstepResult179_g370);
				float4 appendResult76_g370 = (float4(lerpResult82_g370 , lerpResult74_g370));
				#ifdef _FRONTGRADIENT_ON
				float4 staticSwitch88_g370 = appendResult76_g370;
				#else
				float4 staticSwitch88_g370 = _FrontColor;
				#endif
				float3 ase_worldNormal = i.ase_texcoord1.xyz;
				float3 normalizeResult4_g370 = normalize( ase_worldNormal );
				float3 ase_worldPos = i.ase_texcoord2.xyz;
				float3 ase_worldViewDir = UnityWorldSpaceViewDir(ase_worldPos);
				ase_worldViewDir = normalize(ase_worldViewDir);
				float temp_output_8_0_g370 = (lerp(normalizeResult4_g370,( ase_worldViewDir * normalizeResult4_g370 ),_ViewDirection)).z;
				float3 appendResult14_g370 = (float3(0.0 , 0.0 , temp_output_8_0_g370));
				float dotResult17_g370 = dot( float3( 0,0,-1 ) , appendResult14_g370 );
				float clampResult21_g370 = clamp( dotResult17_g370 , -1.0 , 1.0 );
				float ifLocalVar34_g370 = 0;
				if( temp_output_8_0_g370 >= 0.0 )
				ifLocalVar34_g370 = (float)0;
				else
				ifLocalVar34_g370 = (float)1;
				float lerpResult40_g370 = lerp( 0.0 , ( 1.0 - ( degrees( acos( clampResult21_g370 ) ) / 90.0 ) ) , ifLocalVar34_g370);
				float2 uv_FrontTexture = i.texcoord.xy * _FrontTexture_ST.xy + _FrontTexture_ST.zw;
				float4 lerpResult68_g370 = lerp( float4( 1,1,1,1 ) , tex2D( _FrontTexture, uv_FrontTexture ) , _FrontTextureLevel);
				float2 uv_MainTex = i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 lerpResult165_g370 = lerp( float4( 1,1,1,1 ) , tex2D( _MainTex, uv_MainTex ) , _MainTextureLevel);
				float4 temp_output_49_0_g370 = ( staticSwitch88_g370 * lerpResult40_g370 * lerpResult68_g370 * lerpResult165_g370 );
				float cos154_g370 = cos( radians( _RightGradientRotation ) );
				float sin154_g370 = sin( radians( _RightGradientRotation ) );
				float2 rotator154_g370 = mul( uv0161_g370 - float2( 0.5,0.5 ) , float2x2( cos154_g370 , -sin154_g370 , sin154_g370 , cos154_g370 )) + float2( 0.5,0.5 );
				float2 break152_g370 = rotator154_g370;
				float clampResult118_g370 = clamp( ( lerp(lerp(break152_g370.x,break152_g370.y,_RightGradientChangeDirection),( 1.0 - lerp(break152_g370.x,break152_g370.y,_RightGradientChangeDirection) ),_RightGradientRevert) + ( ( _RightGradientCenter * 2.0 ) - 1.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult183_g370 = smoothstep( ( _RightGradientCenter - _RightGradientWidth ) , ( _RightGradientCenter + _RightGradientWidth ) , clampResult118_g370);
				float3 lerpResult113_g370 = lerp( (_RightColor).rgb , (_RightTopColor).rgb , smoothstepResult183_g370);
				float lerpResult123_g370 = lerp( (_RightColor).a , _RightTopColor.a , smoothstepResult183_g370);
				float4 appendResult122_g370 = (float4(lerpResult113_g370 , lerpResult123_g370));
				#ifdef _RIGHTGRADIENT_ON
				float4 staticSwitch129_g370 = appendResult122_g370;
				#else
				float4 staticSwitch129_g370 = _RightColor;
				#endif
				float temp_output_9_0_g370 = (lerp(normalizeResult4_g370,( ase_worldViewDir * normalizeResult4_g370 ),_ViewDirection)).x;
				float3 appendResult15_g370 = (float3(temp_output_9_0_g370 , 0.0 , 0.0));
				float dotResult16_g370 = dot( float3( 1,0,0 ) , appendResult15_g370 );
				float clampResult19_g370 = clamp( dotResult16_g370 , -1.0 , 1.0 );
				float ifLocalVar37_g370 = 0;
				if( temp_output_9_0_g370 <= 0.0 )
				ifLocalVar37_g370 = (float)0;
				else
				ifLocalVar37_g370 = (float)1;
				float lerpResult41_g370 = lerp( 0.0 , ( 1.0 - ( degrees( acos( clampResult19_g370 ) ) / 90.0 ) ) , ifLocalVar37_g370);
				float2 uv_RightTexture = i.texcoord.xy * _RightTexture_ST.xy + _RightTexture_ST.zw;
				float4 lerpResult110_g370 = lerp( float4( 1,1,1,1 ) , tex2D( _RightTexture, uv_RightTexture ) , _RightTextureLevel);
				float4 temp_output_52_0_g370 = ( staticSwitch129_g370 * lerpResult41_g370 * lerpResult110_g370 * lerpResult165_g370 );
				float cos157_g370 = cos( radians( _TopGradientRotation ) );
				float sin157_g370 = sin( radians( _TopGradientRotation ) );
				float2 rotator157_g370 = mul( uv0161_g370 - float2( 0.5,0.5 ) , float2x2( cos157_g370 , -sin157_g370 , sin157_g370 , cos157_g370 )) + float2( 0.5,0.5 );
				float2 break159_g370 = rotator157_g370;
				float clampResult103_g370 = clamp( ( lerp(lerp(break159_g370.x,break159_g370.y,_TopGradientChangeDirection),( 1.0 - lerp(break159_g370.x,break159_g370.y,_TopGradientChangeDirection) ),_TopGradientRevert) + ( ( _TopGradientCenter * 2.0 ) - 1.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult187_g370 = smoothstep( ( _TopGradientCenter - _TopGradientWidth ) , ( _TopGradientCenter + _TopGradientWidth ) , clampResult103_g370);
				float3 lerpResult92_g370 = lerp( (_TopColor).rgb , (_TopTopColor).rgb , smoothstepResult187_g370);
				float lerpResult108_g370 = lerp( (_TopColor).a , _TopTopColor.a , smoothstepResult187_g370);
				float4 appendResult107_g370 = (float4(lerpResult92_g370 , lerpResult108_g370));
				#ifdef _TOPGRADIENT_ON
				float4 staticSwitch98_g370 = appendResult107_g370;
				#else
				float4 staticSwitch98_g370 = _TopColor;
				#endif
				float temp_output_7_0_g370 = (lerp(normalizeResult4_g370,( ase_worldViewDir * normalizeResult4_g370 ),_ViewDirection)).y;
				float3 appendResult13_g370 = (float3(0.0 , temp_output_7_0_g370 , 0.0));
				float dotResult18_g370 = dot( float3( 0,1,0 ) , appendResult13_g370 );
				float clampResult20_g370 = clamp( dotResult18_g370 , -1.0 , 1.0 );
				float ifLocalVar39_g370 = 0;
				if( temp_output_7_0_g370 <= 0.0 )
				ifLocalVar39_g370 = (float)0;
				else
				ifLocalVar39_g370 = (float)1;
				float lerpResult42_g370 = lerp( 0.0 , ( 1.0 - ( degrees( acos( clampResult20_g370 ) ) / 90.0 ) ) , ifLocalVar39_g370);
				float2 uv_TopTexture = i.texcoord.xy * _TopTexture_ST.xy + _TopTexture_ST.zw;
				float4 lerpResult89_g370 = lerp( float4( 1,1,1,1 ) , tex2D( _TopTexture, uv_TopTexture ) , _TopTextureLevel);
				float4 temp_output_50_0_g370 = ( staticSwitch98_g370 * lerpResult42_g370 * lerpResult89_g370 * lerpResult165_g370 );
				float cos153_g370 = cos( radians( _RimGradientRotation ) );
				float sin153_g370 = sin( radians( _RimGradientRotation ) );
				float2 rotator153_g370 = mul( uv0161_g370 - float2( 0.5,0.5 ) , float2x2( cos153_g370 , -sin153_g370 , sin153_g370 , cos153_g370 )) + float2( 0.5,0.5 );
				float2 break158_g370 = rotator153_g370;
				float clampResult136_g370 = clamp( ( lerp(lerp(break158_g370.x,break158_g370.y,_RimGradientChangeDirection),( 1.0 - lerp(break158_g370.x,break158_g370.y,_RimGradientChangeDirection) ),_RimGradientRevert) + ( ( _RimGradientCenter * 2.0 ) - 1.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult191_g370 = smoothstep( ( _RimGradientCenter - _RimGradientWidth ) , ( _RimGradientCenter + _RimGradientWidth ) , clampResult136_g370);
				float3 lerpResult131_g370 = lerp( (_RimColor).rgb , (_RimTopColor).rgb , smoothstepResult191_g370);
				float lerpResult141_g370 = lerp( (_RimColor).a , _RimTopColor.a , smoothstepResult191_g370);
				float4 appendResult140_g370 = (float4(lerpResult131_g370 , lerpResult141_g370));
				#ifdef _RIMGRADIENT_ON
				float4 staticSwitch147_g370 = appendResult140_g370;
				#else
				float4 staticSwitch147_g370 = _RimColor;
				#endif
				float2 uv_RimTexture = i.texcoord.xy * _RimTexture_ST.xy + _RimTexture_ST.zw;
				float4 lerpResult149_g370 = lerp( float4( 1,1,1,1 ) , tex2D( _RimTexture, uv_RimTexture ) , _RimTextureLevel);
				float4 temp_output_51_0_g370 = ( staticSwitch147_g370 * ( 1.0 - ( lerpResult40_g370 + lerpResult41_g370 + lerpResult42_g370 ) ) * lerpResult149_g370 * lerpResult165_g370 );
				float4 appendResult65_g370 = (float4(( (temp_output_49_0_g370).rgb + (temp_output_52_0_g370).rgb + (temp_output_50_0_g370).rgb + (temp_output_51_0_g370).rgb ) , ( _OpacityLevel * ( (temp_output_49_0_g370).a + (temp_output_52_0_g370).a + (temp_output_50_0_g370).a + (temp_output_51_0_g370).a ) )));
				float4 temp_output_1_0_g371 = appendResult65_g370;
				float4 tex2DNode7_g371 = UNITY_SAMPLE_TEX2D( unity_Lightmap, (i.texcoord.zw*(unity_LightmapST).xy + (unity_LightmapST).zw) );
				float3 desaturateInitialColor8_g371 = tex2DNode7_g371.rgb;
				float desaturateDot8_g371 = dot( desaturateInitialColor8_g371, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar8_g371 = lerp( desaturateInitialColor8_g371, desaturateDot8_g371.xxx, 1.0 );
				float3 clampResult11_g371 = clamp( ( lerp((tex2DNode7_g371).rgb,desaturateVar8_g371,_DesaturateLightColor) + _ShadowLevel ) , float3( 0,0,0 ) , float3( 1,1,1 ) );
				float4 appendResult12_g371 = (float4(clampResult11_g371 , 1.0));
				float4 lerpResult14_g371 = lerp( float4( 1,1,1,1 ) , appendResult12_g371 , _LightmapLevel);
				float4 lerpResult16_g371 = lerp( _LightmapColor , float4( 1,1,1,1 ) , lerpResult14_g371);
				float4 appendResult29_g371 = (float4(desaturateVar8_g371 , 1.0));
				float4 lerpResult28_g371 = lerp( lerpResult16_g371 , temp_output_1_0_g371 , appendResult29_g371);
				#ifdef _LIGHTMAP_ON
				float4 staticSwitch32_g371 = lerp(lerpResult28_g371,( lerpResult16_g371 * temp_output_1_0_g371 ),_MultiplyLightmap);
				#else
				float4 staticSwitch32_g371 = temp_output_1_0_g371;
				#endif
				float4 temp_output_9_0_g372 = staticSwitch32_g371;
				float3 temp_output_15_0_g372 = (temp_output_9_0_g372).rgb;
				#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
				float4 ase_lightColor = 0;
				#else //aselc
				float4 ase_lightColor = _LightColor0;
				#endif //aselc
				float3 clampResult6_g372 = clamp( ( lerp(temp_output_15_0_g372,( temp_output_15_0_g372 * ase_lightColor.rgb ),_DirectionalLight) + lerp(float3( 0,0,0 ),(UNITY_LIGHTMODEL_AMBIENT).rgb,_Ambient) ) , float3( 0,0,0 ) , float3( 1,1,1 ) );
				float4 appendResult18_g372 = (float4(clampResult6_g372 , (temp_output_9_0_g372).a));
				#ifdef _LIGHT_ON
				float4 staticSwitch10_g372 = appendResult18_g372;
				#else
				float4 staticSwitch10_g372 = temp_output_9_0_g372;
				#endif
				float4 temp_output_14_0_g373 = staticSwitch10_g372;
				float temp_output_3_0_g373 = length( ( _SimulatedLightPosition - ase_worldPos ) );
				float clampResult8_g373 = clamp( ( min( temp_output_3_0_g373 , _SimulatedLightDistance ) / _SimulatedLightDistance ) , 0.0 , 1.0 );
				float2 appendResult10_g373 = (float2(0.0 , ( 1.0 - clampResult8_g373 )));
				float4 tex2DNode11_g373 = tex2D( _SimulatedLightRampTexture, appendResult10_g373 );
				float clampResult105_g373 = clamp( ( min( temp_output_3_0_g373 , _SimulatedLightDistance ) / _SimulatedLightDistance ) , 0.0 , 1.0 );
				float2 appendResult40_g373 = (float2(0.0 , clampResult105_g373));
				float4 lerpResult17_g373 = lerp( _CenterColor , lerp(temp_output_14_0_g373,_ExternalColor,_UseExternalColor) , clampResult105_g373);
				float4 temp_output_35_0_g373 = ( temp_output_14_0_g373 + lerp(tex2DNode11_g373,( tex2D( _GradientTextureWhite, appendResult40_g373 ) * lerp(lerpResult17_g373,( floor( ( lerpResult17_g373 * _Steps ) ) / ( _Steps - 1.0 ) ),_Posterize) ),_GradientRamp) );
				float4 lerpResult88_g373 = lerp( temp_output_14_0_g373 , temp_output_35_0_g373 , _AdditiveSimulatedLightLevel);
				float temp_output_84_0_g373 = (temp_output_14_0_g373).a;
				float4 appendResult85_g373 = (float4((lerpResult88_g373).rgb , temp_output_84_0_g373));
				float4 clampResult58_g373 = clamp( appendResult85_g373 , float4( 0,0,0,0 ) , float4( 1,1,1,1 ) );
				float4 appendResult83_g373 = (float4(lerp(( temp_output_14_0_g373 * lerp(tex2DNode11_g373,( tex2D( _GradientTextureWhite, appendResult40_g373 ) * lerp(lerpResult17_g373,( floor( ( lerpResult17_g373 * _Steps ) ) / ( _Steps - 1.0 ) ),_Posterize) ),_GradientRamp) ),clampResult58_g373,_AdditiveSimulatedLight).rgb , temp_output_84_0_g373));
				float4 lerpResult62_g373 = lerp( temp_output_14_0_g373 , appendResult83_g373 , _SimulatedLightLevel);
				float4 temp_output_20_0_g374 = lerp(temp_output_14_0_g373,lerpResult62_g373,_SimulatedLight);
				float3 temp_output_26_0_g374 = (temp_output_20_0_g374).rgb;
				float clampResult19_g374 = clamp( ( lerp(( ase_worldPos.y - _HeightFogStartPosition ),( i.ase_texcoord3.xyz.y - _HeightFogStartPosition ),_LocalHeightFog) / _FogHeight ) , 0.0 , 1.0 );
				float3 lerpResult21_g374 = lerp( (_HeightFogColor).rgb , temp_output_26_0_g374 , clampResult19_g374);
				float3 lerpResult23_g374 = lerp( temp_output_26_0_g374 , lerpResult21_g374 , _HeightFogDensity);
				float temp_output_29_0_g374 = (temp_output_20_0_g374).a;
				float lerpResult31_g374 = lerp( temp_output_29_0_g374 , _HeightFogDensity , ( 1.0 - clampResult19_g374 ));
				float4 appendResult28_g374 = (float4(lerpResult23_g374 , lerp(lerpResult31_g374,temp_output_29_0_g374,_UseAlphaValue)));
				#ifdef _HEIGHTFOG_ON
				float4 staticSwitch6_g374 = appendResult28_g374;
				#else
				float4 staticSwitch6_g374 = temp_output_20_0_g374;
				#endif
				float4 temp_output_1_0_g375 = staticSwitch6_g374;
				float temp_output_12_0_g375 = max( (float)0 , min( (float)1 , ( ( _DistanceFogLength - ( length( ( lerp(_WorldSpaceCameraPos,_WorldDistanceFogPosition,_WorldDistanceFog) - ase_worldPos ) ) * _DistanceFogDensity ) ) / ( _DistanceFogLength - _DistanceFogStartPosition ) ) ) );
				float3 lerpResult20_g375 = lerp( (temp_output_1_0_g375).rgb , (_DistanceFogColor).rgb , temp_output_12_0_g375);
				float temp_output_24_0_g375 = (temp_output_1_0_g375).a;
				float clampResult28_g375 = clamp( ( temp_output_24_0_g375 + temp_output_12_0_g375 ) , 0.0 , 1.0 );
				float lerpResult26_g375 = lerp( temp_output_24_0_g375 , clampResult28_g375 , temp_output_12_0_g375);
				float4 appendResult27_g375 = (float4(lerpResult20_g375 , lerp(lerpResult26_g375,temp_output_24_0_g375,_UseAlpha)));
				#ifdef _DISTANCEFOG_ON
				float4 staticSwitch29_g375 = appendResult27_g375;
				#else
				float4 staticSwitch29_g375 = temp_output_1_0_g375;
				#endif
				
				
				myColorVar = staticSwitch29_g375;

				#if defined(_ALPHATEST_ON)
					clip (myColorVar.a - _Cutoff);
				#endif

				return myColorVar;
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "CGFFlatLightingFourColorsMaterialEditor"
	
}