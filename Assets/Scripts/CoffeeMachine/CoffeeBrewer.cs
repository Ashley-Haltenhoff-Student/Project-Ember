using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeBrewer : LockSpot
{
    [Header("Brewer Connections")]
    [SerializeField] private GameObject coffeePrefab;
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private Mugs mugs;

    protected override void IsLockedActions()
    {
        StartCoroutine(Brew());
    }

    private IEnumerator Brew()
    {
        canRemove = false;

        notifyManager.Notify("Brewing...");
        yield return new WaitForSeconds(5);
        notifyManager.Notify("Coffee complete!");

        inventory.Add(coffeePrefab);
        mugs.RefreshMug(); // Allow mug to be spawned again

        canRemove = true;
    } 
}
