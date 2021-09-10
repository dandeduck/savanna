using UnityEngine;

public class Gravity : MonoBehaviour
{
    private CharacterController controller;
    private bool isLocked;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (isLocked)
            if (!controller.isGrounded)
                controller.Move(Physics.gravity * Time.deltaTime);
    }

    public void Lock()
    {
        isLocked = true;
    }

    public void UnLock()
    {
        isLocked = false;
    }
}
