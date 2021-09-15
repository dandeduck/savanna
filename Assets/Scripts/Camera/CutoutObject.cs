using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Material[] obstructionMaterials;

    [SerializeField] private float cutoutRadius;
    [SerializeField] private float fallOffMargin;

    private Camera objCamera;
    private Material[] prevMaterials;

    private void Awake()
    {
        objCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Vector2 position = objCamera.WorldToViewportPoint(target.position);

        SetCutout(position, cutoutRadius, fallOffMargin);
    }

    private void OnDisable()
    {
        SetCutout(Vector2.zero, 0, 0);
    }

    private void SetCutout(Vector2 viewportPosition, float cutout, float falloff)
    {
        foreach (Material material in obstructionMaterials)
        {
            material.SetVector("_CutoutPos", viewportPosition);
            material.SetFloat("_CutoutRadius", cutout / objCamera.orthographicSize);
            material.SetFloat("_CutoutFalloff", falloff / objCamera.orthographicSize);
        }
    }
}
