//by 李红伟

Shader "Hidden/ShutterWipe"
{
	Properties
	{
		_MainTex("MainTex",2d)="white"{}
		_Value("Value",float)=0
		_Amount("Amount",int)=0
		_Direction("Direction",int)=0
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
			uniform float _Amount;
			uniform int _Direction;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 c;

				//采样摄像机画面
				fixed4 screen=tex2D(_MainTex,i.uv);

				//脚本中传过来的_Amount为需要的百叶窗格数，但在shader中需要被1除换算一下
				_Amount=1/_Amount;

				fixed dir;
				if(_Direction==0)
				{
					dir=fmod(i.uv.y/_Amount,1);
				}
				else if(_Direction==1)
				{
					dir=fmod(i.uv.x/_Amount,1);
				}	

				fixed4 mask=fixed4(dir,dir,dir,1);

				mask=step(_Value,mask);
				c=screen*mask;

				return c;
			}
			ENDCG
		}
	}
}
