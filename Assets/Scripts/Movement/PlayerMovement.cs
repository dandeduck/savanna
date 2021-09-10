using UnityEngine;

public class PlayerMovement : Navigator
{
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float crouchSpeed;
    
    private InputHandler input;

    protected override void OnStart()
    {
        base.OnStart();

        input = GetComponent<InputHandler>();
    }

    protected override float MaxVelocity()
    {
        if (input.Sprinting())
            return runSpeed;

        if (input.Crouching())
            return crouchSpeed;

        return walkSpeed;
    }

    public override Vector3 Direction()
    {
        return new Vector3(input.Horizontal(), 0f, input.Vertical()).normalized;
    }
}
