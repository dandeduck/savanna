using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Moveable target;
    [SerializeField] private InputHandler input;
    [SerializeField] private Vector3 offset = new Vector3(0, 13f, -6.7f);

    [SerializeField] private float aheadSpeed;
    [SerializeField] private float followDamping;
    [SerializeField] private float zoomDamping;

    private float angle;
    private float hypotenuse;

    private void Awake()
    {
        angle = Mathf.Atan(Mathf.Abs(offset.y/offset.z));
        hypotenuse = Mathf.Sqrt(offset.y * offset.y + offset.z * offset.z);
    }

    private void LateUpdate()
    {
        float hypotenuseMultiplier = 1;

        if (hypotenuse <= 5)
            hypotenuseMultiplier = 0.75f;

        Vector3 targetPosition = target.transform.position + offset + SmoothedVelocity() * aheadSpeed * hypotenuseMultiplier;
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, followDamping * Time.deltaTime);

        transform.position = new Vector3(newPosition.x, Mathf.Lerp(transform.position.y, targetPosition.y, zoomDamping * Time.deltaTime), newPosition.z);
    }

    private Vector3 SmoothedVelocity()
    {
        if (target.IsStopping())
            return Vector3.zero;
        else
            return target.Velocity();
    }

    public void ZoomIn(float zoomAmount)
    {
        offset = new Vector3(0, CalcZoomedY(zoomAmount), CalcZoomedZ(zoomAmount));
        hypotenuse -= zoomAmount;
    }

    public void ZoomOut(float zoomAmount)
    {
        offset = new Vector3(0, CalcZoomedY(-zoomAmount), CalcZoomedZ(-zoomAmount));
        hypotenuse += zoomAmount;
    }

    private float CalcZoomedY(float zoomAmount)
    {
        return Mathf.Sin(angle) * (hypotenuse - zoomAmount);
    }

    private float CalcZoomedZ(float zoomAmount)
    {
        return -Mathf.Cos(angle) * (hypotenuse - zoomAmount);
    }
}
