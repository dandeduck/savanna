using UnityEngine;

public class PlayerMovement : Moveable
{
    public InputHandler input;

    protected override float MaxSpeed()
    {
        return 2.5f;
    }

    protected override Vector3 MoveDirection()
    {
        return new Vector3(input.Horizontal(), 0f, input.Vertical()).normalized;
    }

    // move towards mouse R.I.P.
    // Vector3 movementDirection = new Vector3(input.Horizontal(), 0f, input.Vertical()).normalized;
    // Vector3 absoluteAimDirection = input.AimDirection() - transform.position;
    // absoluteAimDirection.Normalize();

    // float angle = -Vector2.SignedAngle(Vector2.up, new Vector2(absoluteAimDirection.x, absoluteAimDirection.z)) * Mathf.Deg2Rad;    
    
    // return new Vector3(Mathf.Sin(angle) * movementDirection.z + Mathf.Cos(-angle) * movementDirection.x, 0, Mathf.Cos(angle) * movementDirection.z + Mathf.Sin(-angle) * movementDirection.x);
}
