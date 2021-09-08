using UnityEngine;
using System.Collections;

public class Obstruction : MonoBehaviour
{
    [SerializeField] private float fadeInSpeed = 20f;
    [SerializeField] private float fadeOutSpeed = 1000f;
    [SerializeField] private float opacity = 0.1f;

    private Material[] materials;
    private bool isOpaque;

    private Color[] transparentColors;
    private Color[] opaqueColors;

    private void Start()
    {
        materials = GetComponent<Renderer>().materials;
        isOpaque = true;

        transparentColors = MakeTransparentColors(materials);
        opaqueColors = MakeOpaqueColors(materials);
    }

    public void MakeTransparent()
    {
        if (isOpaque)
        {
            MakeMaterialsTransparent();
            StartCoroutine(FadeOut());
        }
    }

    public void MakeVisible()
    {
        if (!isOpaque)
            StartCoroutine(FadeIn());    
    }

    private IEnumerator FadeIn()
    {
        while (!HasReachedOpaqueness())
        {
            LerpOpaqueness();
            yield return null;
        }
    
        MakeMaterialsOpaque();
    }

    private IEnumerator FadeOut()
    {
        while (!HasReachedTransparency())
        {
            LerpTransparency();
            yield return null;
        }
    }

    private bool HasReachedOpaqueness()
    {
        return materials[materials.Length-1].color.a >= 0.99f;   
    }

    private bool HasReachedTransparency()
    {
        return materials[materials.Length-1].color.a <= opacity + 0.01f;   
    }

    private Color[] MakeTransparentColors(Material[] materials)
    {
        Color[] colors = new Color[materials.Length];

        for (int i = 0; i < materials.Length; i++)
        {
            Color materialColor = materials[i].color;

            colors[i] = new Color(materialColor.r, materialColor.g, materialColor.b, opacity);
        }

        return colors;
    }

    private Color[] MakeOpaqueColors(Material[] materials)
    {
        Color[] colors = new Color[materials.Length];

        for (int i = 0; i < materials.Length; i++)
        {
            Color materialColor = materials[i].color;

            colors[i] = new Color(materialColor.r, materialColor.g, materialColor.b, 1f);
        }

        return colors;
    }

    private void LerpTransparency()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = Color.Lerp(materials[i].color, transparentColors[i], fadeInSpeed * Time.deltaTime);
        }
    }

    private void LerpOpaqueness()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = Color.Lerp(materials[i].color, opaqueColors[i], fadeOutSpeed * Time.deltaTime);
        }
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

    private void SetMaterialProperties(Material material)// -> https://answers.unity.com/questions/1608815/change-surface-type-with-lwrp.html
    {
        SurfaceType surfaceType = (SurfaceType) material.GetFloat("_Surface");

        if (surfaceType == SurfaceType.Opaque)
            SetupOpaqueMaterial(material);

        else 
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
