sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float2 uTargetPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;
float4 uLegacyArmorSourceRect;
float2 uLegacyArmorSheetSize;

float4 PixelSnap(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    return sampleColor;
}

technique Technique1
{
    pass WavyPass
    {
        PixelShader = compile ps_2_0 PixelSnap();
    }
}