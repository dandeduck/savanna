using UnityEngine;

public static class GraphicsUtil
{
    public static void RemoveSharedVertices(Mesh mesh)
    {
        Vector3[] oldVertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        Vector3[] vertices = new Vector3[triangles.Length];

        for (int i = 0; i < triangles.Length; i++) {
            vertices[i] = oldVertices[triangles[i]];
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}
