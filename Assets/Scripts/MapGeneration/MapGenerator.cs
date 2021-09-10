using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private const int MapChunkSize = 241;

    public enum DrawMode {NoiseMap, ColorMap, Mesh};
    public DrawMode drawMode;

    public bool autoUpdate;
    [Range(0,6)]
    public int levelOfDetail;

    [SerializeField] private int seed;
    [SerializeField] private float noiseScale;

    [SerializeField] private int octaves;
    [Range(0, 1)]
    [SerializeField] private float persistance;
    [SerializeField] private float lacunarity;

    [SerializeField] private Vector2 offset;
    [SerializeField] private float heightMultiplier;
    [SerializeField] private AnimationCurve heightCurve;

    [SerializeField] private MapDisplay display;
    [SerializeField] private Gradient terrains;

    public void GenerateMap()
    {
        float [,] noiseMap = ComputeUtil.GenerateNoiseMap(MapChunkSize, MapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);
        Color[] colorMap = new Color[MapChunkSize * MapChunkSize];

        for (int y = 0; y < MapChunkSize; y++)
        {
            for (int x = 0; x < MapChunkSize; x++)
            {
                float height = noiseMap[x, y];

                colorMap[y * MapChunkSize + x] = terrains.Evaluate(height);
            }
        }

        if (drawMode == DrawMode.NoiseMap)
            display.DrawTexture(GraphicsUtil.TextureFromHeightMap(noiseMap));
        else if (drawMode == DrawMode.ColorMap)
            display.DrawTexture(GraphicsUtil.TextureFromColorMap(colorMap, MapChunkSize, MapChunkSize));
        else if (drawMode == DrawMode.Mesh)
            display.DrawMesh(GraphicsUtil.GenerateTerrainMesh(noiseMap, heightMultiplier, heightCurve, levelOfDetail), GraphicsUtil.TextureFromColorMap(colorMap, MapChunkSize, MapChunkSize));
    }

    private void OnValidate()
    {
        if (lacunarity < 1)
            lacunarity = 1;
        if (octaves < 0)
            octaves = 0;
    }
}
