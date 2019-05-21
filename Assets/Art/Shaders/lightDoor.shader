// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:34025,y:32537,varname:node_4013,prsc:2|emission-1304-RGB,alpha-5552-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:33698,y:32378,ptovrint:True,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_ViewPosition,id:4552,x:32109,y:33119,varname:node_4552,prsc:2;n:type:ShaderForge.SFN_Distance,id:6889,x:32545,y:33182,varname:node_6889,prsc:2|A-4552-XYZ,B-7980-XYZ;n:type:ShaderForge.SFN_ObjectPosition,id:7980,x:32164,y:33371,varname:node_7980,prsc:2;n:type:ShaderForge.SFN_Lerp,id:6530,x:33518,y:33042,varname:node_6530,prsc:2|A-9090-OUT,B-8887-OUT,T-2655-OUT;n:type:ShaderForge.SFN_Vector1,id:9090,x:33134,y:33035,varname:node_9090,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:8887,x:33134,y:32941,varname:node_8887,prsc:2,v1:0;n:type:ShaderForge.SFN_TexCoord,id:253,x:32383,y:32840,varname:node_253,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:213,x:32560,y:32820,varname:node_213,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-253-UVOUT;n:type:ShaderForge.SFN_Length,id:929,x:32733,y:32791,varname:node_929,prsc:2|IN-213-OUT;n:type:ShaderForge.SFN_Subtract,id:8916,x:32965,y:32670,varname:node_8916,prsc:2|A-929-OUT,B-833-OUT;n:type:ShaderForge.SFN_Vector1,id:833,x:32712,y:32595,varname:node_833,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:3595,x:33147,y:32685,varname:node_3595,prsc:2|A-8916-OUT,B-5065-OUT;n:type:ShaderForge.SFN_Vector1,id:5065,x:32878,y:32528,varname:node_5065,prsc:2,v1:-1;n:type:ShaderForge.SFN_Add,id:8027,x:33355,y:32639,varname:node_8027,prsc:2|A-833-OUT,B-3595-OUT;n:type:ShaderForge.SFN_Vector1,id:5781,x:33536,y:32858,varname:node_5781,prsc:2,v1:4;n:type:ShaderForge.SFN_Max,id:9195,x:33601,y:32652,varname:node_9195,prsc:2|A-8027-OUT,B-8003-OUT;n:type:ShaderForge.SFN_Vector1,id:8003,x:33331,y:32858,varname:node_8003,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:5552,x:33769,y:32813,varname:node_5552,prsc:2|A-9195-OUT,B-6530-OUT;n:type:ShaderForge.SFN_Divide,id:2655,x:33342,y:33149,varname:node_2655,prsc:2|A-1296-OUT,B-8533-OUT;n:type:ShaderForge.SFN_Slider,id:7380,x:32744,y:33555,ptovrint:False,ptlb:node_7380,ptin:_node_7380,varname:_node_7380,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:8533,x:33145,y:33289,varname:node_8533,prsc:2|A-6168-OUT,B-7380-OUT;n:type:ShaderForge.SFN_Max,id:6168,x:32890,y:33213,varname:node_6168,prsc:2|A-6889-OUT,B-6013-OUT;n:type:ShaderForge.SFN_Slider,id:1296,x:32733,y:33070,ptovrint:False,ptlb:node_7380_copy,ptin:_node_7380_copy,varname:_node_7380_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:4.538462,max:10;n:type:ShaderForge.SFN_Slider,id:6013,x:32466,y:33390,ptovrint:False,ptlb:node_7380_2,ptin:_node_7380_2,varname:_node_7380_2,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:20,cur:20,max:100;proporder:1304-7380-1296-6013;pass:END;sub:END;*/

Shader "Shader Forge/lightDoor" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _node_7380 ("node_7380", Range(0, 1)) = 1
        _node_7380_copy ("node_7380_copy", Range(1, 10)) = 4.538462
        _node_7380_2 ("node_7380_2", Range(20, 100)) = 20
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _node_7380;
            uniform float _node_7380_copy;
            uniform float _node_7380_2;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
////// Lighting:
////// Emissive:
                float3 emissive = _Color.rgb;
                float3 finalColor = emissive;
                float node_833 = 0.5;
                fixed4 finalRGBA = fixed4(finalColor,(max((node_833+((length((i.uv0*2.0+-1.0))-node_833)*(-1.0))),0.0)*lerp(1.0,0.0,(_node_7380_copy/(max(distance(_WorldSpaceCameraPos,objPos.rgb),_node_7380_2)*_node_7380)))));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
