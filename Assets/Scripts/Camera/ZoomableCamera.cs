using UnityEngine;

public class ZoomableCamera : MonoBehaviour
{
    [SerializeField] private InputHandler input;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomDamping;

    private FollowingCamera followingCamera;
    private Offset offset;

    private bool isZooming;

    private void Awake()
    {
        followingCamera = GetComponent<FollowingCamera>();
        offset = GetComponent<Offset>();
        isZooming = false;
    }

    private void LateUpdate()
    {
        SetZoom();

        if (isZooming)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, followingCamera.TargetPositionWithoutOffset().y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * zoomDamping);

            if ((transform.position - targetPosition).magnitude <= 0.5f)
                isZooming = false;
        }
    }

    private void SetZoom()
    {
        if (input.ZoomingIn())
            ZoomIn();
        else if (input.ZoomingOut())
            ZoomOut();
    }

    private void ZoomIn()
    {
        if (offset.Hypotenuse() - zoomSpeed >= minDistance)
            offset.MoveCloser(zoomSpeed);
        isZooming = true;
    }

    private void ZoomOut()
    {
        if (offset.Hypotenuse() + zoomSpeed <= maxDistance)
            offset.MoveFurther(zoomSpeed);
        isZooming = true;
    }
}
