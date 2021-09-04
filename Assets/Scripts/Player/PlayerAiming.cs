using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private Moveable player;
    private Vector3 lastDirection;

    private void Start()
    {
        player = GetComponent<Moveable>();

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

        if (direction.magnitude != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        }
    }
}
