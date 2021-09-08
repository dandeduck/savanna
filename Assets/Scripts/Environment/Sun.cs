using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float dayLengthMinutes;
    [SerializeField] private Color dayColor = new Color(1f, 1f, 1f);
    [SerializeField] private Color transitionColor = new Color(0.901f, 0.454f, 0.317f);
    [SerializeField] private Color nightColor = new Color(0.25f, 0.25f, 0.6f);
    [SerializeField] private Light moon;
    
    private Light sunlight;
    private float degreesPreSecond;
    private float currentAngle;

    private void Start()
    {
        sunlight = GetComponent<Light>();

        transform.rotation = Quaternion.Euler(0, -30, 0);
        currentAngle = 0;
        sunlight.color = dayColor;

        degreesPreSecond = 180f / dayLengthMinutes / 60f;
        Debug.Log(degreesPreSecond);
    }

    private void Update()
    { 
        float degreesToTravel = Time.deltaTime * degreesPreSecond;
        Color newSunlightColor = DetermineNewSunlightColor();

        if (currentAngle > 175)
        {
            sunlight.intensity = Mathf.Lerp(sunlight.intensity, 0, degreesToTravel);
            moon.intensity = Mathf.Lerp(moon.intensity, 1, degreesToTravel);
        }
        else
        {
            sunlight.intensity = Mathf.Lerp(sunlight.intensity, 1, degreesToTravel * 2);
            moon.intensity = Mathf.Lerp(moon.intensity, 0, degreesToTravel * 2);
        }

        AdjustSun(newSunlightColor, degreesToTravel);
    }

    public bool IsDaytime()
    {
        return currentAngle > 0 && currentAngle < 170;
    }

    public bool IsNightTime()
    {
        return currentAngle > 180;
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

    private void AdjustSun(Color sunlightColor, float degreesToTravel)
    {
        sunlight.color = Color.Lerp(sunlight.color, sunlightColor, degreesToTravel);
        transform.Rotate(degreesToTravel, 0, 0, Space.Self);

        currentAngle = (currentAngle + degreesToTravel) % 360;
    }
}
