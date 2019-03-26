// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:False,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:6,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:1,qpre:4,rntp:5,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32740,y:33254,varname:node_2865,prsc:2|emission-5242-OUT,alpha-6158-OUT,voffset-9690-OUT;n:type:ShaderForge.SFN_Tex2d,id:7542,x:32284,y:33154,ptovrint:False,ptlb:texture,ptin:_texture,varname:node_1672,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d743f05314e4e2847987cbd8fd72d633,ntxv:0,isnm:False|UVIN-3822-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:977,x:32143,y:33550,varname:node_977,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:9690,x:32351,y:33550,varname:node_9690,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-977-UVOUT;n:type:ShaderForge.SFN_Multiply,id:5242,x:32500,y:33137,varname:node_5242,prsc:2|A-492-RGB,B-7542-RGB;n:type:ShaderForge.SFN_Color,id:492,x:32396,y:32968,ptovrint:False,ptlb:color,ptin:_color,varname:node_492,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_TexCoord,id:225,x:31454,y:33411,varname:node_225,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:2884,x:31412,y:33154,varname:node_2884,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-225-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:4517,x:31593,y:33154,varname:node_4517,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2884-OUT;n:type:ShaderForge.SFN_ArcTan2,id:305,x:31778,y:33154,varname:node_305,prsc:2,attp:3|A-4517-G,B-4517-R;n:type:ShaderForge.SFN_Append,id:3718,x:31941,y:33154,varname:node_3718,prsc:2|A-305-OUT,B-305-OUT;n:type:ShaderForge.SFN_Vector2,id:5796,x:31454,y:33567,varname:node_5796,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_Distance,id:4392,x:31661,y:33411,varname:node_4392,prsc:2|A-225-UVOUT,B-5796-OUT;n:type:ShaderForge.SFN_Power,id:6158,x:31999,y:33426,varname:node_6158,prsc:2|VAL-4697-OUT,EXP-2093-OUT;n:type:ShaderForge.SFN_Exp,id:2093,x:31931,y:33624,varname:node_2093,prsc:2,et:1|IN-1853-OUT;n:type:ShaderForge.SFN_Slider,id:1853,x:31611,y:33719,ptovrint:False,ptlb:clipIndex,ptin:_clipIndex,varname:node_1853,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.588529,max:8;n:type:ShaderForge.SFN_RemapRange,id:4697,x:31845,y:33409,varname:node_4697,prsc:2,frmn:0,frmx:0.7,tomn:0,tomx:1|IN-4392-OUT;n:type:ShaderForge.SFN_Rotator,id:3822,x:32128,y:33154,varname:node_3822,prsc:2|UVIN-3718-OUT;proporder:492-7542-1853;pass:END;sub:END;*/

Shader "Shader Forge/imageBlur" {
    Properties {
        _color ("color", Color) = (1,1,1,1)
        _texture ("texture", 2D) = "white" {}
        _clipIndex ("clipIndex", Range(0, 8)) = 2.588529
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Overlay+1"
            "RenderType"="Overlay"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZTest Always
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _texture; uniform float4 _texture_ST;
            uniform float4 _color;
            uniform float _clipIndex;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                v.vertex.xyz = float3((o.uv0*2.0+-1.0),0.0);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_3520 = _Time + _TimeEditor;
                float node_3822_ang = node_3520.g;
                float node_3822_spd = 1.0;
                float node_3822_cos = cos(node_3822_spd*node_3822_ang);
                float node_3822_sin = sin(node_3822_spd*node_3822_ang);
                float2 node_3822_piv = float2(0.5,0.5);
                float2 node_4517 = (i.uv0*2.0+-1.0).rg;
                float node_305 = (1-abs(atan2(node_4517.g,node_4517.r)/3.14159265359));
                float2 node_3822 = (mul(float2(node_305,node_305)-node_3822_piv,float2x2( node_3822_cos, -node_3822_sin, node_3822_sin, node_3822_cos))+node_3822_piv);
                float4 _texture_var = tex2D(_texture,TRANSFORM_TEX(node_3822, _texture));
                float3 emissive = (_color.rgb*_texture_var.rgb);
                float3 finalColor = emissive;
                float2 node_5796 = float2(0.5,0.5);
                float node_4392 = distance(i.uv0,node_5796);
                return fixed4(finalColor,pow((node_4392*1.428571+0.0),exp2(_clipIndex)));
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
