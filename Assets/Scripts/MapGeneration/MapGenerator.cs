using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private const int MapChunkSize = 255;

    public enum DrawMode {Mesh};
    public DrawMode drawMode;

    public bool autoUpdate;
    [Range(0,6)]
    public int levelOfDetail;
    public bool useFallOffMap;

    [SerializeField] private int seed;
    [SerializeField] private float noiseScale;

    [SerializeField] private int octaves;
    [Range(0, 1)]
    [SerializeField] private float persistance;
    [SerializeField] private float lacunarity;

    [SerializeField] private Vector2 offset;
    [SerializeField] private float heightMultiplier;
    [SerializeField] private AnimationCurve heightCurve;

    [SerializeField] private Biome biome;

    private float[,] fallOffMap = ComputeUtil.GenerateFalloffMap(MapChunkSize);

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        float [,] noiseMap = ComputeUtil.GenerateNoiseMap(MapChunkSize, MapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);
        Mesh terrainMesh = GraphicsUtil.GenerateTerrainMesh(noiseMap, heightMultiplier, heightCurve, levelOfDetail);
        
        biome.SetMesh(terrainMesh, heightMultiplier);
    }

    private void OnValidate()
    {
        if (lacunarity < 1)
            lacunarity = 1;
        if (octaves < 0)
            octaves = 0;
    }
}
