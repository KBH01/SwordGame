Shader "Hidden/PostPRocessingShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Tint ("Tint", Color) = (1, 0, 0, 1)
        _Intensity ("Intensity", float) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _Intensity;
            fixed4 _Tint;

            float2 Random(float seed)
            {
                return float2(
                    sin(dot(float4(152.6278 * seed, 73.7891 / seed, 6157.1 + seed, 389.0), float4(14, 78.3114, 7485.48758, 703.0) * seed)),
                    cos(dot(float4(7486.1, 789.124, 789.54, 142) * seed + seed, float4(41 / seed, 531.1 - seed, 0.124, 135 * seed) * seed)));
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv + Random(i.uv.x * (_Time.x * 2) * i.uv.y) * _Intensity * 0.05);
                col += _Tint * _Intensity * 2;
                return col;
            }
            ENDCG
        }
    }
}
