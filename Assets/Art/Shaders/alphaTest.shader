Shader"Alpha Test"
{
	Properties
	{
		_Color ("Color Tint", Color) = (1,1,1,1)
		_MainTex ("MainTex", 2D) = "White" {}
	//控制透明度测试时的阈值，决定调用clip进行透明度测试时使用的判定条件，纹理像素的透明度在0-1范围
	_Cutoff ("Alpha Cutoff", Range (0,1)) = 0.5
	}
		SubShader
	{
		//Queue渲染队列，决定此shader的渲染顺序；
		//IgnoreProject是否忽视投影器(Project)的影响；
		//RenderType将此shader归入提前定义的组，以指明此shader使用了透明度测试，一般用于着色器替换
		//通常使用了透明度测试的shader都应该设置这三个标签
		Tags{ "Queue" = "AlphaTest" "IgnoreProject" = "True" "RenderType" = "TransparentCutout" }

		Pass
	{
		Tags{ "LightMode" = "ForwardBase" }

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "LIghting.cginc"

		fixed4 _Color;
	sampler2D _MainTex;
	//需要用 “纹理名_ST” 的方式定义纹理的属性，S是缩放(scale)，T是平移(transform)
	//_MainTex_ST.xy获取缩放值，_MainTex_ST.zw获取偏移值
	float4 _MainTex_ST;
	fixed _Cutoff;

	struct a2v
	{
		float4 vertex : POSITION;
		float3 normal : NORMAL;
		float4 texcoord : TEXCOORD0;
	};

	struct v2f
	{
		float4 pos : SV_POSITION;
		float3 worldNormal : TEXCOORD0;
		float3 worldPos : TEXCOORD1;
		float2 uv : TEXCOORD2;
	};

	//在顶点着色器中计算出世界空间的法线方向和顶点坐标以及变换后的纹理坐标
	v2f vert (a2v v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos (v.vertex);
		o.worldNormal = UnityObjectToWorldNormal (v.normal);
		o.worldPos = mul (unity_ObjectToWorld, v.vertex).xyz;
		o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
		return o;
	}

	fixed4 frag (v2f i) : SV_Target
	{
		fixed3 worldNormal = normalize (i.worldNormal);
	fixed3 worldLightDir = normalize (UnityWorldSpaceLightDir (i.worldPos));

	fixed4 texColor = tex2D (_MainTex, i.uv);
	//透明度测试
	clip (texColor.a - _Cutoff);
	//等同于
	//if((tecColor.a - _Cutoff) < 0.0)
	//{
	// discard;
	//}



	//使用tex2D(需要被采集的纹理名，纹理坐标)进行纹理采样，再与颜色相乘，返回纹素值
	fixed3 albedo = texColor.rgb * _Color.rgb;
	//将环境光与纹理素相乘
	fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
	//漫反射与纹理素相乘
	fixed3 diffuse = _LightColor0.rgb * albedo * saturate (dot (worldNormal,worldLightDir));

	return fixed4 (ambient + diffuse,1.0);
	; }
		ENDCG
	}
	}

		Fallback"Transparent/Cutoff/Vertexlit"
}