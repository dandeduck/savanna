using UnityEngine;

public class Sun : CelestialBody
{
    [SerializeField] private Color dayColor = new Color(1f, 1f, 1f);
    [SerializeField] private Color transitionColor = new Color(0.901f, 0.454f, 0.317f);
    [SerializeField] private Color nightColor = new Color(0.0156f, 0.0156f, 0.262f);
    
    override protected void OnStart()
    {
        lightSource.color = dayColor;
    }

    override protected void OnUpdate()
    { 
        Color newSunlightColor = DetermineNewSunlightColor();
        float travelSpeed = TravelSpeed();

        AdjustSun(newSunlightColor, travelSpeed);
    }

    private Color DetermineNewSunlightColor()
    {
        if (currentAngle < 5)
            return transitionColor;
        
        if (currentAngle < 170 || currentAngle > 180)
            return dayColor;

        if (currentAngle < 175)
            return transitionColor;
        
        return nightColor;
    }

    private void AdjustSun(Color sunlightColor, float travelSpeed)
    {
        lightSource.color = Color.Lerp(lightSource.color, sunlightColor, travelSpeed);
    }
}
