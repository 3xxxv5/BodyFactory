//by 李红伟

Shader "Hidden/LinearWipe"
{
	Properties
	{
		_MainTex("MainTex",2d)="white"{}
		_Angle("_Angle",float)=0
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

//				#if UNITY_UV_STARTS_AT_TOP
//				o.uv.y=1-o.uv.y;
//				#endif

				return o;
			}

			sampler2D _MainTex;
			uniform float _Value;
			uniform float _Angle;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 c;

				fixed4 screen=tex2D(_MainTex,i.uv);

				fixed4 x=fixed4(i.uv.x,i.uv.x,i.uv.x,1);
				fixed4 y=fixed4(i.uv.y,i.uv.y,i.uv.y,1);

				fixed4 mask=lerp(x,y,_Angle);
				mask=step(_Value,mask);
				c=screen*mask;

				return c;
			}
			ENDCG
		}
	}
}
