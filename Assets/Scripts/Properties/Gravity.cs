using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    private void Start()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!controller.isGrounded)
            controller.Move(Physics.gravity * Time.deltaTime);
    }
}
