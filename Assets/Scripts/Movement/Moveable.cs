using UnityEngine;

public abstract class Moveable : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    private CharacterController controller;
    private float currentSpeed;
    private Vector3 currentDirection;
    private Vector3 prevDirection;
    private bool isStopping;
    
    private void Start()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();

        currentSpeed = 0;
        prevDirection = transform.position;   
        isStopping = false;

        OnStart();
    }

    private void Update()
    {
        currentDirection = Direction();

        if (currentDirection.magnitude >= 0.1f)
        {
            isStopping = false;
            prevDirection = currentDirection;
            currentSpeed = Mathf.Min(MaxSpeed(), currentSpeed+acceleration);
            controller.Move(currentDirection * currentSpeed * Time.deltaTime);
        }
        
        else
        {
            isStopping = true;
            currentSpeed = Mathf.Max(0, currentSpeed-deceleration);
            controller.Move(prevDirection * currentSpeed * Time.deltaTime);
        }
    }

    public Vector3 Velocity()
    {
        return currentDirection * currentSpeed;
    }

    public bool IsStopping()
    {
        return isStopping;
    }

    public abstract Vector3 Direction();
    protected abstract float MaxSpeed();
    protected virtual void OnStart() {}
}