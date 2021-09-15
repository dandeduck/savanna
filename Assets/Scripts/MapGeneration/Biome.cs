using UnityEngine;
using System.Collections.Generic;

public class Biome : MonoBehaviour
{
    private Terrain[] terrains;

    private void Start()
    {
        terrains = GetComponentsInChildren<Terrain>();
    }

    public void SetMesh(Mesh mesh, float heightMultiplier)
    {
        if (terrains != null || terrains.Length == 0)
            terrains = GetComponentsInChildren<Terrain>();  
        
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uv = mesh.uv;

        SetHeightMultipliers(heightMultiplier);

        List<int>[] terrainTriangles = DivideTriangles(triangles, vertices);

        SetTerrainMeshes(terrainTriangles, vertices);        
    }

    private void SetHeightMultipliers(float heightMultiplier)
    {
        foreach (Terrain terrain in terrains)
        {
            terrain.SetHeightMultiplier(heightMultiplier);
        }
    }

    private List<int>[] DivideTriangles(int[] triangles, Vector3[] vertices)
    {
        List<int>[] terrainTriangles = InitializeTerrainTriangles();

        for (int i = 0; i < triangles.Length; i+=3) {
            for (int terrainIndex = 0; terrainIndex < terrainTriangles.Length; terrainIndex++)
            {
                if (terrains[terrainIndex].IsAbove(vertices, triangles[i], triangles[i+1], triangles[i+2]))
                {
                    terrainTriangles[terrainIndex].Add(triangles[i]);
                    terrainTriangles[terrainIndex].Add(triangles[i+1]);
                    terrainTriangles[terrainIndex].Add(triangles[i+2]);
                    break;
                }
            }
        }

        return terrainTriangles;
    }

    private List<int>[] InitializeTerrainTriangles()
    {
        List<int>[] terrainTriangles = new List<int>[terrains.Length];

        for (int i = 0; i < terrainTriangles.Length; i++)
        {
            terrainTriangles[i] = new List<int>();
        }

        return terrainTriangles;
    }

    private void SetTerrainMeshes(List<int>[] terrainTriangles, Vector3[] vertices)
    {
        for (int i = 0; i < terrains.Length; i++)
        {
            terrains[i].SetMesh(GenerateTerrainMesh(terrainTriangles[i], vertices));
        }
    }

    private Mesh GenerateTerrainMesh(List<int> triangles, Vector3[] originalVertices)
    {
        List<Vector3> vertices = new List<Vector3>();

        TranslateMesh(triangles, vertices, originalVertices);

        return GenerateMesh(triangles, vertices);
    }

    private void TranslateMesh(List<int> triangles, List<Vector3> vertices, Vector3[] oldVertices)
    {
        Dictionary<int,int> triangleTranslations = new Dictionary<int,int>();
        
        for (int i = 0; i < triangles.Count; i+=3) {
            if (!triangleTranslations.ContainsKey(triangles[i+0])) 
            {
                triangleTranslations[triangles[i+0]] = vertices.Count;
                vertices.Add(oldVertices[triangles[i+0]]);
            }
            triangles[i+0] = triangleTranslations[triangles[i+0]];
        
            if (!triangleTranslations.ContainsKey(triangles[i+1])) 
            {
                triangleTranslations[triangles[i+1]] = vertices.Count;
                vertices.Add(oldVertices[triangles[i+1]]);
            }
            triangles[i+1] = triangleTranslations[triangles[i+1]];
        
            if (!triangleTranslations.ContainsKey(triangles[i+2])) 
            {
                triangleTranslations[triangles[i+2]] = vertices.Count;
                vertices.Add(oldVertices[triangles[i+2]]);
            }
            triangles[i+2] = triangleTranslations[triangles[i+2]];
        }
    }

    private Mesh GenerateMesh(List<int> triangles, List<Vector3> vertices)
    {
        Mesh mesh = new Mesh();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}
