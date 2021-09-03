using UnityEngine;

public abstract class Moveable : MonoBehaviour
{
    public CharacterController controller;
    public float acceleration;    
    public float maxSpeed {get; private set;};

    private float currentSpeed;
    private Vector3 currentDirection;
    private Vector3 prevDirection;
    private bool isStopping;
    
    void Start()
    {
        currentSpeed = 0;
        prevDirection = transform.position;   
        isStopping = false; 
    }

    void Update()
    {
        currentDirection = moveDirection();

        if (currentDirection.magnitude >= 0.1f)
        {
            isStopping = false;
            prevDirection = currentDirection;
            currentSpeed = Mathf.Min(maxSpeed, currentSpeed+acceleration);
            controller.Move(currentDirection * currentSpeed * Time.deltaTime);
        }
        else
        {
            isStopping = true;
            currentSpeed = Mathf.Max(0, currentSpeed-acceleration);
            controller.Move(prevDirection * currentSpeed * Time.deltaTime);
        }
    }

    public Vector3 GetCurrentVelocity()
    {
        return currentDirection * currentSpeed;
    }

    public bool IsStopping()
    {
        return isStopping;
    }

    protected abstract Vector3 moveDirection();
}