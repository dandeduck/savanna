using UnityEngine;

public class ZoomableCamera : MonoBehaviour
{
    [SerializeField] private InputHandler input;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float zoomSpeed;

    private Offset offset;

    private void Awake()
    {
        offset = GetComponent<Offset>();
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
        if (offset.Hypotenuse() - zoomSpeed >= minDistance)
            offset.MoveCloser(zoomSpeed);
    }

    private void ZoomOut()
    {
        if (offset.Hypotenuse() + zoomSpeed <= maxDistance)
            offset.MoveFurther(zoomSpeed);
    }
}
