//by 李红伟

Shader "Hidden/ColorFadeInOut"
{
	Properties
	{
		_MainColor ("MainColor", color) = (0,0,0,1)
		_MainTex("MainTex",2d)="white"{}
		_Value("Value",float)=0
	}
	SubShader
	{
		Tags 
		{
            "IgnoreProjector"="True"
            "Queue"="Overlay"
            "RenderType"="Overlay"
        }
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 _MainColor;
			sampler2D _MainTex;
			uniform float _Value;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = _MainColor;

				fixed4 a=tex2D(_MainTex,i.uv);
				return lerp(a,_MainColor,_Value);
			}
			ENDCG
		}
	}
}
