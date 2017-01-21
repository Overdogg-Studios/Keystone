Shader "Unlit/SkinTone"
{
	Properties
	{
		_Color("Skin", Color) = (0,0,0,1)
		_Boots("Boots", Color) = (0,0,0,1)
		_Hat("Hat",Color) = (0,0,0,1)
		_MainTex("Texture", 2D) = "white" {}
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
	float4 _Color;
	float4 _Boots;
	float4 _Hat;

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
		fixed4 toneColor = fixed4(_Color.x,_Color.y,_Color.z,_Color.w);
	if (texColor.a == 1 && texColor.r == 1 && texColor.g == 1 && texColor.b == 1) // SKIN
	{
		texColor.r = 1 - texColor.r;
		texColor.g = 1 - texColor.g;
		texColor.b = 1 - texColor.b;
		texColor += toneColor;
	}
	else if (texColor.a == 1 && ((texColor.r * 255 == 233 && texColor.g * 255 == 233 && texColor.b * 255 == 233) || (texColor.r * 255 == 232 && texColor.g * 255 == 232 && texColor.b * 255 == 232) || (texColor.r * 255 == 234 && texColor.g * 255 == 234 && texColor.b * 255 == 234)))// SKIN
	{
		texColor.r = 1 - texColor.r;
		texColor.g = 1 - texColor.g;
		texColor.b = 1 - texColor.b;
		texColor += toneColor;
	}
	else if (texColor.a == 1 && texColor.r * 255 == 212 && texColor.g * 255 == 212 && texColor.b * 255 == 212)// SKIN
	{
		texColor.r = 1 - texColor.r;
		texColor.g = 1 - texColor.g;
		texColor.b = 1 - texColor.b;
		texColor += toneColor;
	}
	else if (texColor.a == 1 && ((texColor.r * 255 == 191 && texColor.g * 255 == 191 && texColor.b * 255 == 191) || (texColor.r * 255 == 192 && texColor.g * 255 == 192 && texColor.b * 255 == 192) || (texColor.r * 255 == 193 && texColor.g * 255 == 193 && texColor.b * 255 == 193) || (texColor.r * 255 == 190 && texColor.g * 255 == 190 && texColor.b * 255 == 190)))// SKIN
	{
		texColor.r = 1 - texColor.r;
		texColor.g = 1 - texColor.g;
		texColor.b = 1 - texColor.b;
		texColor += toneColor;
	}
	else if (texColor.a == 1 && texColor.r * 255 == 170 && texColor.g * 255 == 170 && texColor.b * 255 == 170)// SKIN
	{
		texColor.r = 1 - texColor.r;
		texColor.g = 1 - texColor.g;
		texColor.b = 1 - texColor.b;
		texColor += toneColor;
	}
	else if (texColor.a == 1 && texColor.r == 1 && texColor.g == 0 && texColor.b == 0) // BOOTS
	{
		//texColor += _Boots + 1 - fixed4(texColor.r, texColor.r, texColor.r, 0);
		texColor = fixed4(1 - texColor.r,1 - texColor.r,1 - texColor.r, 1);
		texColor += _Boots;
	}
	else if (texColor.a == 1 && texColor.g == 0 && texColor.b == 0 && (texColor.r * 255 == 228 || texColor.r * 255 == 229 || texColor.r * 255 == 230)) // BOOTS
	{
		//texColor += _Boots + 1 - fixed4(texColor.r, texColor.r, texColor.r, 0);
		texColor = fixed4(1 - texColor.r, 1 - texColor.r, 1 - texColor.r, 1);
		texColor += _Boots;
	}
	else if (texColor.a == 1 && texColor.g == 0 && texColor.b == 0 && (texColor.r * 255 == 203 || texColor.r * 255 == 204 || texColor.r * 255 == 205)) // BOOTS
	{
		//texColor += _Boots + 1 - fixed4(texColor.r, texColor.r, texColor.r, 0);
		texColor = fixed4(1 - texColor.r, 1 - texColor.r, 1 - texColor.r, 1);
		texColor += _Boots;
	}
	else if (texColor.a == 1 && texColor.g == 0 && texColor.b == 0 && (texColor.r * 255 == 178 || texColor.r * 255 == 179 || texColor.r * 255 == 180)) // BOOTS
	{
		//texColor += _Boots + 1 - fixed4(texColor.r, texColor.r, texColor.r, 0);
		texColor = fixed4(1 - texColor.r, 1 - texColor.r, 1 - texColor.r, 1);
		texColor += _Boots;
	}
	else if (texColor.a == 1 && texColor.g == 0 && texColor.b == 0 && (texColor.r * 255 == 152 || texColor.r * 255 == 153 || texColor.r * 255 == 154)) // BOOTS
	{
		//texColor += _Boots + 1 - fixed4(texColor.r, texColor.r, texColor.r, 0);
		texColor = fixed4(1 - texColor.r, 1 - texColor.r, 1 - texColor.r, 1);
		texColor += _Boots;
	}
	else if (texColor.a == 1 && (texColor.r * 255 == 99 || texColor.r * 255 == 100 || texColor.r * 255 == 101) && (texColor.g * 255 == 66 || texColor.g * 255 == 67 || texColor.g * 255 == 68) && (texColor.b * 255 == 38 || texColor.b * 255 == 39 || texColor.b * 255 == 40))
	{
		texColor = fixed4(texColor.r - texColor.b,texColor.r - texColor.b, texColor.r - texColor.b, 1);
		texColor += _Hat;
	}
	else if (texColor.a == 1 && (texColor.r * 255 == 121 || texColor.r * 255 == 122 || texColor.r * 255 == 123) && (texColor.g * 255 == 80 || texColor.g * 255 == 81 || texColor.g * 255 == 82) && (texColor.b * 255 == 46 || texColor.b * 255 == 47 || texColor.b * 255 == 48))
	{
		texColor = fixed4(texColor.r - texColor.b, texColor.r - texColor.b, texColor.r - texColor.b, 1);
		texColor += _Hat;
	}
	else if (texColor.a == 1 && (texColor.r * 255 == 131 || texColor.r * 255 == 132 || texColor.r * 255 == 133) && (texColor.g * 255 == 87 || texColor.g * 255 == 88 || texColor.g * 255 == 89) && (texColor.b * 255 == 50 || texColor.b * 255 == 51 || texColor.b * 255 == 52))
	{
		texColor = fixed4(texColor.r - texColor.b, texColor.r - texColor.b, texColor.r - texColor.b, 1);
		texColor += _Hat;
	}
	else if (texColor.a == 1 && (texColor.r * 255 == 87 || texColor.r * 255 == 88 || texColor.r * 255 == 89) && (texColor.g * 255 == 57 || texColor.g * 255 == 58 || texColor.g * 255 == 59) && (texColor.b * 255 == 33 || texColor.b * 255 == 34 || texColor.b * 255 == 35))
	{
		texColor = fixed4(texColor.r - texColor.b, texColor.r - texColor.b, texColor.r - texColor.b, 1);
		texColor += _Hat;
	}
	else if (texColor.a == 1 && (texColor.r * 255 == 109 || texColor.r * 255 == 110 || texColor.r * 255 == 111) && (texColor.g * 255 == 72 || texColor.g * 255 == 73 || texColor.g * 255 == 74) && (texColor.b * 255 == 41 || texColor.b * 255 == 42 || texColor.b * 255 == 43))
	{
		texColor = fixed4(texColor.r - texColor.b, texColor.r - texColor.b, texColor.r - texColor.b, 1);
		texColor += _Hat;
	}
	else if (texColor.a == 1 && ((texColor.r * 255 == 91 && texColor.g * 255 == 185 && texColor.b * 255 == 151) || (texColor.r * 255 == 92 && texColor.g * 255 == 186 && texColor.b * 255 == 152) || (texColor.r * 255 == 90 && texColor.g * 255 == 184 && texColor.b * 255 == 150)))
	{
		discard;
	}
	else if (texColor.a == 0 && texColor.r == 1 && texColor.g == 1 && texColor.b == 1)
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