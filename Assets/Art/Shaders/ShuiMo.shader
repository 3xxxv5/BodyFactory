Shader "Unlit/ShuiMo" 
{
    Properties 
    {    
		_Outline("Outline",Range(0,1)) = 0.5
		_Range("Range" , Range(0,10)) = 0.1		
		_Pow("Pow",Range(0,10))=1
		_FresnelScale("FresnelScale",Range(-10,10)) = 0.5

		_Brightness("Brightness", Float) = 1	
		_Saturation("Saturation", Float) = 1	
		_Contrast("Contrast", Float) = 1

        _MainTex ("MainTexture", 2D) = "white" {}
        _MainNormalTex ("NormalTex", 2D) = "white" {}
		_BumpScale ("Bump Scale", Float) = 1.0
		_FeatureTex("FeatureTex",2D) = "white" {}
				
    }

    SubShader 
    {
        Tags{"RenderType"="Opaque"}
		Pass 
		{
			Tags { "LightMode"="ForwardBase"}
			CGPROGRAM
		    #pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
            #pragma multi_compile_fwdbase

			#include "UnityCG.cginc"
            #include "Lighting.cginc"

			float _Outline;
			float _Pow;
			float _Range;
			half _Brightness;
			half _Saturation;
			half _Contrast;
			half _FresnelScale;
			sampler2D _FeatureTex;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _MainNormalTex;
			half _BumpScale;
			float4 _MainNormalTex_ST;
			struct v2f
			{
                float4 pos : SV_POSITION;
				float4 uv : TEXCOORD0;	
                float4 T2W0 :TEXCOORD1;
                float4 T2W1 :TEXCOORD2;
                float4 T2W2 :TEXCOORD3;
			};
			
			v2f vert(appdata_full v)
			{
				v2f o;				
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.uv.zw = TRANSFORM_TEX(v.texcoord,_MainNormalTex);

                float3 worldPos = mul(unity_ObjectToWorld,v.vertex);
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                float3 worldTangent = UnityObjectToWorldDir(v.tangent);
                float3 worldBitangent = cross(worldNormal ,worldTangent) * v.tangent.w;

                o.T2W0 = float4 (worldTangent.x,worldBitangent.x,worldNormal.x,worldPos .x);
                o.T2W1 = float4 (worldTangent.y,worldBitangent.y,worldNormal.y,worldPos .y);
                o.T2W2 = float4 (worldTangent.z,worldBitangent.z,worldNormal.z,worldPos .z);
				return o;
			}


			half4 frag(v2f i) : SV_Target
			{
				
				float4 albedo = tex2D(_MainTex,i.uv.xy).rgba;
				fixed3 finalColor = albedo * _Brightness;
				fixed gray = 0.2125 * albedo.r + 0.7154 * albedo.g + 0.0721 * albedo.b;
				fixed3 grayColor = fixed3(gray, gray, gray);
				finalColor = lerp(grayColor, finalColor, _Saturation);
				fixed3 avgColor = fixed3(0.5, 0.5, 0.5);
				finalColor = lerp(avgColor, finalColor, _Contrast);
                float3 worldPos  = float3(i.T2W0.w,i.T2W1.w,i.T2W2.w);
                fixed3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));	
                fixed3 bump = UnpackNormal(tex2D(_MainNormalTex,i.uv.zw));
				bump.xy *= _BumpScale;
				bump.z = sqrt(1.0 - saturate(dot(bump.xy, bump.xy)));
                bump = normalize(half3( dot(i.T2W0.xyz,bump),dot(i.T2W1.xyz,bump),dot(i.T2W2.xyz,bump)));
				half nv = max(saturate(dot(bump, viewDir)),0.0001);
				fixed outline = pow(nv, _Pow)/ _Range;	
				fixed edge = outline > _Outline ? 1 : 0;			
				outline = saturate(outline * edge);
				float4 featuretex = tex2D(_FeatureTex,i.uv.xy).rgba;
				outline = saturate(fixed4(outline, outline, outline, outline));
				float4 feature = outline * featuretex;
				fixed3 blend = finalColor < feature ? feature: finalColor;
				fixed F = _FresnelScale + (1 - _FresnelScale) * pow(1 - dot(viewDir, bump), 5);
				blend = blend*(1-F);
				return fixed4(blend,1);
			}
			ENDCG
		}
    }
    FallBack "Diffuse"    
}
