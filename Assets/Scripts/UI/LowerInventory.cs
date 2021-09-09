using UnityEngine;
using TMPro;

public class LowerInventory : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    
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

        if (Input.GetKeyDown(KeyCode.Space))
            inventory.Drop("Ball", 1);
    }
}
