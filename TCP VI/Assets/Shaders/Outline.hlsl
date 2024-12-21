#ifndef OUTLINE_INCLUDED
#define OUTLINE_INCLUDED

//include urp core
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

struct Attributes
{
    float4 positionOS   : POSITION;
    float3 normalOS     : NORMAL;
};

struct VertexOutput
{
    float4 positionCS   : SV_POSITION;
};

// Properties
float _Thickness;
float4 _Color;
int _Visible;

VertexOutput Vertex(Attributes input)
{
    VertexOutput output = (VertexOutput)0;
    
    float3 normalOS = input.normalOS;
    
    float3 posOS = input.positionOS.xyz + normalOS * _Thickness;

    output.positionCS = GetVertexPositionInputs(posOS).positionCS;

    return output;
}

float4 Fragment(VertexOutput input) : SV_Target {
    // Check if _Visible is set to 0 and discard the fragment if so
    if (_Visible == 0)
    {
        discard;
    }
    
    // Return the color if visible
    return _Color;
}

#endif