using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Moveable target;
    [SerializeField] private InputHandler input;
    [SerializeField] private Vector3 offset = new Vector3(0, 13f, -5.5f);

    [SerializeField] private float aheadSpeed;
    [SerializeField] private float followDamping;

    private void LateUpdate()
    {
        Vector3 targetPosition = target.transform.position + offset + SmoothedVelocity() * aheadSpeed;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followDamping * Time.deltaTime);
    }

    private Vector3 SmoothedVelocity()
    {
        if (target.IsStopping())
            return Vector3.zero;
        else
            return target.Velocity();
    }
}
