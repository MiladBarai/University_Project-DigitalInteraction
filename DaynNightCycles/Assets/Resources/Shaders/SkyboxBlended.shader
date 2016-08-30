Shader "Skybox/Blended 6 Sided" 
{
	Properties
	{
		
		_Blend("Blend", Range(0.0, 1.0)) = 0.5		
		_Rotation("Rotation", Range(0, 360)) = 0
		[NoScaleOffset] _FrontTex("Front [+Z] (1:st)", 2D) = "white" {}
		[NoScaleOffset] _BackTex("Back [-Z] (1:st)", 2D) = "white" {}
		[NoScaleOffset] _LeftTex("Left [+X] (1:st)", 2D) = "white" {}
		[NoScaleOffset] _RightTex("Right [-X] (1:st)", 2D) = "white" {}
		[NoScaleOffset] _UpTex("Up [+Y] (1:st)", 2D) = "white" {}
		[NoScaleOffset] _DownTex("Down [-Y] (1:st)", 2D) = "white" {}
		_Tint1("Tint Color (1:st)", Color) = (.5, .5, .5, .5)
		[Gamma] _Exposure1("Exposure (1:st)", Range(0, 8)) = 1.0
		[NoScaleOffset] _FrontTex2("Front [+Z] (2:nd)", 2D) = "white" {}
		[NoScaleOffset] _BackTex2("Back [-Z] (2:nd)", 2D) = "white" {}
		[NoScaleOffset] _LeftTex2("Left [+X] (2:nd)", 2D) = "white" {}
		[NoScaleOffset] _RightTex2("Right [-X] (2:nd)", 2D) = "white" {}
		[NoScaleOffset] _UpTex2("Up [+Y] (2:nd)", 2D) = "white" {}
		[NoScaleOffset] _DownTex2("Down [-Y] (2:nd)", 2D) = "white" {}
		_Tint2("Tint Color (2:nd)", Color) = (.5, .5, .5, .5)
		[Gamma] _Exposure2("Exposure (2:nd)", Range(0, 8)) = 1.0
	}

	SubShader
	{
		Tags{ "Queue" = "Background" "RenderType" = "Background" }
		Cull Off
		ZWrite Off
		Fog{ Mode Off }
		Lighting Off
		//Color[_Tint] what is dis??

		CGINCLUDE
		#include "UnityCG.cginc"

		half4 _Tint1;
		half _Exposure1;
		half4 _Tint2;
		half _Exposure2;
		
		float _Rotation;
		half _Blend;

		float4 RotateAroundYInDegrees(float4 vertex, float degrees)
		{
			float alpha = degrees * UNITY_PI / 180.0;
			float sina, cosa;
			sincos(alpha, sina, cosa);
			float2x2 m = float2x2(cosa, -sina, sina, cosa);
			return float4(mul(m, vertex.xz), vertex.yw).xzyw;
		}

		struct appdata_t {
			float4 vertex : POSITION;
			float2 texcoord : TEXCOORD0;
		};
		struct v2f {
			float4 vertex : SV_POSITION;
			float2 texcoord : TEXCOORD0;
		};
		v2f vert(appdata_t v)
		{
			v2f o;
			o.vertex = mul(UNITY_MATRIX_MVP, RotateAroundYInDegrees(v.vertex, _Rotation));
			o.texcoord = v.texcoord;
			return o;
		}
		//Old
		/*
		half4 skybox_frag(v2f i, sampler2D smp, half4 smpDecode)
		{
			half4 tex = tex2D(smp, i.texcoord);
			half3 c = DecodeHDR(tex, smpDecode);
			c = c * _Tint.rgb * unity_ColorSpaceDouble.rgb;
			c *= _Exposure;
			return half4(c, 1);
		}*/
		//Rewritten:
		/*half4 skybox_frag(v2f i, sampler2D smp, sampler2D smp2)
		{
			//half4 tex = tex2D(smp, i.texcoord) * tex2D(smp2, i.texcoord); ACTUALLY BLENDS!!!
			half4 tex = tex2D(smp, i.texcoord) * half4(1, 1, 1, (1 - _Blend));
			half4 tex2 = tex2D(smp2, i.texcoord) * half4(1, 1, 1, _Blend);

			half3 c = half3(tex.x, tex.y, tex.z);
			half3 c2 = half3(tex2.x, tex2.y, tex2.z);

			half3 d = c * c2;

			d = d * _Tint.rgb * unity_ColorSpaceDouble.rgb;
			d *= _Exposure;
			return half4(d, 1);
		}*/
		//No consideration to exposure or tint
		fixed4 skybox_frag(v2f i, sampler2D smp1, sampler2D smp2)
		{
			//TEST (WORKS!)
			half4 tex1 = tex2D(smp1, i.texcoord);
			half3 c = half3(tex1.x, tex1.y, tex1.z);
			c = c * _Tint1.rgb * unity_ColorSpaceDouble.rgb;
			c *= _Exposure1;

			//Could make two separate tints and exposures...
			//For tex2 here obviously
			half4 tex2 = tex2D(smp2, i.texcoord);
			half3 c2 = half3(tex2.x, tex2.y, tex2.z);
			c2 = c2 * _Tint2.rgb * unity_ColorSpaceDouble.rgb;
			c2 *= _Exposure2;

			fixed4 texFinal = lerp(half4(c, 1), half4(c2, 1), _Blend);
			return texFinal;
		}
		ENDCG

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			sampler2D _FrontTex;
			sampler2D _FrontTex2;
			fixed4 frag(v2f i) : SV_Target{ return skybox_frag(i, _FrontTex, _FrontTex2); }
			ENDCG
			/*CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			sampler2D _FrontTex;
			sampler2D _FrontTex2;
			half4 frag(v2f i) : SV_Target{ return skybox_frag(i, _FrontTex, _FrontTex2); }
			ENDCG*/
			//SetTexture[_FrontTex]{ combine texture }
			//SetTexture[_FrontTex2]{ constantColor(0,0,0,[_Blend]) combine texture lerp(constant) previous }
		}
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			sampler2D _BackTex;
			sampler2D _BackTex2;
			fixed4 frag(v2f i) : SV_Target{ return skybox_frag(i, _BackTex, _BackTex2); }
			ENDCG
		}
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			sampler2D _LeftTex;
			sampler2D _LeftTex2;
			fixed4 frag(v2f i) : SV_Target{ return skybox_frag(i, _LeftTex, _LeftTex2); }
			ENDCG
		}
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			sampler2D _RightTex;
			sampler2D _RightTex2;
			fixed4 frag(v2f i) : SV_Target{ return skybox_frag(i, _RightTex, _RightTex2); }
			ENDCG
		}
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			sampler2D _UpTex;
			sampler2D _UpTex2;
			fixed4 frag(v2f i) : SV_Target{ return skybox_frag(i, _UpTex, _UpTex2); }
			ENDCG
		}
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			sampler2D _DownTex;
			sampler2D _DownTex2;
			fixed4 frag(v2f i) : SV_Target{ return skybox_frag(i, _DownTex, _DownTex2); }
			ENDCG
		}
	}

	Fallback "Skybox/6 Sided", 1
}
