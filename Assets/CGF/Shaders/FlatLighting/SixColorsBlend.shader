///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 04/03/2019
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias Soriano
/// Description: Flat Lighting/Six Colors Blend shader that applies six colors based on the normals of the mesh with a blend mode.
///

// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D

Shader "CG Framework/Flat Lighting/Six Colors Blend"
{
	Properties
	{
		
		[Toggle(_USEALPHACLIP_ON)] _UseAlphaClip ("Use Alpha Clip", Float) = 0
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5	

		_FrontColor("Front Color (RGBA)", Color) = (1,0,0,1)
		_RightColor("Right Color (RGBA)", Color) = (0,1,0,1)
		_TopColor("Top Color (RGBA)", Color) = (0,0,1,1)
		_BackColor("Back Color (RGBA)", Color) = (1,1,0,1)
		_LeftColor("Left Color (RGBA)", Color) = (0,1,1,1)
		_BottomColor("Bottom Color (RGBA)", Color) = (0,0,1,1)
		_MainTex("Main Texture (RGBA)", 2D) = "white" {}
		_FrontTexture("Front Texture (RGBA)", 2D) = "white" {}
		_RightTexture("Right Texture (RGBA)", 2D) = "white" {}
		_TopTexture("Top Texture (RGBA)", 2D) = "white" {}
		_BackTexture("Back Texture (RGBA)", 2D) = "white" {}
		_LeftTexture("Left Texture (RGBA)", 2D) = "white" {}
		_BottomTexture("Bottom Texture (RGBA)", 2D) = "white" {}
		_MainTextureLevel("Main Texture Level", Range( 0 , 1)) = 1
		_FrontTextureLevel("Front Texture Level", Range( 0 , 1)) = 1
		_RightTextureLevel("Right Texture Level", Range( 0 , 1)) = 1
		_TopTextureLevel("Top Texture Level", Range( 0 , 1)) = 1
		_BackTextureLevel("Back Texture Level", Range( 0 , 1)) = 1
		_LeftTextureLevel("Left Texture Level", Range( 0 , 1)) = 1
		_BottomTextureLevel("Bottom Texture Level", Range( 0 , 1)) = 1
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
		[Toggle(_BACKGRADIENT_ON)] _BackGradient("Back Gradient", Float) = 0
		_BackTopColor("Back Top Color (RGBA)", Color) = (0,0,0,1)
		_BackGradientCenter("Back Gradient Center", Range( 0 , 1)) = 0.5
		_BackGradientWidth("Back Gradient Width", Float) = 0.5
		[Toggle][Toggle]_BackGradientRevert("Back Gradient Revert", Float) = 0
		[Toggle][Toggle]_BackGradientChangeDirection("Back Gradient Change Direction", Float) = 1
		_BackGradientRotation("Back Gradient Rotation", Range( 0 , 360)) = 0
		[Toggle(_LEFTGRADIENT_ON)] _LeftGradient("Left Gradient", Float) = 0
		_LeftTopColor("Left Top Color (RGBA)", Color) = (0,0,0,1)
		_LeftGradientCenter("Left Gradient Center", Range( 0 , 1)) = 0.5
		_LeftGradientWidth("Left Gradient Width", Float) = 0.5
		[Toggle][Toggle]_LeftGradientRevert("Left Gradient Revert", Float) = 0
		[Toggle][Toggle]_LeftGradientChangeDirection("Left Gradient Change Direction", Float) = 1
		_LeftGradientRotation("Left Gradient Rotation", Range( 0 , 360)) = 0
		[Toggle(_BOTTOMGRADIENT_ON)] _BottomGradient("Bottom Gradient", Float) = 0
		_BottomTopColor("Bottom Top Color (RGBA)", Color) = (0,0,0,1)
		_BottomGradientCenter("Bottom Gradient Center", Range( 0 , 1)) = 0.5
		_BottomGradientWidth("Bottom Gradient Width", Float) = 0.5
		[Toggle][Toggle]_BottomGradientRevert("Bottom Gradient Revert", Float) = 0
		[Toggle][Toggle]_BottomGradientChangeDirection("Bottom Gradient Change Direction", Float) = 1
		_BottomGradientRotation("Bottom Gradient Rotation", Range( 0 , 360)) = 0
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
		[Toggle(_LIGHTMAP_ON)] _Lightmap("Lightmap", Float) = 0
		_LightmapColor("Lightmap Color (RGB)", Color) = (0,0,0,1)
		_LightmapLevel("Lightmap Level", Range( 0 , 20)) = 5
		_ShadowLevel("Shadow Level", Range( -1 , 1)) = 0
		[Toggle]_MultiplyLightmap("Multiply Lightmap", Float) = 0
		[Toggle]_DesaturateLightColor("Desaturate Light Color", Float) = 1
		[HideInInspector]unity_Lightmap("unity_Lightmap", 2D) = "white" {}
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
			#pragma shader_feature _BACKGRADIENT_ON
			#pragma shader_feature _LEFTGRADIENT_ON
			#pragma shader_feature _BOTTOMGRADIENT_ON



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
			uniform half _MainTextureLevel;
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
			uniform float4 _BackColor;
			uniform float4 _BackTopColor;
			uniform half _BackGradientCenter;
			uniform half _BackGradientWidth;
			uniform half _BackGradientRevert;
			uniform half _BackGradientChangeDirection;
			uniform half _BackGradientRotation;
			uniform sampler2D _BackTexture;
			uniform float4 _BackTexture_ST;
			uniform half _BackTextureLevel;
			uniform float4 _LeftColor;
			uniform float4 _LeftTopColor;
			uniform half _LeftGradientCenter;
			uniform half _LeftGradientWidth;
			uniform half _LeftGradientRevert;
			uniform half _LeftGradientChangeDirection;
			uniform half _LeftGradientRotation;
			uniform sampler2D _LeftTexture;
			uniform float4 _LeftTexture_ST;
			uniform half _LeftTextureLevel;
			uniform float4 _BottomColor;
			uniform float4 _BottomTopColor;
			uniform half _BottomGradientCenter;
			uniform half _BottomGradientWidth;
			uniform half _BottomGradientRevert;
			uniform half _BottomGradientChangeDirection;
			uniform half _BottomGradientRotation;
			uniform sampler2D _BottomTexture;
			uniform float4 _BottomTexture_ST;
			uniform half _BottomTextureLevel;
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
				float2 uv0218_g344 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float cos217_g344 = cos( radians( _FrontGradientRotation ) );
				float sin217_g344 = sin( radians( _FrontGradientRotation ) );
				float2 rotator217_g344 = mul( uv0218_g344 - float2( 0.5,0.5 ) , float2x2( cos217_g344 , -sin217_g344 , sin217_g344 , cos217_g344 )) + float2( 0.5,0.5 );
				float2 break213_g344 = rotator217_g344;
				float clampResult120_g344 = clamp( ( lerp(lerp(break213_g344.x,break213_g344.y,_FrontGradientChangeDirection),( 1.0 - lerp(break213_g344.x,break213_g344.y,_FrontGradientChangeDirection) ),_FrontGradientRevert) + ( ( _FrontGradientCenter * 2.0 ) - 1.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult233_g344 = smoothstep( ( _FrontGradientCenter - _FrontGradientWidth ) , ( _FrontGradientCenter + _FrontGradientWidth ) , clampResult120_g344);
				float3 lerpResult117_g344 = lerp( (_FrontColor).rgb , (_FrontTopColor).rgb , smoothstepResult233_g344);
				float lerpResult108_g344 = lerp( (_FrontColor).a , _FrontTopColor.a , smoothstepResult233_g344);
				float4 appendResult114_g344 = (float4(lerpResult117_g344 , lerpResult108_g344));
				#ifdef _FRONTGRADIENT_ON
				float4 staticSwitch116_g344 = appendResult114_g344;
				#else
				float4 staticSwitch116_g344 = _FrontColor;
				#endif
				float3 ase_worldNormal = i.ase_texcoord1.xyz;
				float3 normalizeResult4_g344 = normalize( ase_worldNormal );
				float3 ase_worldPos = i.ase_texcoord2.xyz;
				float3 ase_worldViewDir = UnityWorldSpaceViewDir(ase_worldPos);
				ase_worldViewDir = normalize(ase_worldViewDir);
				float temp_output_8_0_g344 = (lerp(normalizeResult4_g344,( ase_worldViewDir * normalizeResult4_g344 ),_ViewDirection)).z;
				float3 appendResult14_g344 = (float3(0.0 , 0.0 , temp_output_8_0_g344));
				float dotResult17_g344 = dot( float3( 0,0,-1 ) , appendResult14_g344 );
				float clampResult21_g344 = clamp( dotResult17_g344 , -1.0 , 1.0 );
				float temp_output_36_0_g344 = ( 1.0 - ( degrees( acos( clampResult21_g344 ) ) / 90.0 ) );
				float ifLocalVar34_g344 = 0;
				if( temp_output_8_0_g344 >= 0.0 )
				ifLocalVar34_g344 = (float)0;
				else
				ifLocalVar34_g344 = (float)1;
				float lerpResult40_g344 = lerp( 0.0 , temp_output_36_0_g344 , ifLocalVar34_g344);
				float2 uv_FrontTexture = i.texcoord.xy * _FrontTexture_ST.xy + _FrontTexture_ST.zw;
				float4 lerpResult89_g344 = lerp( float4( 1,1,1,1 ) , tex2D( _FrontTexture, uv_FrontTexture ) , _FrontTextureLevel);
				float2 uv_MainTex = i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 lerpResult227_g344 = lerp( float4( 1,1,1,1 ) , tex2D( _MainTex, uv_MainTex ) , _MainTextureLevel);
				float4 temp_output_49_0_g344 = ( staticSwitch116_g344 * lerpResult40_g344 * lerpResult89_g344 * lerpResult227_g344 );
				float cos216_g344 = cos( radians( _RightGradientRotation ) );
				float sin216_g344 = sin( radians( _RightGradientRotation ) );
				float2 rotator216_g344 = mul( uv0218_g344 - float2( 0.5,0.5 ) , float2x2( cos216_g344 , -sin216_g344 , sin216_g344 , cos216_g344 )) + float2( 0.5,0.5 );
				float2 break219_g344 = rotator216_g344;
				float clampResult137_g344 = clamp( ( lerp(lerp(break219_g344.x,break219_g344.y,_RightGradientChangeDirection),( 1.0 - lerp(break219_g344.x,break219_g344.y,_RightGradientChangeDirection) ),_RightGradientRevert) + ( ( _RightGradientCenter * 2.0 ) - 1.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult237_g344 = smoothstep( ( _RightGradientCenter - _RightGradientWidth ) , ( _RightGradientCenter + _RightGradientWidth ) , clampResult137_g344);
				float3 lerpResult134_g344 = lerp( (_RightColor).rgb , (_RightTopColor).rgb , smoothstepResult237_g344);
				float lerpResult126_g344 = lerp( (_RightColor).a , _RightTopColor.a , smoothstepResult237_g344);
				float4 appendResult131_g344 = (float4(lerpResult134_g344 , lerpResult126_g344));
				#ifdef _RIGHTGRADIENT_ON
				float4 staticSwitch133_g344 = appendResult131_g344;
				#else
				float4 staticSwitch133_g344 = _RightColor;
				#endif
				float temp_output_9_0_g344 = (lerp(normalizeResult4_g344,( ase_worldViewDir * normalizeResult4_g344 ),_ViewDirection)).x;
				float3 appendResult15_g344 = (float3(temp_output_9_0_g344 , 0.0 , 0.0));
				float dotResult16_g344 = dot( float3( 1,0,0 ) , appendResult15_g344 );
				float clampResult19_g344 = clamp( dotResult16_g344 , -1.0 , 1.0 );
				float temp_output_35_0_g344 = ( 1.0 - ( degrees( acos( clampResult19_g344 ) ) / 90.0 ) );
				float ifLocalVar37_g344 = 0;
				if( temp_output_9_0_g344 <= 0.0 )
				ifLocalVar37_g344 = (float)0;
				else
				ifLocalVar37_g344 = (float)1;
				float lerpResult41_g344 = lerp( 0.0 , temp_output_35_0_g344 , ifLocalVar37_g344);
				float2 uv_RightTexture = i.texcoord.xy * _RightTexture_ST.xy + _RightTexture_ST.zw;
				float4 lerpResult92_g344 = lerp( float4( 1,1,1,1 ) , tex2D( _RightTexture, uv_RightTexture ) , _RightTextureLevel);
				float4 temp_output_52_0_g344 = ( staticSwitch133_g344 * lerpResult41_g344 * lerpResult92_g344 * lerpResult227_g344 );
				float cos220_g344 = cos( radians( _TopGradientRotation ) );
				float sin220_g344 = sin( radians( _TopGradientRotation ) );
				float2 rotator220_g344 = mul( uv0218_g344 - float2( 0.5,0.5 ) , float2x2( cos220_g344 , -sin220_g344 , sin220_g344 , cos220_g344 )) + float2( 0.5,0.5 );
				float2 break225_g344 = rotator220_g344;
				float clampResult154_g344 = clamp( ( lerp(lerp(break225_g344.x,break225_g344.y,_TopGradientChangeDirection),( 1.0 - lerp(break225_g344.x,break225_g344.y,_TopGradientChangeDirection) ),_TopGradientRevert) + ( ( _TopGradientCenter * 2.0 ) - 1.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult241_g344 = smoothstep( ( _TopGradientCenter - _TopGradientWidth ) , ( _TopGradientCenter + _TopGradientWidth ) , clampResult154_g344);
				float3 lerpResult151_g344 = lerp( (_TopColor).rgb , (_TopTopColor).rgb , smoothstepResult241_g344);
				float lerpResult143_g344 = lerp( (_TopColor).a , _TopTopColor.a , smoothstepResult241_g344);
				float4 appendResult148_g344 = (float4(lerpResult151_g344 , lerpResult143_g344));
				#ifdef _TOPGRADIENT_ON
				float4 staticSwitch150_g344 = appendResult148_g344;
				#else
				float4 staticSwitch150_g344 = _TopColor;
				#endif
				float temp_output_7_0_g344 = (lerp(normalizeResult4_g344,( ase_worldViewDir * normalizeResult4_g344 ),_ViewDirection)).y;
				float3 appendResult13_g344 = (float3(0.0 , temp_output_7_0_g344 , 0.0));
				float dotResult18_g344 = dot( float3( 0,1,0 ) , appendResult13_g344 );
				float clampResult20_g344 = clamp( dotResult18_g344 , -1.0 , 1.0 );
				float temp_output_38_0_g344 = ( 1.0 - ( degrees( acos( clampResult20_g344 ) ) / 90.0 ) );
				float ifLocalVar39_g344 = 0;
				if( temp_output_7_0_g344 <= 0.0 )
				ifLocalVar39_g344 = (float)0;
				else
				ifLocalVar39_g344 = (float)1;
				float lerpResult42_g344 = lerp( 0.0 , temp_output_38_0_g344 , ifLocalVar39_g344);
				float2 uv_TopTexture = i.texcoord.xy * _TopTexture_ST.xy + _TopTexture_ST.zw;
				float4 lerpResult95_g344 = lerp( float4( 1,1,1,1 ) , tex2D( _TopTexture, uv_TopTexture ) , _TopTextureLevel);
				float4 temp_output_50_0_g344 = ( staticSwitch150_g344 * lerpResult42_g344 * lerpResult95_g344 * lerpResult227_g344 );
				float cos224_g344 = cos( radians( _BackGradientRotation ) );
				float sin224_g344 = sin( radians( _BackGradientRotation ) );
				float2 rotator224_g344 = mul( uv0218_g344 - float2( 0.5,0.5 ) , float2x2( cos224_g344 , -sin224_g344 , sin224_g344 , cos224_g344 )) + float2( 0.5,0.5 );
				float2 break223_g344 = rotator224_g344;
				float clampResult171_g344 = clamp( ( lerp(lerp(break223_g344.x,break223_g344.y,_BackGradientChangeDirection),( 1.0 - lerp(break223_g344.x,break223_g344.y,_BackGradientChangeDirection) ),_BackGradientRevert) + ( ( _BackGradientCenter * 2.0 ) - 1.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult245_g344 = smoothstep( ( _BackGradientCenter - _BackGradientWidth ) , ( _BackGradientCenter + _BackGradientWidth ) , clampResult171_g344);
				float3 lerpResult168_g344 = lerp( (_BackColor).rgb , (_BackTopColor).rgb , smoothstepResult245_g344);
				float lerpResult160_g344 = lerp( (_BackColor).a , _BackTopColor.a , smoothstepResult245_g344);
				float4 appendResult165_g344 = (float4(lerpResult168_g344 , lerpResult160_g344));
				#ifdef _BACKGRADIENT_ON
				float4 staticSwitch167_g344 = appendResult165_g344;
				#else
				float4 staticSwitch167_g344 = _BackColor;
				#endif
				float lerpResult58_g344 = lerp( 0.0 , -temp_output_36_0_g344 , ( 1.0 - ifLocalVar34_g344 ));
				float2 uv_BackTexture = i.texcoord.xy * _BackTexture_ST.xy + _BackTexture_ST.zw;
				float4 lerpResult98_g344 = lerp( float4( 1,1,1,1 ) , tex2D( _BackTexture, uv_BackTexture ) , _BackTextureLevel);
				float4 temp_output_55_0_g344 = ( staticSwitch167_g344 * lerpResult58_g344 * lerpResult98_g344 * lerpResult227_g344 );
				float cos215_g344 = cos( radians( _LeftGradientRotation ) );
				float sin215_g344 = sin( radians( _LeftGradientRotation ) );
				float2 rotator215_g344 = mul( uv0218_g344 - float2( 0.5,0.5 ) , float2x2( cos215_g344 , -sin215_g344 , sin215_g344 , cos215_g344 )) + float2( 0.5,0.5 );
				float2 break214_g344 = rotator215_g344;
				float clampResult188_g344 = clamp( ( lerp(lerp(break214_g344.x,break214_g344.y,_LeftGradientChangeDirection),( 1.0 - lerp(break214_g344.x,break214_g344.y,_LeftGradientChangeDirection) ),_LeftGradientRevert) + ( ( _LeftGradientCenter * 2.0 ) - 1.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult249_g344 = smoothstep( ( _LeftGradientCenter - _LeftGradientWidth ) , ( _LeftGradientCenter + _LeftGradientWidth ) , clampResult188_g344);
				float3 lerpResult185_g344 = lerp( (_LeftColor).rgb , (_LeftTopColor).rgb , smoothstepResult249_g344);
				float lerpResult177_g344 = lerp( (_LeftColor).a , _LeftTopColor.a , smoothstepResult249_g344);
				float4 appendResult182_g344 = (float4(lerpResult185_g344 , lerpResult177_g344));
				#ifdef _LEFTGRADIENT_ON
				float4 staticSwitch184_g344 = appendResult182_g344;
				#else
				float4 staticSwitch184_g344 = _LeftColor;
				#endif
				float lerpResult61_g344 = lerp( 0.0 , -temp_output_35_0_g344 , ( 1.0 - ifLocalVar37_g344 ));
				float2 uv_LeftTexture = i.texcoord.xy * _LeftTexture_ST.xy + _LeftTexture_ST.zw;
				float4 lerpResult101_g344 = lerp( float4( 1,1,1,1 ) , tex2D( _LeftTexture, uv_LeftTexture ) , _LeftTextureLevel);
				float4 temp_output_53_0_g344 = ( staticSwitch184_g344 * lerpResult61_g344 * lerpResult101_g344 * lerpResult227_g344 );
				float cos222_g344 = cos( radians( _BottomGradientRotation ) );
				float sin222_g344 = sin( radians( _BottomGradientRotation ) );
				float2 rotator222_g344 = mul( uv0218_g344 - float2( 0.5,0.5 ) , float2x2( cos222_g344 , -sin222_g344 , sin222_g344 , cos222_g344 )) + float2( 0.5,0.5 );
				float2 break221_g344 = rotator222_g344;
				float clampResult205_g344 = clamp( ( lerp(lerp(break221_g344.x,break221_g344.y,_BottomGradientChangeDirection),( 1.0 - lerp(break221_g344.x,break221_g344.y,_BottomGradientChangeDirection) ),_BottomGradientRevert) + ( ( _BottomGradientCenter * 2.0 ) - 1.0 ) ) , 0.0 , 1.0 );
				float smoothstepResult253_g344 = smoothstep( ( _BottomGradientCenter - _BottomGradientWidth ) , ( _BottomGradientCenter + _BottomGradientWidth ) , clampResult205_g344);
				float3 lerpResult202_g344 = lerp( (_BottomColor).rgb , (_BottomTopColor).rgb , smoothstepResult253_g344);
				float lerpResult194_g344 = lerp( (_BottomColor).a , _BottomTopColor.a , smoothstepResult253_g344);
				float4 appendResult199_g344 = (float4(lerpResult202_g344 , lerpResult194_g344));
				#ifdef _BOTTOMGRADIENT_ON
				float4 staticSwitch201_g344 = appendResult199_g344;
				#else
				float4 staticSwitch201_g344 = _BottomColor;
				#endif
				float lerpResult57_g344 = lerp( 0.0 , -temp_output_38_0_g344 , ( 1.0 - ifLocalVar39_g344 ));
				float2 uv_BottomTexture = i.texcoord.xy * _BottomTexture_ST.xy + _BottomTexture_ST.zw;
				float4 lerpResult104_g344 = lerp( float4( 1,1,1,1 ) , tex2D( _BottomTexture, uv_BottomTexture ) , _BottomTextureLevel);
				float4 temp_output_54_0_g344 = ( staticSwitch201_g344 * lerpResult57_g344 * lerpResult104_g344 * lerpResult227_g344 );
				float4 appendResult80_g344 = (float4(( (temp_output_49_0_g344).rgb + (temp_output_52_0_g344).rgb + (temp_output_50_0_g344).rgb + (temp_output_55_0_g344).rgb + (temp_output_53_0_g344).rgb + (temp_output_54_0_g344).rgb ) , ( _OpacityLevel * ( (temp_output_49_0_g344).a + (temp_output_52_0_g344).a + (temp_output_50_0_g344).a + (temp_output_55_0_g344).a + (temp_output_53_0_g344).a + (temp_output_54_0_g344).a ) )));
				float4 temp_output_1_0_g345 = appendResult80_g344;
				float4 tex2DNode7_g345 = UNITY_SAMPLE_TEX2D( unity_Lightmap, (i.texcoord.zw*(unity_LightmapST).xy + (unity_LightmapST).zw) );
				float3 desaturateInitialColor8_g345 = tex2DNode7_g345.rgb;
				float desaturateDot8_g345 = dot( desaturateInitialColor8_g345, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar8_g345 = lerp( desaturateInitialColor8_g345, desaturateDot8_g345.xxx, 1.0 );
				float3 clampResult11_g345 = clamp( ( lerp((tex2DNode7_g345).rgb,desaturateVar8_g345,_DesaturateLightColor) + _ShadowLevel ) , float3( 0,0,0 ) , float3( 1,1,1 ) );
				float4 appendResult12_g345 = (float4(clampResult11_g345 , 1.0));
				float4 lerpResult14_g345 = lerp( float4( 1,1,1,1 ) , appendResult12_g345 , _LightmapLevel);
				float4 lerpResult16_g345 = lerp( _LightmapColor , float4( 1,1,1,1 ) , lerpResult14_g345);
				float4 appendResult29_g345 = (float4(desaturateVar8_g345 , 1.0));
				float4 lerpResult28_g345 = lerp( lerpResult16_g345 , temp_output_1_0_g345 , appendResult29_g345);
				#ifdef _LIGHTMAP_ON
				float4 staticSwitch32_g345 = lerp(lerpResult28_g345,( lerpResult16_g345 * temp_output_1_0_g345 ),_MultiplyLightmap);
				#else
				float4 staticSwitch32_g345 = temp_output_1_0_g345;
				#endif
				float4 temp_output_9_0_g346 = staticSwitch32_g345;
				float3 temp_output_15_0_g346 = (temp_output_9_0_g346).rgb;
				#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
				float4 ase_lightColor = 0;
				#else //aselc
				float4 ase_lightColor = _LightColor0;
				#endif //aselc
				float3 clampResult6_g346 = clamp( ( lerp(temp_output_15_0_g346,( temp_output_15_0_g346 * ase_lightColor.rgb ),_DirectionalLight) + lerp(float3( 0,0,0 ),(UNITY_LIGHTMODEL_AMBIENT).rgb,_Ambient) ) , float3( 0,0,0 ) , float3( 1,1,1 ) );
				float4 appendResult18_g346 = (float4(clampResult6_g346 , (temp_output_9_0_g346).a));
				#ifdef _LIGHT_ON
				float4 staticSwitch10_g346 = appendResult18_g346;
				#else
				float4 staticSwitch10_g346 = temp_output_9_0_g346;
				#endif
				float4 temp_output_14_0_g341 = staticSwitch10_g346;
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
	CustomEditor "CGFFlatLightingSixColorsBlendMaterialEditor"
	
}