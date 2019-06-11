// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:33675,y:32569,varname:node_2865,prsc:2|diff-6616-RGB,spec-9876-OUT,emission-3180-OUT;n:type:ShaderForge.SFN_Tex2d,id:7736,x:31708,y:32122,ptovrint:True,ptlb:chest head,ptin:_chest_head,varname:_chest_head,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8212d13c320a3c6449bdb2c550eebc05,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:358,x:31522,y:32399,ptovrint:True,ptlb:chest_head_intensity,ptin:_ch_intensity,varname:_ch_intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:2140,x:31718,y:32570,ptovrint:True,ptlb:chest tail,ptin:_chest_tail,varname:_chest_tail,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b80b4421b0646514a862b44258f74f9b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1800,x:31644,y:32974,ptovrint:True,ptlb:head tail,ptin:_head_tail,varname:_head_tail,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:06eee544f315e8946b9ff50b3878d712,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:3326,x:32467,y:33258,varname:node_3326,prsc:2|A-8805-OUT,B-299-OUT,C-9473-OUT,D-7983-OUT,E-87-OUT;n:type:ShaderForge.SFN_Tex2d,id:6616,x:32491,y:32320,ptovrint:True,ptlb:base color,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:35d9b51dd28b7564681833308ffa79b7,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:5632,x:31625,y:32797,ptovrint:True,ptlb:chest_tail_intensity,ptin:_ct_intensity,varname:_ct_intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:7667,x:31566,y:33241,ptovrint:True,ptlb:head_tail_intensity,ptin:_ht_intensity,varname:_ht_intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:8805,x:32096,y:32445,varname:node_8805,prsc:2|A-7736-RGB,B-358-OUT;n:type:ShaderForge.SFN_Multiply,id:299,x:31955,y:32625,varname:node_299,prsc:2|A-2140-RGB,B-5632-OUT;n:type:ShaderForge.SFN_Multiply,id:9473,x:31973,y:33118,varname:node_9473,prsc:2|A-1800-RGB,B-7667-OUT;n:type:ShaderForge.SFN_Vector1,id:9876,x:32554,y:32613,varname:node_9876,prsc:2,v1:0;n:type:ShaderForge.SFN_Color,id:6852,x:32603,y:32832,ptovrint:True,ptlb:emission,ptin:_emission_color,varname:_emission_color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4926471,c2:0.5381337,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:3180,x:33295,y:32825,varname:node_3180,prsc:2|A-9876-OUT,B-6852-RGB,T-3910-OUT;n:type:ShaderForge.SFN_Tex2d,id:7963,x:31535,y:33384,ptovrint:True,ptlb:head,ptin:_head,varname:_head,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0683a17928cbbbf49b0bdb54c4cba0c7,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:4605,x:31480,y:33673,ptovrint:True,ptlb:head,ptin:_head_intensity,varname:_head_intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:7983,x:31985,y:33367,varname:node_7983,prsc:2|A-7963-RGB,B-4605-OUT;n:type:ShaderForge.SFN_Tex2d,id:7158,x:31739,y:33818,ptovrint:True,ptlb:back,ptin:_back,varname:_back,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8cf07e834df9f0046b45b9d246e5baad,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:87,x:32163,y:33544,varname:node_87,prsc:2|A-7158-RGB,B-391-OUT;n:type:ShaderForge.SFN_Slider,id:391,x:31671,y:34065,ptovrint:True,ptlb:_back_intensity,ptin:_back_intensity,varname:_back_intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:1162,x:32327,y:33747,varname:node_1162,prsc:2|A-6596-RGB,B-2179-OUT;n:type:ShaderForge.SFN_Tex2d,id:6596,x:31996,y:34065,ptovrint:True,ptlb:tail,ptin:_tail,varname:_tail,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:fd53c5cb935c0144db6ecb353a815d67,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:2179,x:31875,y:34346,ptovrint:True,ptlb:tail_intensity,ptin:_tail_intensity,varname:_tail_intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Add,id:54,x:32715,y:33154,varname:node_54,prsc:2|A-3326-OUT,B-1162-OUT;n:type:ShaderForge.SFN_Multiply,id:3910,x:32926,y:33211,varname:node_3910,prsc:2|A-54-OUT,B-4893-OUT;n:type:ShaderForge.SFN_Slider,id:4893,x:32389,y:33485,ptovrint:False,ptlb:total_intensity,ptin:_total_intensity,varname:_total_intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.5,cur:1.760684,max:3;proporder:7736-358-2140-1800-6616-5632-7667-6852-7963-4605-7158-391-6596-2179-4893;pass:END;sub:END;*/

