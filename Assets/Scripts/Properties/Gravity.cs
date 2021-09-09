using UnityEngine;

public class Gravity : MonoBehaviour
{
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!controller.isGrounded)
            controller.Move(Physics.gravity * Time.deltaTime);
    }
}
