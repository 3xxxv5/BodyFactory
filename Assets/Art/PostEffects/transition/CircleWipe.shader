//by 李红伟

Shader "Hidden/CircleWipe"
{
	Properties
	{
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

			sampler2D _MainTex;
			uniform float _Value;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 c;

				//获得当前画面
				fixed4 screen=tex2D(_MainTex,i.uv);

//				fixed2 uv2=(i.uv*2-1)*_ScreenParams.w;

				//生成不变形的圆形uv
				fixed2 uv2=(i.uv*_ScreenParams-0.5*_ScreenParams)/_ScreenParams.y;

				//_Value*2表示放大2倍，为了使脚本上的调节从0-1
				_Value*=2;
				fixed4 mask=length(uv2);
				mask=1-step(_Value,mask);

				c=screen*mask;
				return c;
			}
			ENDCG
		}
	}
}
