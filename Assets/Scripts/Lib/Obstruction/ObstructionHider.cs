using UnityEngine;

public class ObstructionHider : MonoBehaviour
{
    private Obstruction lastObstructing;
    private Obstruction collidedObstructing;

    private void OnTriggerEnter(Collider collider)
    {
        Obstruction obstructing = collider.GetComponent<Obstruction>();

        if (obstructing != null)
        {
            collidedObstructing = obstructing;
            collidedObstructing.MakeTransparent();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        Obstruction obstructing = collider.GetComponent<Obstruction>();

        if (obstructing != null)
            obstructing.MakeVisible();
    }

    private void Update()
    {
        Obstruction obstructing = GetClosestObstruction();

        if (obstructing != null)
            HandleObstruction(obstructing);
        else
            HandleNoObstruction();
    }

    private Obstruction GetClosestObstruction()
    {
        Ray cameraRay = Camera.main.ViewportPointToRay(Camera.main.WorldToViewportPoint(transform.position));
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        RaycastHit hit = new RaycastHit();

        Physics.Raycast(cameraRay, out hit);

        return hit.transform.GetComponent<Obstruction>();
    }

    private void HandleObstruction(Obstruction obstructing)
    {
        obstructing.MakeTransparent();

        if (obstructing != lastObstructing)
            HandleNewObstruction(obstructing);
    }

    private void HandleNewObstruction(Obstruction obstructing)
    {
        if (lastObstructing != null && lastObstructing != collidedObstructing)
            lastObstructing.MakeVisible();

        lastObstructing = obstructing;
    }

    private void HandleNoObstruction()
    {
        if (lastObstructing != null)
        {
            lastObstructing.MakeVisible();
            lastObstructing = null;
        }
    }
}
