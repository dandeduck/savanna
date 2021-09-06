using UnityEngine;

public class ZoomableCamera : MonoBehaviour
{
    [SerializeField] private InputHandler input;
    [SerializeField] private float maxSize;
    [SerializeField] private float minSize;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomDamping;

    private Camera objectCamera;
    private float targetSize;

    private void Start()
    {
        objectCamera = GetComponent<Camera>();
        objectCamera.orthographicSize = maxSize;
        targetSize = maxSize;
    }

    private void LateUpdate()
    {
        SetZoom();
    }

    private void SetZoom()
    {
        float zoom = objectCamera.orthographicSize;

        if (input.ZoomingIn())
            targetSize = Mathf.Max(minSize, targetSize - zoomSpeed);
        else if (input.ZoomingOut())
            targetSize = Mathf.Min(maxSize, targetSize + zoomSpeed);
            
        objectCamera.orthographicSize = Mathf.Lerp(zoom, targetSize, zoomDamping * Time.deltaTime);
    }
}
