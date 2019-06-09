//by 李红伟

Shader "Hidden/MaskTexWipe"
{
	Properties
	{
		_MainTex("MainTex",2d)="white"{}
		_MaskTex ("MaskTex",2D)="white"{}
		_Value("Value",float)=0
	}
	SubShader
	{
		Tags {
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

			sampler2D _MainTex;
			sampler2D _MaskTex;
			uniform float _Value;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col;

				//获得当前画面
				fixed4 screen=tex2D(_MainTex,i.uv);

				//采样dissolve贴图
				float4 dissolve=tex2D(_MaskTex,i.uv);

				col=screen*step(_Value,dissolve.r);
				return col;
			}
			ENDCG
		}
	}
}
