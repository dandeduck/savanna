using UnityEngine;
using TMPro;

public class LowerInventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private InputHandler input;
    
    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        int amountOfBalls = 0;

        if (inventory.Items().ContainsKey("Ball"))
            amountOfBalls = inventory.Items()["Ball"].Amount();
        
        text.text = $"{amountOfBalls} Balls";

        if (input.Dropping())
            inventory.Drop("Ball", 1);
    }
}
