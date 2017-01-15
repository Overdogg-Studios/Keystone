Shader "Unlit/NewUnlitShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
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

				fixed4 texColor = tex2D(_MainTex,IN.texcoord);
				if (texColor.a == 1 && texColor.r == 1 && texColor.g == 1 && texColor.b == 1)
				{
					texColor.r = 1;
					texColor.g = 0;
					texColor.b = 0;
				}
				else if (texColor.a == 1 && texColor.r * 255 == 233 && texColor.g * 255 == 233 && texColor.b * 255 == 233)
				{
					//texColor.r = 1;
					texColor.g = 1 - texColor.g;
					texColor.b = 1 - texColor.b;
				}
				else if (texColor.a == 1 && texColor.r * 255 == 212 && texColor.g * 255 == 212 && texColor.b * 255 == 212)
				{
					//texColor.r = 1;
					texColor.g = 1 - texColor.g;
					texColor.b = 1 - texColor.b;
				}
				else if (texColor.a == 1 && ((texColor.r * 255 == 191 && texColor.g * 255 == 191 && texColor.b * 255 == 191) || (texColor.r * 255 == 192 && texColor.g * 255 == 192 && texColor.b * 255 == 192) || (texColor.r * 255 == 193 && texColor.g * 255 == 193 && texColor.b * 255 == 193) || (texColor.r * 255 == 190 && texColor.g * 255 == 190 && texColor.b * 255 == 190)))
				{
					//texColor.r = 1;
					texColor.g = 1 - texColor.g;
					texColor.b = 1 - texColor.b;
				}
				else if (texColor.a == 1 && texColor.r * 255 == 170 && texColor.g * 255 == 170 && texColor.b * 255 == 170)
				{
					//texColor.r = 1;
					texColor.g = 1 - texColor.g;
					texColor.b = 1 - texColor.b;
				}
				else if (texColor.a == 1 && ((texColor.r * 255 == 91 && texColor.g * 255 == 185 && texColor.b * 255 == 151) || (texColor.r * 255 == 92 && texColor.g * 255 == 186 && texColor.b * 255 == 152) || (texColor.r * 255 == 90 && texColor.g * 255 == 184 && texColor.b * 255 == 150)))
				{
					discard;
				}


				return texColor;
			//return _Color;
			}
			ENDCG
		}
	}
}
