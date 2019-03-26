Shader "MyShaders/TexAnim"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
	    _Color ("Color",Color) =(1,1,1,1)
		_Speed ("Speed",Range(0,3)) = 1//纹理移动的速度
		_ScaleX("ScaleX",float)=1
	    _ScaleY ("ScaleY",float) = 1

    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" }

        Pass
        {
			Tags{"LightMode"="ForwardBase" }
			ZWrite Off
			Blend SrcAlpha One

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct a2v
            {
                float4 vertex : POSITION;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD2;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			fixed4  _Color;
			float  _Speed;
			float _ScaleX;
			float _ScaleY;

            v2f vert (a2v v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord.xy*_MainTex_ST.xy*float2(_ScaleX,_ScaleY) + _MainTex_ST.zw + frac(float2(_Speed,0.0)*_Time.y);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv)*_Color;
                return col;
            }
            ENDCG
        }
    }
	Fallback "Diffuse"
}
