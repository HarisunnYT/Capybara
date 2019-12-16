///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 13/05/2019
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias Soriano
/// Description: Skybox/Two Colors shader that applies a two colors grdient on the skybox.
///

Shader "CG Framework/Skybox/Two Colors"
{
	Properties
	{

		_TopColor("Top Color (RGB)", Color) = (0.3882353,0.7960784,1,0)
		_BottomColor("Bottom Color (RGB)", Color) = (0.5735294,0.5735294,0.5735294,0)

		_Intensity("Intensity", Range( 0 , 3)) = 1
		_Exposure("Exposure", Range( 0 , 6)) = 1
		_RotationX("Rotation X", Range( 0 , 360)) = 0
		_RotationY("Rotation Y", Range( 0 , 360)) = 0
		
	}
	
	SubShader
	{
		Tags { "Queue"="Background" "RenderType"="Background" "IgnoreProjector"="True" }
		LOD 100
		Cull Off
		ZWrite Off
		

		Pass
		{

			CGPROGRAM
			#pragma target 3.0 
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			

			struct appdata
			{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
				
			};

			uniform float _RotationX;
			uniform float _RotationY;
			uniform half _Exposure;
			uniform half _Intensity;
			uniform half4 _BottomColor;
			uniform half4 _TopColor;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.texcoord.xy = v.texcoord.xy;
				o.texcoord.zw = v.texcoord1.xy;
				
				
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				fixed4 myColorVar;
				float temp_output_23_0 = radians( _RotationX );
				float temp_output_28_0 = radians( _RotationY );
				float4 appendResult30 = (float4(( sin( temp_output_23_0 ) * sin( temp_output_28_0 ) ) , cos( temp_output_23_0 ) , ( sin( temp_output_23_0 ) * cos( temp_output_28_0 ) ) , 0.0));
				float dotResult10 = dot( float4( i.texcoord.xy, 0.0 , 0.0 ) , appendResult30 );
				float4 lerpResult3 = lerp( _BottomColor , _TopColor , pow( ( ( dotResult10 * 0.5 ) + 0.5 ) , _Exposure ));
				
				
				myColorVar = ( lerpResult3 * _Intensity );

				return myColorVar;
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "CGFSkyboxTwoColorsMaterialEditor"
	
}