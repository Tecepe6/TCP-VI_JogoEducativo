Shader "CustomShaders/Outline"
{
    Properties
    {
        _Thickness("Thickness", Float) = 1
        _Color("Color", Color) = (1, 1, 1, 1)
        _Visible("Visible", Int) = 1
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            Name "Outline"

            //Culling of front faces
            Cull front
            
            HLSLPROGRAM
            //URP defaults:
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            //our functions
            #pragma vertex Vertex
            #pragma fragment Fragment

            //ref or hlsl file
            #include "Outline.hlsl"

            ENDHLSL
        }
    }
}
