using UnityEngine;

public abstract class CelestialBody : MonoBehaviour
{
    [SerializeField] private float halfCycleLengthMinutes;

    protected Light lightSource;
    protected float currentAngle;

    private float degreesPreSecond;

    private void Start()
    {
        lightSource = GetComponent<Light>();
        currentAngle = InitialAngle();
        transform.rotation = Quaternion.Euler(InitialAngle(), -30, 0);

        if (halfCycleLengthMinutes == 0)
            degreesPreSecond = 0;
        else
            degreesPreSecond = 180f / halfCycleLengthMinutes / 60f;   

        OnStart();    
    }

    private void Update()
    {
        if (degreesPreSecond == 0)
            return;

        float travelSpeed = TravelSpeed();

        AdjustBody(travelSpeed);

        OnUpdate();
    }

    
    public float TravelSpeed()
    {
        return Time.deltaTime * degreesPreSecond;
    }

    public bool IsAbove()
    {
        return currentAngle >= 0 && currentAngle <= 175;
    }

    private void AdjustBody(float travelSpeed)
    {
        transform.Rotate(travelSpeed, 0, 0, Space.Self);

        AdjustIntensity(travelSpeed);

        currentAngle = (currentAngle + travelSpeed) % 360;
    }

    private void AdjustIntensity(float travelSpeed)
    {
        if (currentAngle > 175 && currentAngle < 355)
            lightSource.intensity = Mathf.Lerp(lightSource.intensity, 0, travelSpeed);
        else
            lightSource.intensity = Mathf.Lerp(lightSource.intensity, 1, travelSpeed * 2);
    }

    protected virtual float InitialAngle() 
    { 
        return 0;
    }

    protected virtual void OnStart() {}
    protected virtual void OnUpdate() {}
}
