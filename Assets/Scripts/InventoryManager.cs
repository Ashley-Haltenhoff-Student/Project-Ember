using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Order[] inventory = new Order[5];
    [SerializeField] private Image[] inventorySprites = new Image[5];

    [SerializeField] private NotifyManager notifyManager;


    private void Start()
    {
        // Verify all UI inventory spaces are retrievable
        foreach (Image i in inventorySprites)
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
                inventorySprites[i].sprite = order.SpriteRenderer.sprite; // Update Sprite

                notifyManager.Notify(obj.name + " added to inventory!");

                Debug.Log("Added object at inventory space " + i);
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
            if (inventory[i] == order )
            {
                inventorySprites[i].sprite = null; // Update sprite
                DestroyImmediate(inventory[i].gameObject, true); // Remove gameobject
                inventory[i] = null;

                UpdateInventory(); // Update object positions

                return;
            }
        }

        Debug.Log("Error: Object to remove was not found");
    }

    public bool Contains(Order order)
    {
        foreach (Order o in inventory)
        {
            if (o == order)
            {
                return true;
            }
        }

        return false;
    }

    private void UpdateInventory()
    {
        for (int i=0; i < inventory.Length; ++i)
        {
            if (!inventory[i])
            {
                if (!inventory[i+1]) { return; } // There's none left to shift down

                // Shift down order
                inventory[i] = inventory[i + 1]; 
                inventory[i] = null;

                // Shift down order visually
                inventorySprites[i] = inventorySprites[i + 1]; 
                inventorySprites[i] = null;

            }
        }
    }
}
