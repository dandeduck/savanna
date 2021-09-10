using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public bool autoUpdate;

    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;
    [SerializeField] private float noiseScale;
    [SerializeField] private MapDisplay display;

    public void GenerateMap()
    {
        float [,] noiseMap = ComputeUtil.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

        display.DrawnNoiseMap(noiseMap);
    }
}
