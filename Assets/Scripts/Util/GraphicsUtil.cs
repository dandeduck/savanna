using UnityEngine;

public static class GraphicsUtil
{
    public static void RemoveSharedVertices(Mesh mesh)
    {
        Vector3[] oldVertices = mesh.vertices;
        Vector2[] oldUv = mesh.uv;
        int[] oldTriangles = mesh.triangles;
        
        int[] triangles = new int[oldTriangles.Length];
        Vector3[] vertices = new Vector3[oldTriangles.Length];
        Vector2[] uv = new Vector2[oldTriangles.Length];

        for (int i = 0; i < oldTriangles.Length; i++) {
            vertices[i] = oldVertices[oldTriangles[i]];
            uv[i] = oldUv[oldTriangles[i]];
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}
