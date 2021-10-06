using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;

    private void Start()
    {
        Cursor.visible = false;
    }
    
    public float HorizontalMovement()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public float VerticalMovement()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public Quaternion AimDirection()
    {
        return mainCamera.rotation;
    }

    public bool PickingUp()
    {
        return Input.GetKeyDown(KeyCode.F);
    }

    public bool Dropping()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool Sprinting()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool Crouching()
    {
        return Input.GetKey(KeyCode.LeftControl);
    }

    public bool ZoomingIn()
    {
        return Input.GetAxisRaw("Mouse ScrollWheel") > 0;
    }

    public bool ZoomingOut()
    {
        return Input.GetAxisRaw("Mouse ScrollWheel") < 0;
    }

    public bool UsingItem()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    public bool ChangingSelectedItem()
    {
        return SelectedItem() != -1;
    }

    public int SelectedItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            return 0;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            return 1;

        if (Input.GetKeyDown(KeyCode.Alpha3))
            return 2;

        if (Input.GetKeyDown(KeyCode.Alpha4))
            return 3;

        if (Input.GetKeyDown(KeyCode.Alpha5))
            return 4;

        if (Input.GetKeyDown(KeyCode.Alpha6))
            return 5;

        return -1;
    }

    public bool CraftTesting()
    {
        return Input.GetKeyDown(KeyCode.Tab);
    }
}
