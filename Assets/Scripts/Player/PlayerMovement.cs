using UnityEngine;

public class PlayerMovement : Moveable
{
    public InputHandler input;
    public Stamina stamina;

    protected override float MaxSpeed()
    {

        if (input.IsSprinting())
        {
            if (!stamina.IsExhausted())
            {
                stamina.Consume(Time.deltaTime);
                return 5f;
            }
        } 
        else
        {
            stamina.Replenish(Time.deltaTime);
        }

        if (input.IsCrouching())
        {
            return 1.5f;
        }

        return 2.5f;
    }

    public override Vector3 Direction()
    {
        return new Vector3(input.Horizontal(), 0f, input.Vertical()).normalized;
    }
}
