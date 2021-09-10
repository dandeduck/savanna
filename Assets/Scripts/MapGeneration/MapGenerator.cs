using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode {NoiseMap, ColorMap, Mesh};
    public DrawMode drawMode;

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
    [SerializeField] private Gradient terrains;

    public void GenerateMap()
    {
        float [,] noiseMap = ComputeUtil.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        Color[] colorMap = new Color[mapWidth * mapHeight];

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float height = noiseMap[x, y];

                colorMap[y * mapWidth + x] = terrains.Evaluate(height);
            }
        }

        if (drawMode == DrawMode.NoiseMap)
            display.DrawTexture(GraphicsUtil.TextureFromHeightMap(noiseMap));
        else if (drawMode == DrawMode.ColorMap)
            display.DrawTexture(GraphicsUtil.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        else if (drawMode == DrawMode.Mesh)
            display.DrawMesh(GraphicsUtil.GenerateTerrainMesh(noiseMap), GraphicsUtil.TextureFromColorMap(colorMap, mapWidth, mapHeight));
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
