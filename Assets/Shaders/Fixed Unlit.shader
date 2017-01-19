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
		Tags{ "LightMode" = "ForwardAdd" }
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

		fixed4 texColor = tex2D(_MainTex,IN.texcoord);
	if (texColor.a < 0.7 || (texColor.r == 91 / 255 && texColor.g == 185 / 255 && texColor.b == 151 / 255 && texColor.a == 1))
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