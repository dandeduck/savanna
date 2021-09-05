using UnityEngine;

public class Obstruction : MonoBehaviour
{
    private Material[] materials;
    private bool isOpaque;

    private void Start()
    {
        materials = GetComponent<Renderer>().materials;
        isOpaque = true;
    }

    public bool IsOpaque()
    {
        return isOpaque;
    }

    public void MakeTransparent()
    {
        if (isOpaque)
            MakeMaterialsTransparent();
    }

    public void MakeVisible()
    {
        if (!isOpaque)
            MakeMaterialsOpaque();
    }

    private void MakeMaterialsTransparent()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            MakeMaterialTransparent(materials[i]);
            SetupMaterialBlendMode(materials[i]);
        }

        isOpaque = false;
    }

    private void MakeMaterialTransparent(Material material)
    {
        material.SetFloat("_Surface", (float) SurfaceType.Transparent);
        material.SetFloat("_Blend", (float) BlendMode.Alpha);
    }

    private void MakeMaterialsOpaque()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            MakeMaterialOpaque(materials[i]);
            SetupMaterialBlendMode(materials[i]);
        }

        isOpaque = true;
    }

    private void MakeMaterialOpaque(Material material)
    {
        material.SetFloat("_Surface", (float) SurfaceType.Opaque);
    }

    private void SetupMaterialBlendMode(Material material)
    {
        SetAlphaClip(material);
        SetMaterialProperties(material);
    }

    private void SetAlphaClip(Material material)
    {
        bool alphaClip = material.GetFloat("_AlphaClip") == 1;

        if (alphaClip)
            material.EnableKeyword("_ALPHATEST_ON");

        else
            material.DisableKeyword("_ALPHATEST_ON");
    }

    private void SetMaterialProperties(Material material)
    {
        SurfaceType surfaceType = (SurfaceType) material.GetFloat("_Surface");

        if (surfaceType == SurfaceType.Opaque)
            SetupOpaqueMaterial(material);

        else // -> https://answers.unity.com/questions/1608815/change-surface-type-with-lwrp.html
            SetupAlphaTransparentMaterial(material);
    }

    private void SetupOpaqueMaterial(Material material)
    {
        material.SetOverrideTag("RenderType", "");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }

    private void SetupAlphaTransparentMaterial(Material material)
    {
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        material.SetShaderPassEnabled("ShadowCaster", false);
    }
}
