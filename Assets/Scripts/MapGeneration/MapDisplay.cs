using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    private Renderer textureRenderer;
    
    private void Start()
    {
        textureRenderer = GetComponent<Renderer>();
    }

    public void DrawTexture(Texture2D texture)
    {
         if (textureRenderer == null)
            textureRenderer = GetComponent<Renderer>();

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

}
