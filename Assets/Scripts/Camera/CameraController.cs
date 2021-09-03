using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Moveable player;
    public float aheadSpeed;
    public float followDamping;
    
    public Vector3 cameraOffset = new Vector3(0, 13f, -5.5f);

    void LateUpdate()
    {
        Vector3 targetPosition = player.transform.position + cameraOffset + SmoothedPlayerVelocity() * aheadSpeed;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followDamping * Time.deltaTime);
    }

    private Vector3 SmoothedPlayerVelocity()
    {
        if (player.IsStopping())
        {
            return Vector3.zero;
        }
        else
        {
            return player.GetVelocity();
        }
    }
}
