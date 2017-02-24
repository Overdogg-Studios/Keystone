Shader "Unlit/Table"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_TextureSize("Width",Float) = 0
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
		float4 _TextureSize;

		v2f vert(appdata IN)
		{
			v2f OUT;
			OUT.pos = mul(UNITY_MATRIX_MVP, IN.vertex);
			OUT.texcoord = IN.texcoord;
			return OUT;
		}
		fixed4 frag(v2f IN) : COLOR
		{
			fixed4 currentPixel = tex2D(_MainTex,IN.texcoord);

			fixed4 topMiddle = fixed4(1, 1, 1, 1);
			fixed4 left = fixed4(1, 1, 1, 1);
			fixed4 right = fixed4(1, 1, 1, 1);
			fixed4 bottomMiddle = fixed4(1, 1, 1, 1);

			float pixel = 1 / _TextureSize;

			fixed4 result = (1,1,1,1);

			if (IN.texcoord.y > 0)
			{
				topMiddle = tex2D(_MainTex, IN.texcoord + fixed2(0,pixel));
			}

			if (IN.texcoord.y < 1)
			{
				bottomMiddle = tex2D(_MainTex, IN.texcoord + fixed2(0, pixel));
			}
			if (IN.texcoord.x > 0)
			{
				left = tex2D(_MainTex, IN.texcoord + fixed2(-pixel, 0));
			}
			if (IN.texcoord.x < 1)
			{
				right = tex2D(_MainTex, IN.texcoord + fixed2(0, pixel));
			}

			if (topMiddle.a == 0 && left.a == 0 && right.a == 0 && bottomMiddle.a == 1)
			{
				result.a = 1;
				result.r = 0;
				result.g = 0;
				result.b = 0;
			}
			else if (left.a == 0 && right.a == 0 && bottomMiddle.a == 0 && topMiddle.a == 1)
			{
				result.a = 1;
				result.r = 0;
				result.g = 0;
				result.b = 0;
			}
			else if (right.a == 0 && bottomMiddle.a == 0 && topMiddle.a == 0 && left.a == 1)
			{
				result.a = 1;
				result.r = 0;
				result.g = 0;
				result.b = 0;
			}
			else if (bottomMiddle.a == 0 && topMiddle.a == 0 && left.a == 0 && right.a == 1)
			{
				result.a = 1;
				result.r = 0;
				result.g = 0;
				result.b = 0;
			}

			if (result.a == 1 && result.r == 1 && result.b == 1 && result.g == 1)
			{
				result.a = currentPixel.a;
				result.r = currentPixel.r;
				result.g = currentPixel.g;
				result.b = currentPixel.b;
			}

			return result;

	//return _Color;
		}
		ENDCG
		}
	}
}