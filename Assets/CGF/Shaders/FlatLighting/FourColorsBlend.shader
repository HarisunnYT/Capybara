///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 04/03/2019
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias Soriano
/// Description: Flat Lighting/Four Colors Blend shader that applies four colors based on the normals of the mesh with a blend mode.
///

// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D

Shader "CG Framework/Flat Lighting/Four Colors Blend"
{
	Properties
	{
		
		[Toggle(_USEALPHACLIP_ON)] _UseAlphaClip ("Use Alpha Clip", Float) = 0
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

		// Blend mode requirement
		[HideInInspector]_BlendMode ("Blend Mode", Float) = 0.0
		// end
		[HideInInspector]_ZWrite ("ZWrite", Float) = 0.0
	}
	
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" "LightMode"="ForwardBase" }
		LOD 100
		Cull Back
		ZWrite [_ZWrite]
		

		// Blend mode requirement
		GrabPass { }
		// end

		Pass
		{

			Blend SrcAlpha OneMinusSrcAlpha 
            ZWrite Off

			CGPROGRAM
			#pragma target 3.0 
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			// Blend mode requirement
			#include "Assets/CGF/Shaders/CGIncludes/BlendModes.cginc"
			#pragma multi_compile __ _BLENDMODE_DARKEN _BLENDMODE_MULTIPLY _BLENDMODE_COLORBURN _BLENDMODE_LINEARBURN _BLENDMODE_DARKERCOLOR _BLENDMODE_LIGHTEN _BLENDMODE_SCREEN _BLENDMODE_COLORDODGE _BLENDMODE_LINEARDODGE _BLENDMODE_LIGHTERCOLOR _BLENDMODE_OVERLAY _BLENDMODE_SOFTLIGHT _BLENDMODE_HARDLIGHT _BLENDMODE_VIVIDLIGHT _BLENDMODE_LINEARLIGHT _BLENDMODE_PINLIGHT _BLENDMODE_HARDMIX _BLENDMODE_DIFFERENCE _BLENDMODE_EXCLUSION _BLENDMODE_SUBTRACT _BLENDMODE_DIVIDE _BLENDMODE_HUE _BLENDMODE_SATURATION _BLENDMODE_COLOR _BLENDMODE_LUMINOSITY
			// end

			#pragma multi_compile __ _USEALPHACLIP_ON
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

				// Blend mode requirement
				float4 ase_texcoord6 : TEXCOORD6;
				// end
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

			// Blend mode requirement
			uniform sampler2D _GrabTexture;
			inline float4 NewComputeGrabScreenPos( float4 pos )
			{
				#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
				#else
				float scale = 1.0;
				#endif
				float4 o = pos;
				o.y = pos.w * 0.5f;
				o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
				return o;
			}
			// end
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.texcoord.xy = v.texcoord.xy;
				o.texcoord.zw = v.texcoord1.xy;

				// Blend mode requirement
				float4 clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(clipPos);
				o.ase_texcoord6 = screenPos;
				// end
				
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
				float2 uv0161_g338 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float cos155_g338 = cos( radians( _FrontGradientRotation ) );
				float sin155_g338 = sin( radians( _FrontGradientRotation ) );
				float2 rotator155_g338 = mul( uv0161_g338 - float2( 0.5,0.5 ) , float2x2( cos155_g338 , -sin155_g338 , sin155_g338 , cos155_g338 )) + float2( 0.5,0.5 );
				float2 break156_g338 = rotator155_g338;
				float clampResult73_g338 = clamp( ( lerp(lerp(break156_g338.x,break156_g338.y,_FrontGradientChangeDirection),( 1.0 - lerp(break156_g338.x,break156_g338.y,_FrontGradientChangeDirection) ),_FrontGradientRevert) + ( ( _FrontGradientCenter * 2.0 ) - 1.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult179_g338 = smoothstep( ( _FrontGradientCenter - _FrontGradientWidth ) , ( _FrontGradientCenter + _FrontGradientWidth ) , clampResult73_g338);
				float3 lerpResult82_g338 = lerp( (_FrontColor).rgb , (_FrontTopColor).rgb , smoothstepResult179_g338);
				float lerpResult74_g338 = lerp( (_FrontColor).a , _FrontTopColor.a , smoothstepResult179_g338);
				float4 appendResult76_g338 = (float4(lerpResult82_g338 , lerpResult74_g338));
				#ifdef _FRONTGRADIENT_ON
				float4 staticSwitch88_g338 = appendResult76_g338;
				#else
				float4 staticSwitch88_g338 = _FrontColor;
				#endif
				float3 ase_worldNormal = i.ase_texcoord1.xyz;
				float3 normalizeResult4_g338 = normalize( ase_worldNormal );
				float3 ase_worldPos = i.ase_texcoord2.xyz;
				float3 ase_worldViewDir = UnityWorldSpaceViewDir(ase_worldPos);
				ase_worldViewDir = normalize(ase_worldViewDir);
				float temp_output_8_0_g338 = (lerp(normalizeResult4_g338,( ase_worldViewDir * normalizeResult4_g338 ),_ViewDirection)).z;
				float3 appendResult14_g338 = (float3(0.0 , 0.0 , temp_output_8_0_g338));
				float dotResult17_g338 = dot( float3( 0,0,-1 ) , appendResult14_g338 );
				float clampResult21_g338 = clamp( dotResult17_g338 , -1.0 , 1.0 );
				float ifLocalVar34_g338 = 0;
				if( temp_output_8_0_g338 >= 0.0 )
				ifLocalVar34_g338 = (float)0;
				else
				ifLocalVar34_g338 = (float)1;
				float lerpResult40_g338 = lerp( 0.0 , ( 1.0 - ( degrees( acos( clampResult21_g338 ) ) / 90.0 ) ) , ifLocalVar34_g338);
				float2 uv_FrontTexture = i.texcoord.xy * _FrontTexture_ST.xy + _FrontTexture_ST.zw;
				float4 lerpResult68_g338 = lerp( float4( 1,1,1,1 ) , tex2D( _FrontTexture, uv_FrontTexture ) , _FrontTextureLevel);
				float2 uv_MainTex = i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 lerpResult165_g338 = lerp( float4( 1,1,1,1 ) , tex2D( _MainTex, uv_MainTex ) , _MainTextureLevel);
				float4 temp_output_49_0_g338 = ( staticSwitch88_g338 * lerpResult40_g338 * lerpResult68_g338 * lerpResult165_g338 );
				float cos154_g338 = cos( radians( _RightGradientRotation ) );
				float sin154_g338 = sin( radians( _RightGradientRotation ) );
				float2 rotator154_g338 = mul( uv0161_g338 - float2( 0.5,0.5 ) , float2x2( cos154_g338 , -sin154_g338 , sin154_g338 , cos154_g338 )) + float2( 0.5,0.5 );
				float2 break152_g338 = rotator154_g338;
				float clampResult118_g338 = clamp( ( lerp(lerp(break152_g338.x,break152_g338.y,_RightGradientChangeDirection),( 1.0 - lerp(break152_g338.x,break152_g338.y,_RightGradientChangeDirection) ),_RightGradientRevert) + ( ( _RightGradientCenter * 2.0 ) - 1.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult183_g338 = smoothstep( ( _RightGradientCenter - _RightGradientWidth ) , ( _RightGradientCenter + _RightGradientWidth ) , clampResult118_g338);
				float3 lerpResult113_g338 = lerp( (_RightColor).rgb , (_RightTopColor).rgb , smoothstepResult183_g338);
				float lerpResult123_g338 = lerp( (_RightColor).a , _RightTopColor.a , smoothstepResult183_g338);
				float4 appendResult122_g338 = (float4(lerpResult113_g338 , lerpResult123_g338));
				#ifdef _RIGHTGRADIENT_ON
				float4 staticSwitch129_g338 = appendResult122_g338;
				#else
				float4 staticSwitch129_g338 = _RightColor;
				#endif
				float temp_output_9_0_g338 = (lerp(normalizeResult4_g338,( ase_worldViewDir * normalizeResult4_g338 ),_ViewDirection)).x;
				float3 appendResult15_g338 = (float3(temp_output_9_0_g338 , 0.0 , 0.0));
				float dotResult16_g338 = dot( float3( 1,0,0 ) , appendResult15_g338 );
				float clampResult19_g338 = clamp( dotResult16_g338 , -1.0 , 1.0 );
				float ifLocalVar37_g338 = 0;
				if( temp_output_9_0_g338 <= 0.0 )
				ifLocalVar37_g338 = (float)0;
				else
				ifLocalVar37_g338 = (float)1;
				float lerpResult41_g338 = lerp( 0.0 , ( 1.0 - ( degrees( acos( clampResult19_g338 ) ) / 90.0 ) ) , ifLocalVar37_g338);
				float2 uv_RightTexture = i.texcoord.xy * _RightTexture_ST.xy + _RightTexture_ST.zw;
				float4 lerpResult110_g338 = lerp( float4( 1,1,1,1 ) , tex2D( _RightTexture, uv_RightTexture ) , _RightTextureLevel);
				float4 temp_output_52_0_g338 = ( staticSwitch129_g338 * lerpResult41_g338 * lerpResult110_g338 * lerpResult165_g338 );
				float cos157_g338 = cos( radians( _TopGradientRotation ) );
				float sin157_g338 = sin( radians( _TopGradientRotation ) );
				float2 rotator157_g338 = mul( uv0161_g338 - float2( 0.5,0.5 ) , float2x2( cos157_g338 , -sin157_g338 , sin157_g338 , cos157_g338 )) + float2( 0.5,0.5 );
				float2 break159_g338 = rotator157_g338;
				float clampResult103_g338 = clamp( ( lerp(lerp(break159_g338.x,break159_g338.y,_TopGradientChangeDirection),( 1.0 - lerp(break159_g338.x,break159_g338.y,_TopGradientChangeDirection) ),_TopGradientRevert) + ( ( _TopGradientCenter * 2.0 ) - 1.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult187_g338 = smoothstep( ( _TopGradientCenter - _TopGradientWidth ) , ( _TopGradientCenter + _TopGradientWidth ) , clampResult103_g338);
				float3 lerpResult92_g338 = lerp( (_TopColor).rgb , (_TopTopColor).rgb , smoothstepResult187_g338);
				float lerpResult108_g338 = lerp( (_TopColor).a , _TopTopColor.a , smoothstepResult187_g338);
				float4 appendResult107_g338 = (float4(lerpResult92_g338 , lerpResult108_g338));
				#ifdef _TOPGRADIENT_ON
				float4 staticSwitch98_g338 = appendResult107_g338;
				#else
				float4 staticSwitch98_g338 = _TopColor;
				#endif
				float temp_output_7_0_g338 = (lerp(normalizeResult4_g338,( ase_worldViewDir * normalizeResult4_g338 ),_ViewDirection)).y;
				float3 appendResult13_g338 = (float3(0.0 , temp_output_7_0_g338 , 0.0));
				float dotResult18_g338 = dot( float3( 0,1,0 ) , appendResult13_g338 );
				float clampResult20_g338 = clamp( dotResult18_g338 , -1.0 , 1.0 );
				float ifLocalVar39_g338 = 0;
				if( temp_output_7_0_g338 <= 0.0 )
				ifLocalVar39_g338 = (float)0;
				else
				ifLocalVar39_g338 = (float)1;
				float lerpResult42_g338 = lerp( 0.0 , ( 1.0 - ( degrees( acos( clampResult20_g338 ) ) / 90.0 ) ) , ifLocalVar39_g338);
				float2 uv_TopTexture = i.texcoord.xy * _TopTexture_ST.xy + _TopTexture_ST.zw;
				float4 lerpResult89_g338 = lerp( float4( 1,1,1,1 ) , tex2D( _TopTexture, uv_TopTexture ) , _TopTextureLevel);
				float4 temp_output_50_0_g338 = ( staticSwitch98_g338 * lerpResult42_g338 * lerpResult89_g338 * lerpResult165_g338 );
				float cos153_g338 = cos( radians( _RimGradientRotation ) );
				float sin153_g338 = sin( radians( _RimGradientRotation ) );
				float2 rotator153_g338 = mul( uv0161_g338 - float2( 0.5,0.5 ) , float2x2( cos153_g338 , -sin153_g338 , sin153_g338 , cos153_g338 )) + float2( 0.5,0.5 );
				float2 break158_g338 = rotator153_g338;
				float clampResult136_g338 = clamp( ( lerp(lerp(break158_g338.x,break158_g338.y,_RimGradientChangeDirection),( 1.0 - lerp(break158_g338.x,break158_g338.y,_RimGradientChangeDirection) ),_RimGradientRevert) + ( ( _RimGradientCenter * 2.0 ) - 1.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult191_g338 = smoothstep( ( _RimGradientCenter - _RimGradientWidth ) , ( _RimGradientCenter + _RimGradientWidth ) , clampResult136_g338);
				float3 lerpResult131_g338 = lerp( (_RimColor).rgb , (_RimTopColor).rgb , smoothstepResult191_g338);
				float lerpResult141_g338 = lerp( (_RimColor).a , _RimTopColor.a , smoothstepResult191_g338);
				float4 appendResult140_g338 = (float4(lerpResult131_g338 , lerpResult141_g338));
				#ifdef _RIMGRADIENT_ON
				float4 staticSwitch147_g338 = appendResult140_g338;
				#else
				float4 staticSwitch147_g338 = _RimColor;
				#endif
				float2 uv_RimTexture = i.texcoord.xy * _RimTexture_ST.xy + _RimTexture_ST.zw;
				float4 lerpResult149_g338 = lerp( float4( 1,1,1,1 ) , tex2D( _RimTexture, uv_RimTexture ) , _RimTextureLevel);
				float4 temp_output_51_0_g338 = ( staticSwitch147_g338 * ( 1.0 - ( lerpResult40_g338 + lerpResult41_g338 + lerpResult42_g338 ) ) * lerpResult149_g338 * lerpResult165_g338 );
				float4 appendResult65_g338 = (float4(( (temp_output_49_0_g338).rgb + (temp_output_52_0_g338).rgb + (temp_output_50_0_g338).rgb + (temp_output_51_0_g338).rgb ) , ( _OpacityLevel * ( (temp_output_49_0_g338).a + (temp_output_52_0_g338).a + (temp_output_50_0_g338).a + (temp_output_51_0_g338).a ) )));
				float4 temp_output_1_0_g339 = appendResult65_g338;
				float4 tex2DNode7_g339 = UNITY_SAMPLE_TEX2D( unity_Lightmap, (i.texcoord.zw*(unity_LightmapST).xy + (unity_LightmapST).zw) );
				float3 desaturateInitialColor8_g339 = tex2DNode7_g339.rgb;
				float desaturateDot8_g339 = dot( desaturateInitialColor8_g339, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar8_g339 = lerp( desaturateInitialColor8_g339, desaturateDot8_g339.xxx, 1.0 );
				float3 clampResult11_g339 = clamp( ( lerp((tex2DNode7_g339).rgb,desaturateVar8_g339,_DesaturateLightColor) + _ShadowLevel ) , float3( 0,0,0 ) , float3( 1,1,1 ) );
				float4 appendResult12_g339 = (float4(clampResult11_g339 , 1.0));
				float4 lerpResult14_g339 = lerp( float4( 1,1,1,1 ) , appendResult12_g339 , _LightmapLevel);
				float4 lerpResult16_g339 = lerp( _LightmapColor , float4( 1,1,1,1 ) , lerpResult14_g339);
				float4 appendResult29_g339 = (float4(desaturateVar8_g339 , 1.0));
				float4 lerpResult28_g339 = lerp( lerpResult16_g339 , temp_output_1_0_g339 , appendResult29_g339);
				#ifdef _LIGHTMAP_ON
				float4 staticSwitch32_g339 = lerp(lerpResult28_g339,( lerpResult16_g339 * temp_output_1_0_g339 ),_MultiplyLightmap);
				#else
				float4 staticSwitch32_g339 = temp_output_1_0_g339;
				#endif
				float4 temp_output_9_0_g340 = staticSwitch32_g339;
				float3 temp_output_15_0_g340 = (temp_output_9_0_g340).rgb;
				#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
				float4 ase_lightColor = 0;
				#else //aselc
				float4 ase_lightColor = _LightColor0;
				#endif //aselc
				float3 clampResult6_g340 = clamp( ( lerp(temp_output_15_0_g340,( temp_output_15_0_g340 * ase_lightColor.rgb ),_DirectionalLight) + lerp(float3( 0,0,0 ),(UNITY_LIGHTMODEL_AMBIENT).rgb,_Ambient) ) , float3( 0,0,0 ) , float3( 1,1,1 ) );
				float4 appendResult18_g340 = (float4(clampResult6_g340 , (temp_output_9_0_g340).a));
				#ifdef _LIGHT_ON
				float4 staticSwitch10_g340 = appendResult18_g340;
				#else
				float4 staticSwitch10_g340 = temp_output_9_0_g340;
				#endif
				float4 temp_output_14_0_g341 = staticSwitch10_g340;
				float temp_output_3_0_g341 = length( ( _SimulatedLightPosition - ase_worldPos ) );
				float clampResult8_g341 = clamp( ( min( temp_output_3_0_g341 , _SimulatedLightDistance ) / _SimulatedLightDistance ) , 0.0 , 1.0 );
				float2 appendResult10_g341 = (float2(0.0 , ( 1.0 - clampResult8_g341 )));
				float4 tex2DNode11_g341 = tex2D( _SimulatedLightRampTexture, appendResult10_g341 );
				float clampResult105_g341 = clamp( ( min( temp_output_3_0_g341 , _SimulatedLightDistance ) / _SimulatedLightDistance ) , 0.0 , 1.0 );
				float2 appendResult40_g341 = (float2(0.0 , clampResult105_g341));
				float4 lerpResult17_g341 = lerp( _CenterColor , lerp(temp_output_14_0_g341,_ExternalColor,_UseExternalColor) , clampResult105_g341);
				float4 temp_output_35_0_g341 = ( temp_output_14_0_g341 + lerp(tex2DNode11_g341,( tex2D( _GradientTextureWhite, appendResult40_g341 ) * lerp(lerpResult17_g341,( floor( ( lerpResult17_g341 * _Steps ) ) / ( _Steps - 1.0 ) ),_Posterize) ),_GradientRamp) );
				float4 lerpResult88_g341 = lerp( temp_output_14_0_g341 , temp_output_35_0_g341 , _AdditiveSimulatedLightLevel);
				float temp_output_84_0_g341 = (temp_output_14_0_g341).a;
				float4 appendResult85_g341 = (float4((lerpResult88_g341).rgb , temp_output_84_0_g341));
				float4 clampResult58_g341 = clamp( appendResult85_g341 , float4( 0,0,0,0 ) , float4( 1,1,1,1 ) );
				float4 appendResult83_g341 = (float4(lerp(( temp_output_14_0_g341 * lerp(tex2DNode11_g341,( tex2D( _GradientTextureWhite, appendResult40_g341 ) * lerp(lerpResult17_g341,( floor( ( lerpResult17_g341 * _Steps ) ) / ( _Steps - 1.0 ) ),_Posterize) ),_GradientRamp) ),clampResult58_g341,_AdditiveSimulatedLight).rgb , temp_output_84_0_g341));
				float4 lerpResult62_g341 = lerp( temp_output_14_0_g341 , appendResult83_g341 , _SimulatedLightLevel);
				float4 temp_output_20_0_g342 = lerp(temp_output_14_0_g341,lerpResult62_g341,_SimulatedLight);
				float3 temp_output_26_0_g342 = (temp_output_20_0_g342).rgb;
				float clampResult19_g342 = clamp( ( lerp(( ase_worldPos.y - _HeightFogStartPosition ),( i.ase_texcoord3.xyz.y - _HeightFogStartPosition ),_LocalHeightFog) / _FogHeight ) , 0.0 , 1.0 );
				float3 lerpResult21_g342 = lerp( (_HeightFogColor).rgb , temp_output_26_0_g342 , clampResult19_g342);
				float3 lerpResult23_g342 = lerp( temp_output_26_0_g342 , lerpResult21_g342 , _HeightFogDensity);
				float temp_output_29_0_g342 = (temp_output_20_0_g342).a;
				float lerpResult31_g342 = lerp( temp_output_29_0_g342 , _HeightFogDensity , ( 1.0 - clampResult19_g342 ));
				float4 appendResult28_g342 = (float4(lerpResult23_g342 , lerp(lerpResult31_g342,temp_output_29_0_g342,_UseAlphaValue)));
				#ifdef _HEIGHTFOG_ON
				float4 staticSwitch6_g342 = appendResult28_g342;
				#else
				float4 staticSwitch6_g342 = temp_output_20_0_g342;
				#endif
				float4 temp_output_1_0_g343 = staticSwitch6_g342;
				float temp_output_12_0_g343 = max( (float)0 , min( (float)1 , ( ( _DistanceFogLength - ( length( ( lerp(_WorldSpaceCameraPos,_WorldDistanceFogPosition,_WorldDistanceFog) - ase_worldPos ) ) * _DistanceFogDensity ) ) / ( _DistanceFogLength - _DistanceFogStartPosition ) ) ) );
				float3 lerpResult20_g343 = lerp( (temp_output_1_0_g343).rgb , (_DistanceFogColor).rgb , temp_output_12_0_g343);
				float temp_output_24_0_g343 = (temp_output_1_0_g343).a;
				float clampResult28_g343 = clamp( ( temp_output_24_0_g343 + temp_output_12_0_g343 ) , 0.0 , 1.0 );
				float lerpResult26_g343 = lerp( temp_output_24_0_g343 , clampResult28_g343 , temp_output_12_0_g343);
				float4 appendResult27_g343 = (float4(lerpResult20_g343 , lerp(lerpResult26_g343,temp_output_24_0_g343,_UseAlpha)));
				#ifdef _DISTANCEFOG_ON
				float4 staticSwitch29_g343 = appendResult27_g343;
				#else
				float4 staticSwitch29_g343 = temp_output_1_0_g343;
				#endif
				
				
				myColorVar = staticSwitch29_g343;

				#if defined(_USEALPHACLIP_ON)
					clip (myColorVar.a - _Cutoff);
				#endif

				// Blend mode requirement
				float4 screenPos = i.ase_texcoord6;
				float4 grabScreenPos = NewComputeGrabScreenPos( screenPos );
				float4 grabScreenColor = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD( grabScreenPos ) );

				return CalculateBlendMode(grabScreenColor, myColorVar);
				// end

			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "CGFFlatLightingFourColorsBlendMaterialEditor"
	
}