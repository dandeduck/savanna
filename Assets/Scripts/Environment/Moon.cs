using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField] private Sun sun;

    private Light moonlight;
    
    private void Start()
    {
        transform.rotation = Quaternion.Euler(175, -30, 0);    
        moonlight = GetComponent<Light>();
        moonlight.intensity = 0;
    }

    private void Update()
    {
        if (sun.IsDaytime())
            moonlight.intensity = Mathf.Lerp(moonlight.intensity, 0, sun.TravelSpeed() * 2);
        else
            moonlight.intensity = Mathf.Lerp(moonlight.intensity, 1, sun.TravelSpeed() * 2);
    }
}
