using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Moveable target;
    [SerializeField] private InputHandler input;

    [SerializeField] private float aheadSpeed;
    [SerializeField] private float followDamping;

    private Offset offset;
    private Vector3 targetPosition;

    private void Awake()
    {
        offset = GetComponent<Offset>();
    }

    private void LateUpdate()
    {
        float hypotenuseMultiplier = 1;

        if (offset.Hypotenuse() <= 5)
            hypotenuseMultiplier = 0.75f;

        targetPosition = target.transform.position + SmoothedVelocity() * aheadSpeed * hypotenuseMultiplier;

        transform.position = Vector3.Lerp(transform.position, targetPosition + offset.Get(), followDamping * Time.deltaTime);
    }

    private Vector3 SmoothedVelocity()
    {
        if (target.IsStopping())
            return Vector3.zero;
        else
            return target.Velocity();
    }

    public Vector3 TargetPositionWithoutOffset()
    {
        return targetPosition;
    }
}
