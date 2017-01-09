Shader "TestShaders/Fixed Unlit"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white"{}
	}
		SubShader
	{
		Pass
		{
			Tags {"LightMode" = "ForwardAdd"}
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				#include "Lighting.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 texcoord : TEXCOORD0;
				};
				struct v2f
				{
					float4 pos : SV_POSITION;
					float2 texcoord : TEXCOORD0;
				};

				sampler2D _MainTex;

				v2f vert(appdata IN)
				{
					v2f OUT;
					OUT.pos = mul(UNITY_MATRIX_MVP, IN.vertex);
					OUT.texcoord = IN.texcoord;
					return OUT;
				}
				fixed4 frag(v2f IN) : COLOR
				{
					float brightness = 1;
					fixed3 diffuse = brightness * _LightColor0;
					fixed3 lightDirection = -normalize(_WorldSpaceLightPos0);
					fixed3 relfectedLightDirection = reflect(lightDirection, fixed3(0, 0, 1));

					fixed4 texColor = tex2D(_MainTex,IN.texcoord);
					if (texColor.a < 0.7)
					{
						discard;
					}
					return fixed4(diffuse,1) * texColor + fixed4(relfectedLightDirection,1);
					//return _Color;
				}
			ENDCG
		}
	}
}
