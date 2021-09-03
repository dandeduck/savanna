using UnityEngine;

public class InputHandler : MonoBehaviour
{
    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
    
    public float Horizontal()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public float Vertical()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public Vector3 AimDirection()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            return new Vector3(pointToLook.x, 0, pointToLook.z);
        }

        return Vector3.zero;
    }
}
