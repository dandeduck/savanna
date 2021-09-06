using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Moveable player;
    [SerializeField] private InputHandler input;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 13f, -5.5f);

    [SerializeField] private float aheadSpeed;
    [SerializeField] private float followDamping;

    [SerializeField] private float maxSize;
    [SerializeField] private float minSize;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomDamping;

    private float targetSize;

    private void Start()
    {
        Camera.main.orthographicSize = maxSize;
    }

    private void LateUpdate()
    {
        FollowPlayer();
        SetZoom();
    }

    private void SetZoom()
    {
        float zoom = Camera.main.orthographicSize;

        if (input.ZoomingIn())
            targetSize = Mathf.Max(minSize, targetSize - zoomSpeed);
        else if (input.ZoomingOut())
            targetSize = Mathf.Min(maxSize, targetSize + zoomSpeed);
            
        Camera.main.orthographicSize = Mathf.Lerp(zoom, targetSize, zoomDamping * Time.deltaTime);
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = player.transform.position + cameraOffset + SmoothedPlayerVelocity() * aheadSpeed;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followDamping * Time.deltaTime);
    }

    private Vector3 SmoothedPlayerVelocity()
    {
        if (player.IsStopping())
            return Vector3.zero;
        else
            return player.Velocity();
    }
}
