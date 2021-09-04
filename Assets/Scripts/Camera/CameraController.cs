using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Moveable player;
    [SerializeField] private InputHandler input;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 13f, -5.5f);

    [SerializeField] private float aheadSpeed;
    [SerializeField] private float followDamping;

    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float zoomStep;

    private void Start()
    {
        Camera.main.orthographicSize = minZoom;
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
        {
            Camera.main.orthographicSize = Mathf.Max(maxZoom, zoom - zoomStep);
        }
        else if (input.ZoomingOut())
        {
            Camera.main.orthographicSize = Mathf.Min(minZoom, zoom + zoomStep);
        }
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = player.transform.position + cameraOffset + SmoothedPlayerVelocity() * aheadSpeed;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followDamping * Time.deltaTime);
    }

    private Vector3 SmoothedPlayerVelocity()
    {
        if (player.IsStopping())
        {
            return Vector3.zero;
        }
        else
        {
            return player.Velocity();
        }
    }
}
