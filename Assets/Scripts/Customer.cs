using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPoint = new Vector3 (-11.5f, -1f, 0);



    [SerializeField] private Vector3 sittingPosition = Vector3.zero;
    [SerializeField] private bool isSitting = false;


    [SerializeField] private Player player;
    [SerializeField] private TableManager tableManager;
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private NotifyManager notifyManager;
    [SerializeField] private UIManager UI;

    private NavMeshAgent agent;

    [SerializeField] private Order order;
    [SerializeField] new private string name;
    

    private void Start()
    {
        // Get Connections
        player = FindFirstObjectByType<Player>();
        tableManager = FindFirstObjectByType<TableManager>();
        agent = GetComponent<NavMeshAgent>();
        UI = FindAnyObjectByType<UIManager>();  
        notifyManager = FindAnyObjectByType<NotifyManager>();
        inventory = FindFirstObjectByType<InventoryManager>();

        Spawn();
    }

    private void Update()
    {
        if (sittingPosition != Vector3.zero)
        {
            if (Vector2.Distance(sittingPosition, transform.position) > 0.5f)
            {
                agent.SetDestination(sittingPosition);
            }
            else
            {
                isSitting = true;
                sittingPosition = Vector3.zero;
            }
        }

        if (Vector2.Distance(player.transform.position, transform.position) < 2f)
        {
            if (inventory.Contains(order))
            {
                inventory.Remove(order);

                notifyManager.Notify($"{name} got their order!");
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
        while (!isSitting)
        {
            yield return null;
        }

        // Wait for customer to sit down
        // Develop decision time here
        while (!isSitting)
        {
            yield return null;
        }

        // Add to List
        
        order = orderManager.GetNewOrder(name); // Assign Order

        if (order != null)
        {
            Debug.Log($"customer {name} recieved the {order} order");
        }
        else
        {
            Debug.Log($"Customer {name} did not recieve the order correctly");
        }
    }

    public Order Order 
    { 
        get { return order; } 
        set { order = value; } 
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
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
}
