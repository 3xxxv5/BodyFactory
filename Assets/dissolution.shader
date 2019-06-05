Shader "Custom/dissolution" {
	Properties{
		_Color ("Color", Color) = (1,1,1,1)
		_NoisTex ("Dissolution",2D) = "white"{}
	_Glossiness ("Smoothness", Range (0,1)) = 0.5
		_Metallic ("Metallic", Range (0,1)) = 0.0

		_DissolvePercentage ("DissolvePercentage",Range (0,1)) = 1
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

		sampler3D _NoisTex;
	struct Input {
		float2 uv_MainTex;
		float3 worldPos;
	};

	half _Glossiness;
	half _Metallic;
	fixed4 _Color;
	float _DissolvePercentage;
	void surf (Input IN, inout SurfaceOutputStandard o) {
		// Albedo comes from a texture tinted by color
		//根据像素世界坐标获取3D贴图中的颜色：基本色
		//这里的 c 可以直接用_Color代替，这样物体表面就是纯色了
		fixed4 c = tex3D (_NoisTex,float3(IN.worldPos.rgb)) * _Color;
		o.Albedo = c.rgb;
		// Metallic and smoothness come from slider variables
		o.Metallic = _Metallic;
		o.Smoothness = _Glossiness;
		o.Alpha = c.a;
		//根据像素世界坐标获取3D贴图中的颜色计算溶解
		half gradient = tex3D (_NoisTex, float3(IN.worldPos.rgb)).r;
		clip (gradient - _DissolvePercentage);
	}
	ENDCG
	}
		FallBack "Diffuse"
}