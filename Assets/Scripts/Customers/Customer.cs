using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Customer : MonoBehaviour
{

    [SerializeField] private Vector3 spawnPoint = new Vector3 (-11.5f, -1f, 0);
    [SerializeField] private Vector3 sittingPosition = Vector3.zero;
    [SerializeField] private Order order;
    [SerializeField] new private string name;
    [SerializeField] private Reaction reaction;

    // Connections
    private Player player;
    private TableManager tableManager;
    private OrderManager orderManager;
    private InventoryManager inventory;
    private NotifyManager notifyManager;
    private UIManager UI;

    private NavMeshAgent agent;
    private Table chosenTable;
    public GlobalEvents events;

    [Header("Booleans")]
    [SerializeField] private bool isSitting = false;
    [SerializeField] private bool isDrinking = false;
    [SerializeField] private bool isGone = false;
    

    private void Start()
    {
        

        // Get Connections
        player = FindFirstObjectByType<Player>();
        tableManager = FindFirstObjectByType<TableManager>();
        agent = GetComponent<NavMeshAgent>();
        notifyManager = FindAnyObjectByType<NotifyManager>();
        inventory = FindFirstObjectByType<InventoryManager>();
        UI = FindFirstObjectByType<UIManager>();
        orderManager = FindFirstObjectByType<OrderManager>();

        
        reaction = GetComponentInChildren<Reaction>();

        Spawn();
    }

    private void Update()
    {
        // Validating if the player is at their table or not
        if (sittingPosition != Vector3.zero)
        {
            if (Vector2.Distance(sittingPosition, transform.position) > 0.5f)
            {
                agent.SetDestination(sittingPosition); // continue walking
            }
            else
            {
                isSitting = true;
                sittingPosition = Vector3.zero;
            }
        }

        // If player is near 
        if (Vector2.Distance(player.transform.position, transform.position) < 2f && isSitting)
        {
            Vector2 pos = transform.position;
            UI.EToInteract(new Vector3(pos.x - 50, pos.y - 50, 0));

            if (Input.GetKeyDown(KeyCode.E))
            {
                // Validing in order to give them their drink
                if (inventory.Contains(order) && !isDrinking)
                {
                    inventory.Remove(order); // Update Inventory
                    UI.RemoveOrder(order.OrderNumber); // Update UI

                    reaction.React("happy");

                    StartCoroutine(DrinkAndLeave());
                    isDrinking = true;
                }
            }

        }
        else
        {
            UI.HideEToInteract();
        }
    }

    private void OnMouseDown()
    {
        // Set the player's position to the customer
        if (isSitting && player.canMove)
        {
            player.GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
        }
    }

    private void OnMouseOver()
    {
        // The only time the player can't move is if a window is open
        if (order && player.canMove)
        {
            UI.OnCustomerHover(name, order.Name, Input.mousePosition);

        }
    }

    private void OnMouseExit()
    {
        UI.OnCustomerCursorLeave();
    }

    private void Spawn()
    {
        gameObject.transform.position = spawnPoint;

        OrderingSequence();
    }

    private void OrderingSequence()
    {
        // Find table and update occupiancy 
        List<Table> tables = tableManager.OpenTables;

        if (tables.Count < 0)
        {
            StartCoroutine(Leave());
            return;
        }

        chosenTable = tables[Random.Range(0, tables.Count)];
        tableManager.TableIsOccupied(chosenTable);

        // Find seat
        Vector2[] seatOptions = chosenTable.SeatPositions; // Get seat positions
        Vector2 seat = seatOptions[Random.Range(0, seatOptions.Length)];

        if (seat != null)
        {
            sittingPosition = seat;
        }
        else { Debug.Log("Seat is null"); }
        
    }

    public void SitAndOrderDrink()
    {
        StartCoroutine(OrderDrink());
    }

    private IEnumerator OrderDrink()
    {

        // Wait for customer to sit down
        // Develop decision time here
        while (!isSitting)
        {
            yield return null;
        }

        orderManager.GetNewOrder(order, name); // Assign Order
    }

    private IEnumerator DrinkAndLeave()
    {
        notifyManager.Notify($"{name} got their order!");

        yield return new WaitForSeconds(3);

        notifyManager.Notify($"{name} is leaving...");
        tableManager.TableIsOpen(chosenTable);

        // Start leaving
        while (Vector2.Distance(spawnPoint, transform.position) > 0.5f)
        {
            agent.SetDestination(spawnPoint); // Continue walking
            yield return null;
        }

        // Customer is now gone
        isGone = true;
        events.TriggerEvent(events.CustomerLeft);
    }

    private IEnumerator Leave()
    {
        // Start leaving
        while (Vector2.Distance(spawnPoint, transform.position) > 0.5f)
        {
            agent.SetDestination(spawnPoint); // Continue walking
            yield return null;
        }

        // Customer is now gone
        isGone = true;
        events.TriggerEvent(events.CustomerLeft);
    }

    // functions designed for easier and limited access to certain variables
    public string Name
    {
        get { return name; }
        set
        {
            if (name != null)
            {
                name = value;
            }
            else
            {
                name = "mr. no name";
            }
        }
    }
    public bool IsSitting
    {
        get { return IsSitting; }
    }
    public bool IsGone
    {
        get { return isGone; }
    }
}
