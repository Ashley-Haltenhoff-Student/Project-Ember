
using UnityEngine;

public class EspressoControl : MonoBehaviour
{
    [SerializeField] private GlobalEvents events;

    [SerializeField] private InventoryManager inventory;
    [SerializeField] private GameObject espressoPrefab;
    [SerializeField] private GameObject espressoCup;

    [SerializeField] private bool canBrew = false;
    [SerializeField] private bool canGrind = false;

    private void Start()
    {
        if (!inventory) { inventory = FindFirstObjectByType<InventoryManager>(); }

        if (!espressoPrefab) { Debug.Log("There is no espresso prefab available"); }

        espressoCup.SetActive(false); // Hidden until coffee beans are done grinding
    }

    public bool CanBrew() { return canBrew; }
    public void CanBrew(bool canBrew) { this.canBrew = canBrew; }

    public bool CanGrind() { return canGrind; }
    public void CanGrind(bool canGrind) { this.canGrind = canGrind; }

    public void AddEspresso()
    {
        inventory.Add(espressoPrefab);
        events.TriggerEvent(events.EspressoMade); // for portafilter to reset
    }

    public void ToggleEspressoCup(bool isVisable)
    {
        espressoCup.SetActive(isVisable);
    }
}
