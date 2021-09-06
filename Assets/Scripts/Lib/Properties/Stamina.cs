using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float staminaCapacity;
    [SerializeField] private float replenishmentRate;

    private float value;
    private bool isExhausted;

    private void Start()
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
            isExhausted = true;
    }

    public void Replenish(float timeReplenished)
    {
        value = Mathf.Min(value + timeReplenished * replenishmentRate, staminaCapacity);

        if (value == staminaCapacity)
            isExhausted = false;
    }
}
