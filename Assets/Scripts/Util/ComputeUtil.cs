using UnityEngine;

public static class ComputeUtil
{
    private const int OFFSET_RANGE = 100000;

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];
        Vector2[] octaveOffsets = GenerateOctaveOffsets(seed, octaves, offset);
        
        if (scale <= 0)
            scale = 0.0001f;
        
        FillNoiseMap(noiseMap, mapWidth, mapHeight, scale, octaves, persistance, lacunarity, octaveOffsets);
        
        return noiseMap;
    }

    private static void FillNoiseMap(float[,] noiseMap, int mapWidth, int mapHeight, float scale, int octaves, float persistance, float lacunarity, Vector2[] octaveOffsets)
    {
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float noiseHeight = CalcNoiseHeight(x, y, halfWidth, halfHeight, scale, octaves, persistance, lacunarity, octaveOffsets);

                if (noiseHeight > maxNoiseHeight)
                    maxNoiseHeight = noiseHeight;
                else if (noiseHeight < minNoiseHeight)
                    minNoiseHeight = noiseHeight;  

                noiseMap[x, y] = noiseHeight;
            }
        }
        NormalizeNoiseMap(noiseMap, mapWidth, mapHeight, minNoiseHeight, maxNoiseHeight);
    }

    private static Vector2[] GenerateOctaveOffsets(int seed, int octaves, Vector2 offset)
    {
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-OFFSET_RANGE, OFFSET_RANGE) + offset.x;
            float offsetY = prng.Next(-OFFSET_RANGE, OFFSET_RANGE) + offset.y;

            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        return octaveOffsets;
    }

    private static float CalcNoiseHeight(int x, int y, float halfWidth, float halfHeight, float scale, int octaves, float persistance, float lacunarity, Vector2[] octaveOffsets)
    {
        float amplitude = 1;
        float frequency = 1;
        float noiseHeight = 0;

        for (int i = 0; i < octaves; i++)
        {   
            float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
            float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

            float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

            noiseHeight += perlinValue * amplitude;
            amplitude *= persistance;
            frequency *= lacunarity;
        }

        return noiseHeight; 
    }

    private static void NormalizeNoiseMap(float[,] noiseMap, int mapWidth, int mapHeight, float minNoiseHeight, float maxNoiseHeight)
    {
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x,y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
    }
}
