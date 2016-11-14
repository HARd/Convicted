Shader "Color Space/YUVtoRGB" 
{
    Properties 
    {
        _YTex ("Y (RGB)", 2D) = "black" {}
        _UTex ("U (RGB)", 2D) = "black" {}
        _VTex ("V (RGB)", 2D) = "black" {}
        
        _Opacity ("Opacity", Range (0,1.0)) = 1.0
    }
    SubShader 
    {
		Tags {  "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Pass 
        {
        	Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Lighting Off Fog { Color (0,0,0,0) }
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _YTex;
			sampler2D _UTex;
			sampler2D _VTex;
			float _Opacity;
			
			struct v2f 
			{
				float4  pos : SV_POSITION;
				half2  uvY : TEXCOORD0;
				half2  uvCbCr : TEXCOORD1;
			};

			v2f vert (appdata_base v)
			{
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.uvY = float2(v.texcoord.x, v.texcoord.y); // хз почему в юнити текстуры верхтормашками...
				return o;
			}

			float r,g,b, y,cb,cr;
			fixed4 frag (v2f i) : COLOR
			{		
				// приведение компоненты Y из диапазона 601 (16-235) в полный (0-255)
				// подробнее см. http://www.digitalvideo.ru/archiv/005/00528.htm
				// используются предрасчитанные константы 16 / 255 = 0.062,  (235 - 16) / 255 = 1.164
				y = ( tex2D (_YTex, i.uvY).a - 0.062 ) * 1.164;
				cb = tex2D (_UTex, i.uvY).a;
				cr = tex2D (_VTex, i.uvY).a;
				
				r = y + 1.402 * (cr - 0.5);
				g = y - 0.344 * (cb - 0.5) - 0.714 * (cr - 0.5);
				b = y + 1.772 * (cb - 0.5);
				
				fixed4 rgbVec = fixed4(r, g, b, _Opacity);
				//fixed4 rgbVec = fixed4(i.uvY.x, i.uvY.y, 0.0f, 1.0f);

				return rgbVec;
			}
			ENDCG
		}
	}
	FallBack "Legacy Shaders/Transparent/Cutout/VertexLit"
}


