using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public bool autoUpdate;

    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;
    [SerializeField] private int seed;
    [SerializeField] private float noiseScale;

    [SerializeField] private int octaves;
    [Range(0, 1)]
    [SerializeField] private float persistance;
    [SerializeField] private float lacunarity;

    [SerializeField] private Vector2 offset;

    [SerializeField] private MapDisplay display;

    public void GenerateMap()
    {
        float [,] noiseMap = ComputeUtil.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        display.DrawnNoiseMap(noiseMap);
    }

    private void OnValidate()
    {
        if (mapHeight < 1)
            mapHeight = 1;
        if (mapWidth < 1)
            mapWidth = 1;
        if (lacunarity < 1)
            lacunarity = 1;
        if (octaves < 0)
            octaves = 0;
    }
}
