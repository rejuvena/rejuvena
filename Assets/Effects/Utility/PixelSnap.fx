sampler2D input : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	uv = floor(uv * 48) / 48;

	float4 Color;
	Color = tex2D(input , uv.xy);

	return Color;
}