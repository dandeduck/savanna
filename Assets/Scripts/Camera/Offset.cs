using UnityEngine;

public class Offset : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, 13f, -6.7f);

    private float angle;
    private float hypotenuse;

    private void Start()
    {
        angle = Mathf.Atan(Mathf.Abs(offset.y/offset.z));
        hypotenuse = Mathf.Sqrt(offset.y * offset.y + offset.z * offset.z);
    }

    public Vector3 Get()
    {
        return offset;
    }

    public float Hypotenuse()
    {
        return hypotenuse;
    }

    public float Angle()
    {
        return angle;
    }

    public void MoveFurther(float dist)
    {
        ChangeHypotenuse(dist);
    }

    public void MoveCloser(float dist)
    {
        ChangeHypotenuse(-dist);
    }

    public void ChangeHypotenuse(float change)
    {
        offset = new Vector3(offset.x, CalcFurtherY(change), CalcFurtherZ(change));
        hypotenuse += change;
    }
    
    private float CalcFurtherY(float zoomAmount)
    {
        return Mathf.Sin(angle) * (hypotenuse + zoomAmount);
    }

    private float CalcFurtherZ(float zoomAmount)
    {
        return -Mathf.Cos(angle) * (hypotenuse + zoomAmount);
    }
}
