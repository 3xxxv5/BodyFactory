// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:34871,y:32120,varname:node_4013,prsc:2|diff-1304-RGB,voffset-4433-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:32479,y:32247,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4641544,c2:0.7426471,c3:0.5121704,c4:1;n:type:ShaderForge.SFN_TexCoord,id:9162,x:31568,y:32741,varname:node_9162,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_OneMinus,id:1174,x:31737,y:32976,varname:node_1174,prsc:2|IN-9162-V;n:type:ShaderForge.SFN_Vector1,id:9706,x:32349,y:33212,varname:node_9706,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:9197,x:32404,y:33101,varname:node_9197,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:2327,x:32723,y:33207,varname:nothing,prsc:2|A-3467-OUT,B-1174-OUT;n:type:ShaderForge.SFN_Vector1,id:3467,x:32349,y:32965,varname:normal,prsc:2,v1:0;n:type:ShaderForge.SFN_ObjectPosition,id:1070,x:32305,y:32558,varname:node_1070,prsc:2;n:type:ShaderForge.SFN_Add,id:1065,x:32481,y:33459,varname:left,prsc:2|A-5645-OUT,B-9706-OUT;n:type:ShaderForge.SFN_Lerp,id:1343,x:33351,y:33328,varname:node_1343,prsc:2|A-5129-OUT,B-2327-OUT,T-7966-OUT;n:type:ShaderForge.SFN_Subtract,id:6470,x:32701,y:32618,varname:node_6470,prsc:2|A-1070-X,B-6319-X;n:type:ShaderForge.SFN_Vector4Property,id:6319,x:32305,y:32754,ptovrint:False,ptlb:heroPos,ptin:_heroPos,varname:_heroPos,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_Distance,id:7966,x:32833,y:33026,varname:node_7966,prsc:2|A-1070-XYZ,B-6319-XYZ;n:type:ShaderForge.SFN_Slider,id:1845,x:32088,y:32422,ptovrint:False,ptlb:distance,ptin:_distance,varname:_distance,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:300;n:type:ShaderForge.SFN_Subtract,id:7051,x:32066,y:33306,varname:node_7051,prsc:2|A-1174-OUT,B-9706-OUT;n:type:ShaderForge.SFN_Multiply,id:5645,x:32302,y:33472,varname:node_5645,prsc:2|A-7051-OUT,B-9197-OUT;n:type:ShaderForge.SFN_Subtract,id:6948,x:32701,y:32736,varname:node_6948,prsc:2|A-1070-Y,B-6319-Y;n:type:ShaderForge.SFN_Subtract,id:446,x:32669,y:32856,varname:node_446,prsc:2|A-1070-Z,B-6319-Z;n:type:ShaderForge.SFN_Append,id:7820,x:34248,y:32630,varname:node_7820,prsc:2|A-9509-OUT,B-2327-OUT,C-6327-OUT;n:type:ShaderForge.SFN_If,id:4433,x:34539,y:32401,varname:node_4433,prsc:2|A-7966-OUT,B-1845-OUT,GT-2327-OUT,EQ-2327-OUT,LT-7820-OUT;n:type:ShaderForge.SFN_Lerp,id:5938,x:33351,y:33490,varname:node_5938,prsc:2|A-5129-OUT,B-2327-OUT,T-7966-OUT;n:type:ShaderForge.SFN_Multiply,id:711,x:32678,y:33474,varname:forward,prsc:2|A-1065-OUT,B-2733-OUT;n:type:ShaderForge.SFN_Slider,id:2733,x:32291,y:33738,ptovrint:False,ptlb:strength,ptin:_strength,varname:_strength,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:30;n:type:ShaderForge.SFN_If,id:7287,x:33200,y:32642,varname:node_7287,prsc:2|A-6470-OUT,B-3467-OUT,GT-4762-OUT,EQ-8381-OUT,LT-8381-OUT;n:type:ShaderForge.SFN_If,id:2048,x:33200,y:32803,varname:node_2048,prsc:2|A-446-OUT,B-3467-OUT,GT-4762-OUT,EQ-8381-OUT,LT-8381-OUT;n:type:ShaderForge.SFN_Vector1,id:9682,x:32944,y:32552,varname:node_9682,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:9509,x:33789,y:32695,varname:node_9509,prsc:2|A-7287-OUT,B-1343-OUT;n:type:ShaderForge.SFN_Vector1,id:4762,x:32870,y:32832,varname:node_4762,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:8381,x:32905,y:32692,varname:node_8381,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:6327,x:33830,y:32938,varname:node_6327,prsc:2|A-2048-OUT,B-5938-OUT;n:type:ShaderForge.SFN_Multiply,id:5129,x:32846,y:33489,varname:node_5129,prsc:2|A-711-OUT,B-711-OUT;proporder:1304-6319-1845-2733;pass:END;sub:END;*/

