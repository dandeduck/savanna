using UnityEngine;

public class PlayerMovement : Moveable
{
    public InputHandler input;

    protected override float MaxSpeed()
    {
        if (input.IsSprinting())
        {
            return 5f;
        }

        if (input.IsCrouching())
        {
            return 1.5f;
        }

        return 2.5f;
    }

    public override Vector3 MovementDirection()
    {
        return new Vector3(input.Horizontal(), 0f, input.Vertical()).normalized;
    }
}