Shader "Shader Forge/dragon_new" {
    Properties {
        _chest_head ("chest head", 2D) = "white" {}
        _ch_intensity ("chest_head_intensity", Range(0, 1)) = 1
        _chest_tail ("chest tail", 2D) = "white" {}
        _head_tail ("head tail", 2D) = "white" {}
        _MainTex ("base color", 2D) = "white" {}
        _ct_intensity ("chest_tail_intensity", Range(0, 1)) = 1
        _ht_intensity ("head_tail_intensity", Range(0, 1)) = 1
        _emission_color ("emission", Color) = (0.4926471,0.5381337,1,1)
        _head ("head", 2D) = "white" {}
        _head_intensity ("head", Range(0, 1)) = 1
        _back ("back", 2D) = "white" {}
        _back_intensity ("_back_intensity", Range(0, 1)) = 1
        _tail ("tail", 2D) = "white" {}
        _tail_intensity ("tail_intensity", Range(0, 1)) = 1
        _total_intensity ("total_intensity", Range(0.5, 3)) = 1.760684
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _chest_head; uniform float4 _chest_head_ST;
            uniform float _ch_intensity;
            uniform sampler2D _chest_tail; uniform float4 _chest_tail_ST;
            uniform sampler2D _head_tail; uniform float4 _head_tail_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _ct_intensity;
            uniform float _ht_intensity;
            uniform float4 _emission_color;
            uniform sampler2D _head; uniform float4 _head_ST;
            uniform float _head_intensity;
            uniform sampler2D _back; uniform float4 _back_ST;
            uniform float _back_intensity;
            uniform sampler2D _tail; uniform float4 _tail_ST;
            uniform float _tail_intensity;
            uniform float _total_intensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 0.5;
                float perceptualRoughness = 1.0 - 0.5;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float node_9876 = 0.0;
                float3 specularColor = node_9876;
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuseColor = _MainTex_var.rgb; // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _chest_head_var = tex2D(_chest_head,TRANSFORM_TEX(i.uv0, _chest_head));
                float4 _chest_tail_var = tex2D(_chest_tail,TRANSFORM_TEX(i.uv0, _chest_tail));
                float4 _head_tail_var = tex2D(_head_tail,TRANSFORM_TEX(i.uv0, _head_tail));
                float4 _head_var = tex2D(_head,TRANSFORM_TEX(i.uv0, _head));
                float4 _back_var = tex2D(_back,TRANSFORM_TEX(i.uv0, _back));
                float4 _tail_var = tex2D(_tail,TRANSFORM_TEX(i.uv0, _tail));
                float3 emissive = lerp(float3(node_9876,node_9876,node_9876),_emission_color.rgb,((((_chest_head_var.rgb*_ch_intensity)+(_chest_tail_var.rgb*_ct_intensity)+(_head_tail_var.rgb*_ht_intensity)+(_head_var.rgb*_head_intensity)+(_back_var.rgb*_back_intensity))+(_tail_var.rgb*_tail_intensity))*_total_intensity));
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _chest_head; uniform float4 _chest_head_ST;
            uniform float _ch_intensity;
            uniform sampler2D _chest_tail; uniform float4 _chest_tail_ST;
            uniform sampler2D _head_tail; uniform float4 _head_tail_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _ct_intensity;
            uniform float _ht_intensity;
            uniform float4 _emission_color;
            uniform sampler2D _head; uniform float4 _head_ST;
            uniform float _head_intensity;
            uniform sampler2D _back; uniform float4 _back_ST;
            uniform float _back_intensity;
            uniform sampler2D _tail; uniform float4 _tail_ST;
            uniform float _tail_intensity;
            uniform float _total_intensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 0.5;
                float perceptualRoughness = 1.0 - 0.5;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float node_9876 = 0.0;
                float3 specularColor = node_9876;
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuseColor = _MainTex_var.rgb; // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _chest_head; uniform float4 _chest_head_ST;
            uniform float _ch_intensity;
            uniform sampler2D _chest_tail; uniform float4 _chest_tail_ST;
            uniform sampler2D _head_tail; uniform float4 _head_tail_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _ct_intensity;
            uniform float _ht_intensity;
            uniform float4 _emission_color;
            uniform sampler2D _head; uniform float4 _head_ST;
            uniform float _head_intensity;
            uniform sampler2D _back; uniform float4 _back_ST;
            uniform float _back_intensity;
            uniform sampler2D _tail; uniform float4 _tail_ST;
            uniform float _tail_intensity;
            uniform float _total_intensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float node_9876 = 0.0;
                float4 _chest_head_var = tex2D(_chest_head,TRANSFORM_TEX(i.uv0, _chest_head));
                float4 _chest_tail_var = tex2D(_chest_tail,TRANSFORM_TEX(i.uv0, _chest_tail));
                float4 _head_tail_var = tex2D(_head_tail,TRANSFORM_TEX(i.uv0, _head_tail));
                float4 _head_var = tex2D(_head,TRANSFORM_TEX(i.uv0, _head));
                float4 _back_var = tex2D(_back,TRANSFORM_TEX(i.uv0, _back));
                float4 _tail_var = tex2D(_tail,TRANSFORM_TEX(i.uv0, _tail));
                o.Emission = lerp(float3(node_9876,node_9876,node_9876),_emission_color.rgb,((((_chest_head_var.rgb*_ch_intensity)+(_chest_tail_var.rgb*_ct_intensity)+(_head_tail_var.rgb*_ht_intensity)+(_head_var.rgb*_head_intensity)+(_back_var.rgb*_back_intensity))+(_tail_var.rgb*_tail_intensity))*_total_intensity));
                
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffColor = _MainTex_var.rgb;
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, node_9876, specColor, specularMonochrome );
                o.Albedo = diffColor + specColor * 0.125; // No gloss connected. Assume it's 0.5
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
