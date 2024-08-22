Shader "Custom/UnlitAlphaColorMaskShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _AlphaCutoff("Alpha Cutoff", Range(0, 1)) = 0.1
    }
        SubShader
        {
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                fixed4 _Color;
                float _AlphaCutoff;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 texColor = tex2D(_MainTex, i.uv) * _Color; // Multiplica pela cor
                    texColor.a = tex2D(_MainTex, i.uv).r; // Usa o canal vermelho para alpha

                    // Discard fragments based on the alpha cutoff value
                    if (texColor.a < _AlphaCutoff)
                        discard;

                    return texColor;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}