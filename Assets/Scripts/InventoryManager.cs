using System.Collections;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Order[] inventory = new Order[5];
    [SerializeField] private InventorySpot[] spots = new InventorySpot[5];
    [SerializeField] private Sprite defaultSprite;

    [SerializeField] private NotifyManager notifyManager;
    [SerializeField] private UIManager UI;


    private void Start()
    {
        // Verify all UI inventory spaces are retrievable
        foreach (InventorySpot i in spots)
        {
            if (!i)
            {
                Debug.Log("Inventory UI spaces cannot be retrieved");
            }
        }

        if (!notifyManager) { notifyManager = FindFirstObjectByType<NotifyManager>(); }
    }

    
    public void Add(GameObject obj)
    {

        // Loop through inventory to find the first empty spot
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                Order order = obj.GetComponent<Order>();
                inventory[i] = order; // Add order
                spots[i].image.sprite = order.sprite; // Update Sprite
                spots[i].itemName = order.name; // Update name when hovering

                notifyManager.Notify(obj.name + " added to inventory!");

                return;
            }
        }

        notifyManager.Notify("There is no space left in your inventory!");
        // Code when inventory is full

    }


    public void Remove(Order order)
    {
        // Loop through inventory to find the object to remove
        for (int i = 0; i < inventory.Length; i++)
        {
            // Check for name, not simply if it's the correct order, because it will search for the order name
            if (inventory[i].name == order.name)
            {
                spots[i].image.sprite = defaultSprite; // Update sprite
                spots[i].itemName = "none"; // Update name when hovering

                inventory[i] = null;
                StartCoroutine(UpdateInventory());
                return;
            }
        }

        Debug.Log("Error: Object to remove was not found");
    }

    public bool Contains(Order order)
    {
        foreach (Order o in inventory)
        {
            // Check for name, not simply if it's the correct order, because it will search for the order name
            if (o.name == order.name)
            {
                return true;
            }
        }

        return false;
    }

    private IEnumerator UpdateInventory()
    {
        for (int i = 0; i < inventory.Length; ++i)
        {
            if (!inventory[i])
            {
                if (!inventory[i+1]) { yield break; } // There's none left to shift down

                yield return new WaitForSeconds(1);

                // Shift down order
                inventory[i] = inventory[i + 1]; 
                inventory[i + 1] = null;
                

                // Shift down order visually
                spots[i].image.sprite = spots[i + 1].image.sprite;
                spots[i + 1].image.sprite = defaultSprite;
                spots[i + 1].itemName = "none"; // Update name when hovering
            }
        }
    }

    
}
