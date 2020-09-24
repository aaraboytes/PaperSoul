void GetLightingInformation_float(out float3 Direction, out float3 Color,out float Attenuation)
{
    Direction = float3(-0.5, 0.5, -0.5);
    Color = float3(1, 1, 1);
    Attenuation = 0.4;
    #ifdef SHADERGRAPH_PREVIEW
        
    #else
        /*Light light = GetMainLight();
        Direction = light.direction;
        Attenuation = light.distanceAttenuation;
        Color = light.color;*/
    #endif
}