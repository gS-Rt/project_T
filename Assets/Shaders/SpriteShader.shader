/*
//문제: 동일 우선도 스프라이트끼리 겹침 문제
Shader "Custom/SpriteShader" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        [PerRendererData]_MainTex("Sprite Texture", 2D) = "white" {}
        _Cutoff("Shadow alpha cutoff", Range(0,1)) = 0.5

        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader{
            Tags
            {
                "Queue" = "Geometry"
                "RenderType" = "TransparentCutout"
            }
            LOD 200

            Cull Off
            //ZWrite Off //깊이값 체크해서 가장 가까운 것만 렌더링
            //ZTest NotEqual

            CGPROGRAM
            // Lambert lighting model, and enable shadows on all light types
            #pragma surface surf Lambert addshadow fullforwardshadows

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MainTex;
            fixed4 _Color;
            fixed _Cutoff;

            struct Input
            {
                float2 uv_MainTex;
            };


            void surf(Input IN, inout SurfaceOutput o) {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Alpha = c.a;
                clip(o.Alpha - _Cutoff);
            }
            ENDCG
        }
            FallBack "Diffuse"
}
*/
// Sprite Shadow Shader - AllenDevs
/*
//문제: 3d 오브젝트의 그림자를 못받음, 스프라이트 렌더링 우선순위가 확실하게 작동하지 않음
Shader "Sprites/Custom/SpriteShader"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
    _Color("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
    [PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
        _Cutoff("Alpha Cutoff", Range(0,1)) = 0.5
    }

        SubShader
    {
        Tags
    {
        "Queue" = "Transparent"
        "IgnoreProjector" = "True"
        "RenderType" = "Transparent"
        "PreviewType" = "Plane"
        "CanUseSpriteAtlas" = "True"
    }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        CGPROGRAM
#pragma surface surf Lambert vertex:vert alphatest:_Cutoff addshadow nofog nolightmap nodynlightmap keepalpha noinstancing
#pragma multi_compile_local _ PIXELSNAP_ON
#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
#include "UnitySprites.cginc"

        struct Input
    {
        float2 uv_MainTex;
        fixed4 color;
    };

    void vert(inout appdata_full v, out Input o)
    {
        v.vertex = UnityFlipSprite(v.vertex, _Flip);

#if defined(PIXELSNAP_ON)
        v.vertex = UnityPixelSnap(v.vertex);
#endif

        UNITY_INITIALIZE_OUTPUT(Input, o);
        o.color = v.color * _Color * _RendererColor;
    }

    void surf(Input IN, inout SurfaceOutput o)
    {
        fixed4 c = SampleSpriteTexture(IN.uv_MainTex) * IN.color;
        o.Albedo = c.rgb * c.a;
        o.Alpha = c.a;
    }
    ENDCG
    }

        Fallback "Transparent/VertexLit"
}
*/

Shader "Custom/SpriteShader" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        [PerRendererData]_MainTex("Sprite Texture", 2D) = "white" {}
        _Cutoff("Shadow alpha cutoff", Range(0,1)) = 0.5
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
    }
        SubShader{
            Tags
            {
                "Queue" = "Geometry"
                "RenderType" = "TransparentCutout"
                "IgnoreProjector" = "True"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
            }
            LOD 200

            Cull Off
            Lighting Off
            ZWrite Off
            Blend One OneMinusSrcAlpha

            CGPROGRAM
            // Lambert lighting model, and enable shadows on all light types
            #pragma surface surf Lambert addshadow fullforwardshadows

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MainTex;
            fixed4 _Color;
            fixed _Cutoff;

            struct Input
            {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutput o) {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Alpha = c.a;
                clip(o.Alpha - _Cutoff);
            }
            ENDCG
        }
            FallBack "Diffuse"
} //위의 두 코드 섞음, 스켈레톤 애니메이션 스프라이트 겹침 문제 수정
  //스프라이트 우선순위 렌더링 안되는 문제 포함된 코드

/*
Shader "Custom/SpriteShader" {
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
        _Cutoff("Alpha Cutoff", Range(0,1)) = 0.5

        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }

        SubShader
        {
            Tags
            {
                "Queue" = "Geometry"
                "IgnoreProjector" = "True"
                "RenderType" = "TransparentCutout"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
            }
            LOD 200

            Cull Off
            Lighting Off
            ZWrite Off
            Blend One OneMinusSrcAlpha


            CGPROGRAM
            #pragma surface surf Lambert addshadow fullforwardshadows

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MainTex;
            fixed4 _Color;
            fixed _Cutoff;

            struct Input
            {
                float2 uv_MainTex;
            };

            

            void surf(Input IN, inout SurfaceOutput o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Alpha = c.a;
                clip(o.Alpha - _Cutoff);
            }
            ENDCG
        }

        Fallback "Transparent/VertexLit"
}*/