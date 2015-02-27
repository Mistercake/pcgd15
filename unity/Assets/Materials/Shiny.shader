  Shader "Custom/Shiny" {
    Properties {
      _Detail ("Detail", 2D) = "gray" {}
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert finalcolor:mycolor
      struct Input {
          float4 screenPos;
      };
	  sampler2D _Detail;
	  void mycolor (Input IN, SurfaceOutput o, inout fixed4 color)
      {
		  float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
          screenUV *= float2(40,40);
          color.rgb = tex2D (_Detail, screenUV).rgb;
      }
      void surf (Input IN, inout SurfaceOutput o) { }
      ENDCG
    } 
    Fallback "Diffuse"
  }