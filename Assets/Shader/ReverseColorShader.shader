// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ShaderLearning"
{
	Properties {
		_MainTex("Main Texture", 2D) = "white" { }
	}
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
		}
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			zwrite off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			//all properties should be predefined in CGPROGRAM
			//sampler 2d is a glsl type to store a 2d texture

			struct appdata
			//the data you get from the program(or cpu) part
			{
				float4 vertex : POSITION; //vertex position
				float2 uv : TEXCOORD0; //texture position
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD1;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				//world space position to clipping space position (naturally stretched to screen size).
				o.uv = v.uv;
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				float4 color = tex2D(_MainTex, i.uv);
				//pick the color of the current uv pixel.
				color *= float4(i.uv.r, i.uv.g, 1, 1);
				return color;
			}
			ENDCG
		}
	}
}