using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float dayLengthMinutes;
    [SerializeField] private Color dayColor = new Color(1f, 1f, 1f);
    [SerializeField] private Color transitionColor = new Color(0.901f, 0.454f, 0.317f);
    [SerializeField] private Color nightColor = new Color(0.25f, 0.25f, 0.6f);
    [SerializeField] private Light sunlight;

    private float degreesPreSecond;
    private float currentAngle;

    private void Start()
    {
        if (sunlight == null)
            sunlight = GetComponent<Light>();

        transform.rotation = Quaternion.Euler(0, -30, 0);
        currentAngle = 0;
        sunlight.color = dayColor;

        if (dayLengthMinutes == 0)
            degreesPreSecond = 0;
        else
            degreesPreSecond = 180f / dayLengthMinutes / 60f;
    }

    private void Update()
    { 
        if (degreesPreSecond == 0)
            return;

        float travelSpeed = TravelSpeed();
        Color newSunlightColor = DetermineNewSunlightColor();

        AdjustSun(newSunlightColor, travelSpeed);
    }

    public float TravelSpeed()
    {
        return Time.deltaTime * degreesPreSecond;
    }

    public bool IsDaytime()
    {
        return currentAngle >= 0 && currentAngle <= 175;
    }

    private Color DetermineNewSunlightColor()
    {
        if (currentAngle < 5)
            return transitionColor;
        
        if (currentAngle < 170)
            return dayColor;

        if (currentAngle < 175)
            return transitionColor;
        
        return nightColor;
    }

    private void AdjustSunIntensity(float travelSpeed)
    {
        if (currentAngle > 175)
            sunlight.intensity = Mathf.Lerp(sunlight.intensity, 0, travelSpeed);
        else
            sunlight.intensity = Mathf.Lerp(sunlight.intensity, 1, travelSpeed * 2);
    }

    private void AdjustSun(Color sunlightColor, float travelSpeed)
    {
        sunlight.color = Color.Lerp(sunlight.color, sunlightColor, travelSpeed);
        transform.Rotate(travelSpeed, 0, 0, Space.Self);

        AdjustSunIntensity(travelSpeed);

        currentAngle = (currentAngle + travelSpeed) % 360;
    }
}
