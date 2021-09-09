using UnityEngine;
using TMPro;

public class LowerInventory : MonoBehaviour
{
    private Inventory inventory;
    private TMP_Text text;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        int amountOfBalls = 0;

        if (inventory.Items().ContainsKey("Ball"))
            amountOfBalls = inventory.Items()["Ball"].Amount();
        
        text.text = $"{amountOfBalls} Balls";
    }
}
