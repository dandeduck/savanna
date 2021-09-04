using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float heldThreshold;

    private Moveable player;
    private Vector3 lastDirection;

    private float timeOnLastDirection;
    private Vector3 lastHeldDirection;

    private void Start()
    {
        player = GetComponent<Moveable>();
        lastDirection = Vector3.zero;
        timeOnLastDirection = 0;
        lastHeldDirection = Vector3.zero;
    }

    private void Update()
    {
        Vector3 direction = player.Direction();
        float timePassed = Time.deltaTime;

        lastHeldDirection = CalcLastHeldDirection(direction, timePassed);

        if (direction.magnitude == 0)
        {
            direction = lastHeldDirection;
        }
        else
        {
            lastDirection = direction;
        }

        if (direction.magnitude != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        }
    }

    private Vector3 CalcLastHeldDirection(Vector3 currentDirection, float timePassed)
    {
        if (currentDirection == lastHeldDirection)
        {
            timeOnLastDirection += timePassed;
            return lastHeldDirection;
        }
        else if (timeOnLastDirection >= heldThreshold)
        {
            return currentDirection;
        }
        else
        {
            return lastDirection;
        }
    }
}
