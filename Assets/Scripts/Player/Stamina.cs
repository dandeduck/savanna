using UnityEngine;

public class Stamina : MonoBehaviour
{
    public float staminaCapacity;
    public float replenishmentRate;

    private float value;
    private bool isExhausted;

    void Start()
    {
        value = staminaCapacity;   
    }

    public float Value()
    {
        return value;
    }

    public bool IsExhausted()
    {
        return isExhausted;
    }

    public void Consume(float timeUsed)
    {
        value = Mathf.Max(value - timeUsed, 0);

        if (value == 0)
        {
            isExhausted = true;
        }
    }

    public void Replenish(float timeReplenished)
    {
        value = Mathf.Min(value + timeReplenished * replenishmentRate, staminaCapacity);

        if (value == staminaCapacity)
        {
            isExhausted = false;
        }
    }
}
