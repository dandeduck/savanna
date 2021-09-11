using UnityEngine;
using System.Collections.Generic;

public class MapDisplay : MonoBehaviour
{
    public MeshRenderer lowerMeshRenderer;
    public MeshFilter lowerMeshFilter;
    public MeshCollider lowerMeshCollider;
    
    public MeshRenderer higherMeshRenderer;
    public MeshFilter higherMeshFilter;
    public MeshCollider higherMeshCollider;

    public void DrawMesh(MeshData meshData, Texture2D texture, Gradient terrains)
    {
        Mesh mesh = meshData.CreateMesh();

        List<int> upperTriangles = new List<int>();
        List<int> lowerTriangles = new List<int>();

        int[] oldTriangles = mesh.triangles;
        Vector3[] oldVertices = mesh.vertices;
        Vector2[] oldUv = mesh.uv;

        float threshold = 0.01f;

         for (int i = 0; i < oldTriangles.Length; i+=3) {
            if (oldVertices[oldTriangles[i+0]].y > threshold ||
                oldVertices[oldTriangles[i+1]].y > threshold ||
                oldVertices[oldTriangles[i+2]].y > threshold) {
        
                upperTriangles.Add(oldTriangles[i+0]);
                upperTriangles.Add(oldTriangles[i+1]);
                upperTriangles.Add(oldTriangles[i+2]);
            }
            else {
                lowerTriangles.Add(oldTriangles[i+0]);
                lowerTriangles.Add(oldTriangles[i+1]);
                lowerTriangles.Add(oldTriangles[i+2]);
            }
        }

        List<Vector2> upperUv = new List<Vector2>();
        List<Vector3> upperVertices = new List<Vector3>();
        
        List<Vector2> lowerUv = new List<Vector2>();
        List<Vector3> lowerVertices = new List<Vector3>();

        SetVerticesAndUvsFromTriangles(upperTriangles, upperVertices, upperUv, oldVertices, oldUv);
        SetVerticesAndUvsFromTriangles(lowerTriangles, lowerVertices, lowerUv, oldVertices, oldUv);

        Mesh upperMesh = new Mesh();
        Mesh lowerMesh = new Mesh();

        lowerMesh.vertices = lowerVertices.ToArray();
        lowerMesh.triangles = lowerTriangles.ToArray();
        lowerMesh.uv = lowerUv.ToArray();
        lowerMesh.RecalculateNormals();
        lowerMesh.RecalculateBounds();

        upperMesh.vertices = upperVertices.ToArray();
        upperMesh.triangles = upperTriangles.ToArray();
        upperMesh.uv = upperUv.ToArray();
        upperMesh.RecalculateNormals();
        upperMesh.RecalculateBounds();

        higherMeshRenderer.sharedMaterial.mainTexture = texture;
        lowerMeshRenderer.sharedMaterial.mainTexture = texture;

        higherMeshFilter.sharedMesh = upperMesh;
        higherMeshCollider.sharedMesh = upperMesh;

        lowerMeshFilter.sharedMesh = lowerMesh;
        lowerMeshCollider.sharedMesh = lowerMesh;
    }

    private void SetVerticesAndUvsFromTriangles(List<int> triangles, List<Vector3> vertices, List<Vector2> uv, Vector3[] oldVertices, Vector2[] oldUv)
    {
        Dictionary<int,int> triangleTranslations = new Dictionary<int,int>();
        
        for (int i = 0; i < triangles.Count; i+=3) {
            if (!triangleTranslations.ContainsKey(triangles[i+0])) 
            {
                triangleTranslations[triangles[i+0]] = vertices.Count;
                vertices.Add(oldVertices[triangles[i+0]]);
                uv.Add(oldUv[triangles[i+0]]);
            }
            triangles[i+0] = triangleTranslations[triangles[i+0]];
        
            if (!triangleTranslations.ContainsKey(triangles[i+1])) 
            {
                triangleTranslations[triangles[i+1]] = vertices.Count;
                vertices.Add(oldVertices[triangles[i+1]]);
                uv.Add(oldUv[triangles[i+1]]);
            }
            triangles[i+1] = triangleTranslations[triangles[i+1]];
        
            if (!triangleTranslations.ContainsKey(triangles[i+2])) 
            {
                triangleTranslations[triangles[i+2]] = vertices.Count;
                vertices.Add(oldVertices[triangles[i+2]]);
                uv.Add(oldUv[triangles[i+2]]);
            }
            triangles[i+2] = triangleTranslations[triangles[i+2]];
        }
    }
}
