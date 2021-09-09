using UnityEngine;

public class PlayerMovement : Moveable
{
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private InputHandler input;
    [SerializeField] private Stamina stamina;

    protected override void OnStart()
    {
        if (input == null)
            input = GetComponent<InputHandler>();
        if (stamina == null)
            stamina = GetComponent<Stamina>();
    }

    protected override float MaxSpeed()
    {
        if (input.Sprinting())
        {
            if (!stamina.IsExhausted())
            {
                stamina.Consume(Time.deltaTime);
                return runSpeed;
            }
        }

        stamina.Replenish(Time.deltaTime);

        if (input.Crouching())
            return crouchSpeed;

        return walkSpeed;
    }

    public override Vector3 Direction()
    {
        return new Vector3(input.Horizontal(), 0f, input.Vertical()).normalized;
    }
}
