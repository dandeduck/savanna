using UnityEngine;

public class Obstructing : MonoBehaviour
{
    [SerializeField] private Shader transparentShader;
    private const float transparentAlpha = 0.1f;

    private Renderer objectRenderer;

    private Material[] regularMaterials;
    private Material[] transparentMaterials;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        regularMaterials = objectRenderer.materials;
        transparentMaterials = MakeMaterialsTransparent(regularMaterials);
    }

    public void MakeTransperent()
    {
        objectRenderer.materials = transparentMaterials;
    }

    public void MakeVisible()
    {
        objectRenderer.materials = regularMaterials;
    }

    private Material[] MakeMaterialsTransparent(Material[] materials)
    {
        Material[] transparent = new Material[materials.Length];

        for (int i = 0; i < materials.Length; i++)
        {
            transparent[i] = MakeTransperent(materials[i]);
        }

        return transparent;
    }

    private Material MakeTransperent(Material material)
    {
        Material transparent = new Material(material);
        Color tranparentColor = material.color;
        tranparentColor.a = transparentAlpha;

        transparent.SetColor("_Color", tranparentColor);
        transparent.shader = transparentShader;
        
        return transparent;
    }
}
