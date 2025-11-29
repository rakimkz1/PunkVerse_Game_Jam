void MainLight_float(float3 WorldPos, out float3 Direction, out float3 Color, out float DistanceAtten, out ShadowAtten)
{
#ifdef SHADERGRAPH_PREVIEW
    Direction = normalize(float3(0.5f, 0.5f, 0.25f));
    Color = float3(1.0f, 1.0f, 1.0f);
    DistanceAtten = 1.0f;
    ShadowAtten = 1.0f;
#else
    
#if SHADOWS_SCREEN
    half4 clipPos = TransformWorldToClip(WorldPos);
    half4 shadowCoord = ComputeScreenPos(clipPos);
#else
    half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif
    Light mainLignt = GetMainLight(shadowCoord);
    
    Direction = mainLignt.direction;
    Color = mainLignt.color;
    DistanceAtten = mainLignt.distanceAttenuation;
    ShadowAtten = mainLignt.shadowAttenuation;
#endif
}