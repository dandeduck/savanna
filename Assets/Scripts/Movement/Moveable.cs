using UnityEngine;

public abstract class Moveable : MonoBehaviour
{
    [SerializeField] private float acceleration;

    private CharacterController controller;
    private float currentSpeed;
    private Vector3 currentDirection;
    private Vector3 prevDirection;

    private bool isStopping;
    private bool isLocked;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();

        currentSpeed = 0;
        prevDirection = transform.position;   
        isStopping = false;
        isLocked = false;

        OnStart();
    }

    private void Update()
    {
        if (!isLocked)
            Move();
    }

    public float Acceleration()
    {
        return acceleration;
    }

    public virtual Vector3 Velocity()
    {
        return controller.velocity;
    }

    public bool IsStopping()
    {
        return isStopping;
    }

    public void Lock()
    {
        isLocked = true;
    }

    public void UnLock()
    {
        isLocked = false;
    }

    private void Move()
    {
        currentDirection = Direction();

        if (currentDirection.magnitude >= 0.1f)
            MoveForward();
        
        else
            MoveBackwards();
    }

    private void MoveForward()
    {
        isStopping = false;
        prevDirection = currentDirection;
        currentSpeed = Mathf.Min(MaxVelocity(), currentSpeed + acceleration * Time.deltaTime);
        controller.Move(currentDirection * currentSpeed * Time.deltaTime);
    }

    private void MoveBackwards()
    {
        isStopping = true;
        currentSpeed = Mathf.Max(0, currentSpeed - acceleration * 2 * Time.deltaTime);
        controller.Move(prevDirection * currentSpeed * Time.deltaTime);
    }

    public abstract Vector3 Direction();
    protected abstract float MaxVelocity();
    protected virtual void OnStart() {}
}