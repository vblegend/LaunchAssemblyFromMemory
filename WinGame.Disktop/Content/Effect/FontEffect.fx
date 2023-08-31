#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	//	float4 color = tex2D(SpriteTextureSampler,input.TextureCoordinates);
	//	float4 clr;

	//	clr.rgb = input.Color.rgb;
	//	clr.a = color.a;
	//	return clr;





	//	float mask = tex2D(SpriteTextureSampler,input.TextureCoordinates).a;
	//	float4 clr;
	//	clr.rgb = input.Color.rgb;
	//	if( mask < 0.2 )
	//		discard;
	//	else  
	//		clr.a = 1.0;
	//	// do some anti-aliasing
	//	clr.a *= smoothstep(0, 1.00, mask);
	//	return clr;




	   
	    float4 color = tex2D(SpriteTextureSampler,input.TextureCoordinates);
	    color.rgb*= input.Color.rgb;
	    return color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};