Shader "Shader Forge/GRASS" {
    Properties {
        _Color ("Color", Color) = (0.4641544,0.7426471,0.5121704,1)
        _heroPos ("heroPos", Vector) = (0,0,0,0)
        _distance ("distance", Range(0, 300)) = 1
        _strength ("strength", Range(0, 30)) = 0
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
            uniform float4 _Color;
            uniform float4 _heroPos;
            uniform float _distance;
            uniform float _strength;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float node_7966 = distance(objPos.rgb,_heroPos.rgb);
                float node_4433_if_leA = step(node_7966,_distance);
                float node_4433_if_leB = step(_distance,node_7966);
                float normal = 0.0;
                float node_7287_if_leA = step((objPos.r-_heroPos.r),normal);
                float node_7287_if_leB = step(normal,(objPos.r-_heroPos.r));
                float node_8381 = (-1.0);
                float node_4762 = 1.0;
                float node_1174 = (1.0 - o.uv0.g);
                float node_9706 = 0.5;
                float forward = ((((node_1174-node_9706)*(-1.0))+node_9706)*_strength);
                float node_5129 = (forward*forward);
                float nothing = (normal*node_1174);
                float node_2048_if_leA = step((objPos.b-_heroPos.b),normal);
                float node_2048_if_leB = step(normal,(objPos.b-_heroPos.b));
                v.vertex.xyz += lerp((node_4433_if_leA*float3((lerp((node_7287_if_leA*node_8381)+(node_7287_if_leB*node_4762),node_8381,node_7287_if_leA*node_7287_if_leB)*lerp(node_5129,nothing,node_7966)),nothing,(lerp((node_2048_if_leA*node_8381)+(node_2048_if_leB*node_4762),node_8381,node_2048_if_leA*node_2048_if_leB)*lerp(node_5129,nothing,node_7966))))+(node_4433_if_leB*nothing),nothing,node_4433_if_leA*node_4433_if_leB);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
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
                float3 diffuseColor = _Color.rgb;
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
            uniform float4 _Color;
            uniform float4 _heroPos;
            uniform float _distance;
            uniform float _strength;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float node_7966 = distance(objPos.rgb,_heroPos.rgb);
                float node_4433_if_leA = step(node_7966,_distance);
                float node_4433_if_leB = step(_distance,node_7966);
                float normal = 0.0;
                float node_7287_if_leA = step((objPos.r-_heroPos.r),normal);
                float node_7287_if_leB = step(normal,(objPos.r-_heroPos.r));
                float node_8381 = (-1.0);
                float node_4762 = 1.0;
                float node_1174 = (1.0 - o.uv0.g);
                float node_9706 = 0.5;
                float forward = ((((node_1174-node_9706)*(-1.0))+node_9706)*_strength);
                float node_5129 = (forward*forward);
                float nothing = (normal*node_1174);
                float node_2048_if_leA = step((objPos.b-_heroPos.b),normal);
                float node_2048_if_leB = step(normal,(objPos.b-_heroPos.b));
                v.vertex.xyz += lerp((node_4433_if_leA*float3((lerp((node_7287_if_leA*node_8381)+(node_7287_if_leB*node_4762),node_8381,node_7287_if_leA*node_7287_if_leB)*lerp(node_5129,nothing,node_7966)),nothing,(lerp((node_2048_if_leA*node_8381)+(node_2048_if_leB*node_4762),node_8381,node_2048_if_leA*node_2048_if_leB)*lerp(node_5129,nothing,node_7966))))+(node_4433_if_leB*nothing),nothing,node_4433_if_leA*node_4433_if_leB);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
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
                float3 diffuseColor = _Color.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _heroPos;
            uniform float _distance;
            uniform float _strength;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float node_7966 = distance(objPos.rgb,_heroPos.rgb);
                float node_4433_if_leA = step(node_7966,_distance);
                float node_4433_if_leB = step(_distance,node_7966);
                float normal = 0.0;
                float node_7287_if_leA = step((objPos.r-_heroPos.r),normal);
                float node_7287_if_leB = step(normal,(objPos.r-_heroPos.r));
                float node_8381 = (-1.0);
                float node_4762 = 1.0;
                float node_1174 = (1.0 - o.uv0.g);
                float node_9706 = 0.5;
                float forward = ((((node_1174-node_9706)*(-1.0))+node_9706)*_strength);
                float node_5129 = (forward*forward);
                float nothing = (normal*node_1174);
                float node_2048_if_leA = step((objPos.b-_heroPos.b),normal);
                float node_2048_if_leB = step(normal,(objPos.b-_heroPos.b));
                v.vertex.xyz += lerp((node_4433_if_leA*float3((lerp((node_7287_if_leA*node_8381)+(node_7287_if_leB*node_4762),node_8381,node_7287_if_leA*node_7287_if_leB)*lerp(node_5129,nothing,node_7966)),nothing,(lerp((node_2048_if_leA*node_8381)+(node_2048_if_leB*node_4762),node_8381,node_2048_if_leA*node_2048_if_leB)*lerp(node_5129,nothing,node_7966))))+(node_4433_if_leB*nothing),nothing,node_4433_if_leA*node_4433_if_leB);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
