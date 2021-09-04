using UnityEngine;

public class EnvironmentHider : MonoBehaviour
{
    private const float transperantAlpha = 0f;

    private void OnTriggerEnter(Collider other)
    {
        Obstructing obstructing = other.GetComponent<Obstructing>();

        if (obstructing != null)
        {
            obstructing.MakeTransperent();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Obstructing obstructing = other.GetComponent<Obstructing>();

        if (obstructing != null)
        {
            obstructing.MakeVisible();
        }
    }
}
