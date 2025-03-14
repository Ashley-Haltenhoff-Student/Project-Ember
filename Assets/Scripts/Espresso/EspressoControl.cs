
using UnityEngine;

public class EspressoControl : MonoBehaviour
{
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private GameObject espressoPrefab;

    [SerializeField] private bool canBrew = false;
    [SerializeField] private bool canGrind = false;

    private void Start()
    {
        if (!inventory) { inventory = FindFirstObjectByType<InventoryManager>(); }

        if (!espressoPrefab) { Debug.Log("There is no espresso prefab available"); }
    }

    public bool CanBrew() { return canBrew; }
    public void CanBrew(bool canBrew) { this.canBrew = canBrew; }

    public bool CanGrind() { return canGrind; }
    public void CanGrind(bool canGrind) { this.canGrind = canGrind; }

    public void AddEspresso()
    {
        inventory.Add(espressoPrefab);
    }
}
