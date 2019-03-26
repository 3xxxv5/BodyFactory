Shader "Fog Volume/RT viewers/RT_Depth"
{
	Properties{
		_MainTex("Base", 2D) = "" {}
		_Intensity("Intensity", Range(1, 20)) = 1
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma multi_compile _ _FOG_LOWRES_RENDERER 	
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			float _Intensity;
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

			sampler2D RT_Depth, _MainTex;
			float4 RT_Depth_ST, _MainTex_TexelSize, _MainTex_ST;
			sampler2D   _CameraDepthTexture;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, RT_Depth);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture

				float Depth = tex2D(RT_Depth, i.uv).r;

			Depth *= 1000;
			return 1/Depth*_Intensity;
//#if UNITY_REVERSED_Z!=1
//				return float4(1, 0, 0, 1);
//#else
//				return float4(0, 1, 0, 1);
//#endif
			//Depth = LinearEyeDepth(Depth);
		//	Depth = 1.0 / (_ZBufferParams.z * Depth + _ZBufferParams.w);
			//Depth = max(.001, Depth);
			//

			//if (Depth < .0001)Depth = 1;
			//Depth /= _ProjectionParams.w;
			
			//Depth *= 1000;
			
			//https://docs.unity3d.com/Manual/SL-UnityShaderVariables.html
			//_ZBufferParams 	float4 	Used to linearize Z buffer values. x is (1-far/near), y is (far/near), z is (x/far) and w is (y/far).
			half far = 1000;
			half near = .01;
			float x, y, z, w;
			x = 1 - far / near;
			y = far / near;
			z = x / far;
			w = y / far;
			//Depth = 1/ (z * Depth + w);
				//if (Depth < .0000001)Depth = 1000;
				return (1/Depth*_Intensity);
				//return DecodeFloatRGBA(z)*50;
			}
			ENDCG
		}
	}
}
