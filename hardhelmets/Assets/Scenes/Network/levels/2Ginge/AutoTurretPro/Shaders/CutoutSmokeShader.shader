Shader "Custom/CutoutSmokeShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Cutoff("Cutoff", Range(0,1)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Lighting Off

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf NoLight addshadow

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float4 _VCOL : Color;
			float3 worldNormal;
			float3 worldPos;
			float3 localPos;
			INTERNAL_DATA
		};
		half _Cutoff;
		half4 colorA : COLOR;
		half4 _Color;
		half4 LightingNoLight(SurfaceOutput s, half3 lightDir, half atten)
		{
			return half4(s.Albedo, s.Alpha) * atten;
		}
		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			half4 c = length(tex2D (_MainTex, IN.uv_MainTex)) * _Color * IN._VCOL;
			//o.Albedo = (_Scalar / l);// * _Color * IN._VCOL * (1 + dot(IN.worldNormal, float3(0,1,0)))/2;
			o.Albedo = _Color * IN._VCOL * ((1 + dot(IN.worldNormal, UNITY_MATRIX_IT_MV[2]))/2);
			// Metallic and smoothness come from slider variables
			o.Alpha = c.a* (1 + dot(IN.worldNormal, UNITY_MATRIX_IT_MV[2]))/2;

			clip(pow(o.Alpha - _Cutoff,5));
		}
		ENDCG
	}
	FallBack "Diffuse"
}
