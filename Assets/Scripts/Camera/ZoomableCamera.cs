using UnityEngine;

public class ZoomableCamera : MonoBehaviour
{
    [SerializeField] private InputHandler input;
    [SerializeField] private float maxZoom;
    [SerializeField] private float zoomSpeed;

    private FollowingCamera objectCamera;
    private float zoom;

    private void Start()
    {
        objectCamera = GetComponent<FollowingCamera>();
    }

    private void LateUpdate()
    {
        if (input.ZoomingIn())
            ZoomIn();
        else if (input.ZoomingOut())
            ZoomOut();
    }

    private void ZoomIn()
    {
        if (zoom + zoomSpeed <= maxZoom)
        {
            objectCamera.ZoomIn(zoomSpeed);
            zoom += zoomSpeed;
        }
    }

    private void ZoomOut()
    {
        if (zoom - zoomSpeed >= 0)
        {
            objectCamera.ZoomOut(zoomSpeed);
            zoom -= zoomSpeed;
        }
    }
}
