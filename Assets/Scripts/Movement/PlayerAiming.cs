using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    
    private Moveable player;
    private Vector3 lastDirection;
    private bool isLocked;

    private void Start()
    {
        player = GetComponent<Moveable>();
        lastDirection = Vector3.zero;
        isLocked = false;
    }

    private void Update()
    {
        if (!isLocked)
        {
            Vector3 direction = player.Direction();

            Rotate(direction);
        }
    }

    public void Lock()
    {
        isLocked = true;
    }

    public void UnLock()
    {
        isLocked = false;
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
