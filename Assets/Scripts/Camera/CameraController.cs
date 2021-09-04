using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Moveable player;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 13f, -5.5f);

    [SerializeField] private float aheadSpeed;
    [SerializeField] private float followDamping;

    private void LateUpdate()
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
