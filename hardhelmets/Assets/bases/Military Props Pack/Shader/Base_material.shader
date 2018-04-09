// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:6,x:33589,y:32423,varname:node_6,prsc:2|diff-1057-OUT,spec-2225-OUT,gloss-3805-OUT,normal-538-OUT,amdfl-7497-OUT;n:type:ShaderForge.SFN_Tex2d,id:236,x:32710,y:31956,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_236,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-1167-OUT;n:type:ShaderForge.SFN_Multiply,id:1428,x:33032,y:32034,varname:node_1428,prsc:2|A-236-RGB,B-3946-RGB;n:type:ShaderForge.SFN_Color,id:3946,x:32875,y:32094,ptovrint:False,ptlb:Diffuse Color,ptin:_DiffuseColor,varname:node_3946,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_TexCoord,id:9865,x:32201,y:32457,varname:node_9865,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:1167,x:32398,y:32511,varname:node_1167,prsc:2|A-9865-UVOUT,B-5277-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5277,x:32201,y:32629,ptovrint:False,ptlb:Tesselation,ptin:_Tesselation,varname:node_5277,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:7213,x:32692,y:32352,ptovrint:False,ptlb:SRM,ptin:_SRM,varname:node_7213,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-1167-OUT;n:type:ShaderForge.SFN_OneMinus,id:4547,x:32884,y:32389,varname:node_4547,prsc:2|IN-7213-G;n:type:ShaderForge.SFN_Tex2d,id:2746,x:33126,y:32843,ptovrint:False,ptlb:normal,ptin:_normal,varname:node_2746,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True|UVIN-1167-OUT;n:type:ShaderForge.SFN_Multiply,id:2225,x:33298,y:32564,varname:node_2225,prsc:2|A-7213-B,B-1705-OUT;n:type:ShaderForge.SFN_Multiply,id:3805,x:33240,y:32297,varname:node_3805,prsc:2|A-3053-OUT,B-4207-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1705,x:33099,y:32696,ptovrint:False,ptlb:metall,ptin:_metall,varname:node_1705,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:4207,x:32884,y:32277,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_4207,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Vector1,id:9398,x:32486,y:32767,varname:node_9398,prsc:2,v1:0.5;n:type:ShaderForge.SFN_ValueProperty,id:2188,x:32672,y:32780,ptovrint:False,ptlb:noise str,ptin:_noisestr,varname:node_4330,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Lerp,id:3053,x:33075,y:32337,varname:node_3053,prsc:2|A-9398-OUT,B-4547-OUT,T-2188-OUT;n:type:ShaderForge.SFN_Vector4,id:9482,x:32941,y:33209,varname:node_9482,prsc:2,v1:0,v2:0,v3:1,v4:0;n:type:ShaderForge.SFN_ValueProperty,id:1706,x:33135,y:33305,ptovrint:False,ptlb:normal str,ptin:_normalstr,varname:node_4668,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Lerp,id:538,x:33359,y:32824,varname:node_538,prsc:2|A-9482-OUT,B-2746-RGB,T-1706-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7497,x:33298,y:32732,ptovrint:False,ptlb:ambient light,ptin:_ambientlight,varname:node_7497,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Add,id:5044,x:33301,y:32011,varname:node_5044,prsc:2|A-1428-OUT,B-2176-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2176,x:33113,y:32094,ptovrint:False,ptlb:metall contrast,ptin:_metallcontrast,varname:node_2176,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Lerp,id:7442,x:33321,y:32157,varname:node_7442,prsc:2|A-1428-OUT,B-5044-OUT,T-7213-B;n:type:ShaderForge.SFN_SwitchProperty,id:1057,x:33657,y:32077,ptovrint:False,ptlb:dirt add,ptin:_dirtadd,varname:node_1057,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-7442-OUT,B-9216-OUT;n:type:ShaderForge.SFN_Lerp,id:9216,x:33531,y:31805,varname:node_9216,prsc:2|A-7442-OUT,B-4977-RGB,T-9461-OUT;n:type:ShaderForge.SFN_Multiply,id:9940,x:33168,y:31886,varname:node_9940,prsc:2|A-1083-OUT,B-7213-A;n:type:ShaderForge.SFN_Clamp01,id:9461,x:33339,y:31886,varname:node_9461,prsc:2|IN-9940-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1083,x:32967,y:31826,ptovrint:False,ptlb:dirt str,ptin:_dirtstr,varname:node_1083,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Color,id:4977,x:33126,y:31733,ptovrint:False,ptlb:dirt color,ptin:_dirtcolor,varname:node_4977,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;proporder:236-3946-5277-7213-2746-4207-1705-1706-2188-7497-2176-1057-1083-4977;pass:END;sub:END;*/

