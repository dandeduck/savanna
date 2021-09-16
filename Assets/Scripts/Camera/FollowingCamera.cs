using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Moveable target;
    [SerializeField] private InputHandler input;

    [SerializeField] private float aheadSpeed;
    [SerializeField] private float followDamping;
    [SerializeField] private float zoomDamping;

    private Offset offset;

    private void Awake()
    {
        offset = GetComponent<Offset>();
    }

    private void LateUpdate()
    {
        float hypotenuseMultiplier = 1;

        if (offset.Hypotenuse() <= 5)
            hypotenuseMultiplier = 0.75f;

        Vector3 targetPosition = target.transform.position + offset.Get() + SmoothedVelocity() * aheadSpeed * hypotenuseMultiplier;
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
}
