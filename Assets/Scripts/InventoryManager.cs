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
                inventory[i] = obj.GetComponent<Order>(); // Add order
                inventorySprites[i].sprite = inventory[i].SpriteRenderer.sprite; // Update Sprite

                notifyManager.Notify(obj.name + " added to inventory!");

                Debug.Log("Added object at inventory space " + i);
                return;
            }
        }

        notifyManager.Notify("There is no space left in your inventory!");
        // Code when inventory is full

    }


    public void Remove(GameObject obj)
    {
        // Loop through inventory to find the object to remove
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == obj )
            {
                inventory[i] = null; // Remove gameobject
                inventorySprites[i].sprite = null; // Update sprite
                return;
            }
        }

        Debug.Log("Error: Object to remove was not found");
    }


}
