Shader "Unlit/CapyUnlit"
{
    Properties
    {
		_MainTex("Texture", 2D) = "white" {}
    }

    SubShader
	{
		Tags { "LightMode" = "ForwardBase" }

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#pragma multi_compile_fwdbase
			#include "AutoLight.cginc"

			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				LIGHTING_COORDS(0,1)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			v2f vert(appdata_t v)
		   {
				v2f o;
				o.uv = v.texcoord;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.vertex, _MainTex);
				TRANSFER_VERTEX_TO_FRAGMENT(o);

				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);

				 float attenuation = LIGHT_ATTENUATION(i);
				 return col * clamp(attenuation, 0, 1);
			}

			ENDCG
		}
	}

    Fallback "VertexLit"
}