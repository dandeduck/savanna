using UnityEngine;

public class Obstructing : MonoBehaviour
{
    [SerializeField] private Shader transparentShader;
    [SerializeField] private float transparentAlpha;

    private Renderer objectRenderer;
    private Collider objectCollider;

    private Material[] regularMaterials;
    private Material[] transparentMaterials;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();

        regularMaterials = objectRenderer.materials;
        transparentMaterials = MakeMaterialsTransparent(regularMaterials);
    }

    public void MakeTransperent()
    {
        objectRenderer.materials = transparentMaterials;
        objectCollider.isTrigger = true;
    }

    public void MakeVisible()
    {
        objectRenderer.materials = regularMaterials;
        objectCollider.isTrigger = false;
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
