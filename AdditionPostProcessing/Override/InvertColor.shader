Shader "Post Process/Invert Color"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
	SubShader
	{
        Tags {"RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"}
	    ZTest Always Cull Off ZWrite Off

        Pass
        {
			Name "Invert Color"

			HLSLINCLUDE
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			ENDHLSL
            			
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct a2v
			{
				float4 positionOS : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _MainTex_ST;
			CBUFFER_END

			TEXTURE2D(_MainTex);
			SAMPLER(sampler_MainTex);

			v2f vert(a2v v)
			{
				v2f o;
				o.pos = TransformObjectToHClip(v.positionOS.xyz);
				o.uv = v.uv;
				return o;
			}

			half4 frag(v2f i) : SV_Target
			{
				half4 color = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, i.uv);
				color = 1 - color;
				return color;
			}
			ENDHLSL
	     }
	}
}