Shader "Custom/Base_material" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _DiffuseColor ("Diffuse Color", Color) = (1,1,1,1)
        _Tesselation ("Tesselation", Float ) = 1
        _SRM ("SRM", 2D) = "white" {}
        _normal ("normal", 2D) = "bump" {}
        _Gloss ("Gloss", Float ) = 1
        _metall ("metall", Float ) = 1
        _normalstr ("normal str", Float ) = 0
        _noisestr ("noise str", Float ) = 0
        _ambientlight ("ambient light", Float ) = 0
        _metallcontrast ("metall contrast", Float ) = 0
        [MaterialToggle] _dirtadd ("dirt add", Float ) = 1
        _dirtstr ("dirt str", Float ) = 1
        _dirtcolor ("dirt color", Color) = (0,0,0,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
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
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _DiffuseColor;
            uniform float _Tesselation;
            uniform sampler2D _SRM; uniform float4 _SRM_ST;
            uniform sampler2D _normal; uniform float4 _normal_ST;
            uniform float _metall;
            uniform float _Gloss;
            uniform float _noisestr;
            uniform float _normalstr;
            uniform float _ambientlight;
            uniform float _metallcontrast;
            uniform fixed _dirtadd;
            uniform float _dirtstr;
            uniform float4 _dirtcolor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 node_1167 = (i.uv0*_Tesselation);
                float3 _normal_var = UnpackNormal(tex2D(_normal,TRANSFORM_TEX(node_1167, _normal)));
                float3 normalLocal = lerp(float4(0,0,1,0),float4(_normal_var.rgb,0.0),_normalstr);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float4 _SRM_var = tex2D(_SRM,TRANSFORM_TEX(node_1167, _SRM));
                float gloss = (lerp(0.5,(1.0 - _SRM_var.g),_noisestr)*_Gloss);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_2225 = (_SRM_var.b*_metall);
                float3 specularColor = float3(node_2225,node_2225,node_2225);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                indirectDiffuse += float3(_ambientlight,_ambientlight,_ambientlight); // Diffuse Ambient Light
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(node_1167, _Diffuse));
                float3 node_1428 = (_Diffuse_var.rgb*_DiffuseColor.rgb);
                float3 node_7442 = lerp(node_1428,(node_1428+_metallcontrast),_SRM_var.b);
                float3 diffuseColor = lerp( node_7442, lerp(node_7442,_dirtcolor.rgb,saturate((_dirtstr*_SRM_var.a))), _dirtadd );
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
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
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _DiffuseColor;
            uniform float _Tesselation;
            uniform sampler2D _SRM; uniform float4 _SRM_ST;
            uniform sampler2D _normal; uniform float4 _normal_ST;
            uniform float _metall;
            uniform float _Gloss;
            uniform float _noisestr;
            uniform float _normalstr;
            uniform float _metallcontrast;
            uniform fixed _dirtadd;
            uniform float _dirtstr;
            uniform float4 _dirtcolor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 node_1167 = (i.uv0*_Tesselation);
                float3 _normal_var = UnpackNormal(tex2D(_normal,TRANSFORM_TEX(node_1167, _normal)));
                float3 normalLocal = lerp(float4(0,0,1,0),float4(_normal_var.rgb,0.0),_normalstr);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float4 _SRM_var = tex2D(_SRM,TRANSFORM_TEX(node_1167, _SRM));
                float gloss = (lerp(0.5,(1.0 - _SRM_var.g),_noisestr)*_Gloss);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_2225 = (_SRM_var.b*_metall);
                float3 specularColor = float3(node_2225,node_2225,node_2225);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(node_1167, _Diffuse));
                float3 node_1428 = (_Diffuse_var.rgb*_DiffuseColor.rgb);
                float3 node_7442 = lerp(node_1428,(node_1428+_metallcontrast),_SRM_var.b);
                float3 diffuseColor = lerp( node_7442, lerp(node_7442,_dirtcolor.rgb,saturate((_dirtstr*_SRM_var.a))), _dirtadd );
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
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
