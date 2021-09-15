using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] private float endHeightPercentage;

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;
    private float endHeight;

    private void Awake()
    {
        endHeight = endHeightPercentage;
    }

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
    }

    public void SetHeightMultiplier(float heightMultiplier)
    {
        this.endHeight = heightMultiplier * endHeightPercentage;
    }

    public void SetMesh(Mesh mesh)
    {
        if (meshFilter == null)
            meshFilter = GetComponent<MeshFilter>();
        if (meshCollider == null)
            meshCollider = GetComponent<MeshCollider>();

        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    public bool IsAbove(Vector3[] vertices, params int[] triangles)
    {
        foreach (int triangle in triangles)
        {
            if (!IsAbove(vertices[triangle]))
                return false;
        }

        return true;
    }

    public bool IsAbove(Vector2 vertex)
    {
        return IsAbove(vertex.y);
    }

    public bool IsAbove(float height)
    {
        return endHeight >= height;
    }
}
