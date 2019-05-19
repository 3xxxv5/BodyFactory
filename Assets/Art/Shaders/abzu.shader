// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:34140,y:32570,varname:node_4013,prsc:2|diff-3409-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:33265,y:32973,ptovrint:False,ptlb:lower,ptin:_lower,varname:_lower,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.6112132,c2:0.8110167,c3:0.9779412,c4:1;n:type:ShaderForge.SFN_Color,id:9523,x:33117,y:32690,ptovrint:False,ptlb:upper,ptin:_upper,varname:_upper,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8382353,c2:0.6718209,c3:0.6718209,c4:1;n:type:ShaderForge.SFN_If,id:392,x:33837,y:32553,varname:node_392,prsc:2|A-159-OUT,B-2458-OUT,GT-9523-RGB,EQ-1304-RGB,LT-1304-RGB;n:type:ShaderForge.SFN_Vector1,id:2458,x:32767,y:32501,varname:node_2458,prsc:2,v1:0.7;n:type:ShaderForge.SFN_NormalVector,id:1046,x:32164,y:32320,prsc:2,pt:True;n:type:ShaderForge.SFN_ComponentMask,id:159,x:32561,y:32214,varname:node_159,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-1046-OUT;n:type:ShaderForge.SFN_Lerp,id:3409,x:33751,y:32414,varname:node_3409,prsc:2|A-9523-RGB,B-1304-RGB,T-9426-OUT;n:type:ShaderForge.SFN_If,id:3157,x:33123,y:32220,varname:node_3157,prsc:2|A-159-OUT,B-2458-OUT,GT-6462-OUT,EQ-159-OUT,LT-159-OUT;n:type:ShaderForge.SFN_Vector1,id:6462,x:32783,y:32366,varname:node_6462,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:2963,x:32980,y:32086,varname:node_2963,prsc:0|B-159-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:598,x:32534,y:31963,ptovrint:False,ptlb:node_598,ptin:_node_598,varname:_node_598,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:5211,x:33291,y:32405,varname:node_5211,prsc:2,v1:9;n:type:ShaderForge.SFN_Posterize,id:9426,x:33550,y:32293,varname:node_9426,prsc:2|IN-3157-OUT,STPS-5211-OUT;proporder:9523-1304;pass:END;sub:END;*/

Shader "Shader Forge/NewShader" {
    Properties {
        _upper ("upper", Color) = (0.8382353,0.6718209,0.6718209,1)
        _lower ("lower", Color) = (0.6112132,0.8110167,0.9779412,1)
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
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _lower;
            uniform float4 _upper;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float node_159 = normalDirection.g;
                float node_2458 = 0.7;
                float node_3157_if_leA = step(node_159,node_2458);
                float node_3157_if_leB = step(node_2458,node_159);
                float node_6462 = 1.0;
                float node_3157 = lerp((node_3157_if_leA*node_159)+(node_3157_if_leB*node_6462),node_159,node_3157_if_leA*node_3157_if_leB);
                float node_5211 = 9.0;
                float3 diffuseColor = lerp(_upper.rgb,_lower.rgb,floor(node_3157 * node_5211) / (node_5211 - 1));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
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
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _lower;
            uniform float4 _upper;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float node_159 = normalDirection.g;
                float node_2458 = 0.7;
                float node_3157_if_leA = step(node_159,node_2458);
                float node_3157_if_leB = step(node_2458,node_159);
                float node_6462 = 1.0;
                float node_3157 = lerp((node_3157_if_leA*node_159)+(node_3157_if_leB*node_6462),node_159,node_3157_if_leA*node_3157_if_leB);
                float node_5211 = 9.0;
                float3 diffuseColor = lerp(_upper.rgb,_lower.rgb,floor(node_3157 * node_5211) / (node_5211 - 1));
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
