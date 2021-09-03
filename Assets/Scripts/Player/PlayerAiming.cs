using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public Moveable player;

    public float rotationSpeed = 5f;

    void Update()
    {
        Quaternion lookRotation = Quaternion.LookRotation(player.MovementDirection());

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
