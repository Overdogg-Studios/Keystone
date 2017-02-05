Shader "Unlit/Shield"
{
	Properties
	{
		_MainTex("ShieldTexture",2D) = "white" {}
		_ShieldWave("ShieldWave",2D) = "white" {}
		_Translation("Translation",Vector) = (0,0,0,0)
		_TextureSize("TextureSize",Float) = 32
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
		float2 texcoord0 : TEXCOORD0;
		float2 texcoord1 : TEXCOORD1;
	};
	struct v2f
	{
		float4 pos : SV_POSITION;
		float2 texcoord0 : TEXCOORD0;
		float2 texcoord1 : TEXCOORD1;
	};

	sampler2D _MainTex;
	sampler2D _ShieldWave;
	float4 _Translation;
	float _TextureSize;

	v2f vert(appdata IN)
	{
		v2f OUT;
		OUT.pos = mul(UNITY_MATRIX_MVP, IN.vertex);
		OUT.texcoord0 = IN.texcoord0;
		OUT.texcoord1 = IN.texcoord1;
		return OUT;
	}
	fixed4 frag(v2f IN) : COLOR
	{
		float2 tCoord1 = float2(IN.texcoord0.x + _Translation.x,IN.texcoord0.y + _Translation.y);
		float2 cTranslation = float2(.25f,.25f);

		if (tCoord1.x > 1)
		{
			tCoord1.x = tCoord1.x - 1;
		}

		if (tCoord1.x < 0)
		{
			tCoord1.x = tCoord1.x + 1;
		}

		if (tCoord1.y > 1)
		{
			tCoord1.y = tCoord1.y - 1;
		}

		if (tCoord1.y < 0)
		{
			tCoord1.y = tCoord1.y + 1;
		}
		fixed4 shieldMap = tex2D(_MainTex, tCoord1);
		fixed4 shieldWave = tex2D(_ShieldWave, IN.texcoord1);
		//fixed4 shieldWave = tex2D(_ShieldWave, IN.texcoord1 + cTranslation);

		fixed4 result;
		result.a = 0;
		result.r = shieldMap.r;
		result.g = shieldMap.g;
		result.b = shieldMap.b;
		
		if (shieldMap.a == 1 && shieldMap.r == 0 && shieldMap.g == 0 && shieldMap.b == 0)
		{
			discard;
		}
		//else if (shieldMap.a == 1 && shieldMap.r == 1 && shieldMap.g == 1 && shieldMap.b == 1)
		//{
			//discard;
		//}
		else
		{
			if (shieldWave.r == 0 && shieldWave.g == 0 && shieldWave.b == 0 && shieldWave.a == 1)
			{
				discard;
			}
		}
		//result.a = 1.0f;
		//result.r = 0.15f;
		if (shieldMap.a == 1 && (shieldMap.r * 255 == 170 || shieldMap.r * 255 == 169 || shieldMap.r * 255 == 171) && (shieldMap.g * 255 == 170 || shieldMap.g * 255 == 171 || shieldMap.g * 255 == 169) && (shieldMap.b * 255 == 170 || shieldMap.b * 255 == 169 || shieldMap.b * 255 == 171))
		{
			result.a = 0.03;
		}
		else if (shieldMap.a == 1 && (shieldMap.r * 255 == 189 || shieldMap.r * 255 == 188 || shieldMap.r * 255 == 190) && (shieldMap.g * 255 == 189 || shieldMap.g * 255 == 188 || shieldMap.g * 255 == 190) && (shieldMap.b * 255 == 189 || shieldMap.b * 255 == 188 || shieldMap.b * 255 == 190))
		{
			result.a = 0.04;
		}
		else if (shieldMap.a == 1 && (shieldMap.r * 255 == 124 || shieldMap.r * 255 == 123 || shieldMap.r * 255 == 125) && (shieldMap.g * 255 == 124 || shieldMap.g * 255 == 123 || shieldMap.g * 255 == 125) && (shieldMap.b * 255 == 124 || shieldMap.b * 255 == 123 || shieldMap.b * 255 == 125))
		{
			result.a = 0.02;
		}
		else if (shieldMap.a == 1 && (shieldMap.r * 255 == 91 || shieldMap.r * 255 == 90 || shieldMap.r * 255 == 92) && (shieldMap.g * 255 == 91 || shieldMap.g * 255 == 90 || shieldMap.g * 255 == 92) && (shieldMap.b * 255 == 91 || shieldMap.b * 255 == 90 || shieldMap.b * 255 == 92))
		{
			result.a = 0.01;
		}
		if (result.a == 1)
		{
			result.a = 0.1;
		}
		result.r = 1;
		return result;
	}
		ENDCG
	}
	}
}