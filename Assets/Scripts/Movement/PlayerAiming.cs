using UnityEngine;

public class PlayerAiming : Lockable
{
    [SerializeField] private float rotationSpeed;
    
    private Moveable player;
    private Vector3 lastDirection;

    protected override void OnStart()
    {
        player = GetComponent<Moveable>();
        lastDirection = Vector3.zero;
    }

    protected override void OnUnlockedUpdate()
    {
        Vector3 direction = player.Direction();

        Rotate(direction);
    }

    private void Rotate(Vector3 direction)
    {
        if (direction.magnitude > 0)
        {
            lastDirection = direction;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        }
    }
}
