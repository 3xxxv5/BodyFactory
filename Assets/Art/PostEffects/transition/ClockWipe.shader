//by 李红伟

Shader "Hidden/ClockWipe"
{
	Properties
	{
		_MainTex("MainTex",2d)="white"{}
		_Value("Value",float)=0
//		[KeywordEnum(Clockwise, CounterClockwise)] _Enum ("Direction", Float) = 0
		//通过此值来判定是顺时针还是逆时针
		_Direction("Direction",float)=0
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
//			#pragma multi_compile _ENUM_CLOCKWISE _ENUM_COUNTERCLOCKWISE
			
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
			uniform float _Direction;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 c;

				fixed4 screen=tex2D(_MainTex,i.uv);

				float2 uv_center = (i.uv*2-1).rg;

				float polar;

//				#if _ENUM_CLOCKWISE
//					polar = 1-(atan2(uv_center.r,-uv_center.g)*0.159+0.5);
//				#elif _ENUM_COUNTERCLOCKWISE
//					polar = (atan2(uv_center.r,-uv_center.g)*0.159+0.5);
//				#endif

				//时针判断
				if (_Direction==0)
				{
					polar = 1-(atan2(uv_center.r,-uv_center.g)*0.159+0.5);
				}
				else if (_Direction==1)
				{
					polar = (atan2(uv_center.r,-uv_center.g)*0.159+0.5);
				}

                float4 mask = float4(polar,polar,polar,1);

				mask=step(_Value,mask);
				c=screen*mask;

				return c;
			}
			ENDCG
		}
	}
}
