Shader "MyShaders/Glow"
{
    Properties
    {
         _MainTex ("Texture", 2D) = "white"{}
        _Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _ColorToChange ("Color To Change", Color) = (1.0, 1.0, 1.0, 1.0)
        [Toggle(Should_Use_glow)]
        _ShouldUseGlow ("Should Use Glow", Range(0.0, 1.0)) = 1.0

        _GlowIntensity ("Glow Intensity", Range(0.0, 3.0)) = 1.0

        Kd("Kd(diffuse)", Color) = (1.0, 1.0, 1.0, 1.0)

        specularShiness("Brilho Especular", Range(0.0, 1024.0)) = 0.5
    }

    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        LOD 100
        Cull Off
        ZWrite On
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag            
            #include "Lighting.cginc"

            sampler2D _MainTex;
            half4 _MainTex_ST;
            half4 _Color;
            half4 _ColorToChange;
            half _GlowIntensity;
            half _ShouldUseGlow;

            half4 Kd;
            half specularShiness;

            struct appdata
            {
                half4 pos : POSITION;
                half2 tex : TEXCOORD0;                
                half3 normal : NORMAL0;
            };

            struct v2f
            {
                half4 pos : SV_POSITION;
                half2 tex : TEXCOORD0;
                half3 normal : TEXCOORD1;
                half3 view : TEXCOORD2;
            };

            v2f vert (appdata inputFromApp)
            {
                v2f v;
                v.pos = UnityObjectToClipPos(inputFromApp.pos);
                v.tex = inputFromApp.tex * _MainTex_ST.xy + _MainTex_ST.zw;
                v.normal = normalize(mul(inputFromApp.normal, unity_ObjectToWorld));
                v.view = normalize(_WorldSpaceCameraPos - v.normal);
                return v;
            }

            half4 frag (v2f i) : SV_TARGET
            {
                half4 textureGlow = tex2D(_MainTex, i.tex);

                half4 lightDirectionNormallized = normalize(_WorldSpaceLightPos0);
                half3 reflection = normalize(2 * dot(i.normal, lightDirectionNormallized) * i.normal - lightDirectionNormallized);

                half specular = pow(max(dot(-reflection, i.view),0.0), specularShiness);
                half4 diffuse = max(dot(i.normal, lightDirectionNormallized), 0.0);

                half4 diffuseComponent = Kd * _LightColor0 * diffuse;
                half4 specularComponent = _Color * _LightColor0 * specular;

                half4 lighting =  saturate(specularComponent) + saturate(diffuseComponent);

                if((textureGlow.x - _ColorToChange.x < 0.1 && textureGlow.x - _ColorToChange.x > -0.1) 
                && (textureGlow.y - _ColorToChange.y < 0.1 && textureGlow.y - _ColorToChange.y > -0.1) 
                && (textureGlow.z - _ColorToChange.z < 0.1 && textureGlow.z - _ColorToChange.z > -0.1)){
                    half4 toReturn = _Color;
                    toReturn.w = 1.0;
                    return toReturn;

                }else{                    
                    return textureGlow;
                }
            }
            ENDCG
        }
    }
}
