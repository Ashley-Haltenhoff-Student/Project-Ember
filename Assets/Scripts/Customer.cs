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
    [SerializeField] private bool isSitting = false;

    // Connections
    private Player player;
    private TableManager tableManager;
    private OrderManager orderManager;
    private InventoryManager inventory;
    private NotifyManager notifyManager;
    private NavMeshAgent agent;

    public GlobalEvents events;

    [SerializeField] private Order order;
    [SerializeField] new private string name;
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
        if (Vector2.Distance(player.transform.position, transform.position) < 2f)
        {
            // Validing in order to give them their drink
            if (inventory.Contains(order) && !isDrinking)
            {
                inventory.Remove(order); // Update Inventory

                StartCoroutine(DrinkAndLeave());
                isDrinking = true;
            }
        }
    }

    private void OnMouseDown()
    {
        // Set the player's position to the customer
        if (isSitting)
        {
            player.GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
        }
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
        Table chosenTable = tables[Random.Range(0, tables.Count)];
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

        order = orderManager.GetNewOrder(name); // Assign Order
    }

    private IEnumerator DrinkAndLeave()
    {
        notifyManager.Notify($"{name} got their order!");

        yield return new WaitForSeconds(3);

        notifyManager.Notify($"{name} is leaving...");

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

    public Order Order 
    { 
        get { return order; } 
        set { order = value; } 
    }
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
    public OrderManager OrderManager
    {
        get { return orderManager; }
        set { orderManager = value; }
    }
    public bool IsGone
    {
        get { return isGone; }
    }
}
