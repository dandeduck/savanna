using UnityEngine;

public abstract class Moveable : Lockable
{
    [SerializeField] private float acceleration;

    private CharacterController controller;
    private float currentSpeed;
    private Vector3 currentDirection;
    private Vector3 prevDirection;

    private bool isStopping;
    
    protected override void OnStart()
    {
        controller = GetComponent<CharacterController>();

        currentSpeed = 0;
        prevDirection = transform.position;   
        isStopping = false;
    }

    protected override void OnUnlockedUpdate()
    {
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

    public override void Lock()
    {
        base.Lock();
        currentSpeed = 0;
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
}