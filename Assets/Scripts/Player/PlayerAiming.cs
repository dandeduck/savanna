using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public Moveable player;
    public float rotationSpeed = 5f;

    private Vector3 lastDirection;

    private void Start()
    {
        lastDirection = Vector3.zero;
    }

    private void Update()
    {
        Vector3 direction = player.Direction();

        if (direction.magnitude == 0)
        {
            direction = lastDirection;
        }
        else
        {
            lastDirection = direction;
        }

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
