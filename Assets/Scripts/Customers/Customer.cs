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
    private OrderManager orderManager;
    private InventoryManager inventory;
    private NotifyManager notifyManager;
    private UIManager UI;

    public GlobalEvents events;
    public TableManager tableManager;
    
    private Table chosenTable;
    private NavMeshAgent agent;

    [Header("Booleans")]
    [SerializeField] private bool isSitting = false;
    [SerializeField] private bool isDrinking = false;
    [SerializeField] private bool isGone = false;
    

    private void Start()
    {
        // Get Connections // could be better :/
        player = FindFirstObjectByType<Player>();
        agent = GetComponent<NavMeshAgent>();
        notifyManager = FindAnyObjectByType<NotifyManager>();
        inventory = FindFirstObjectByType<InventoryManager>();
        UI = FindFirstObjectByType<UIManager>();
        orderManager = FindFirstObjectByType<OrderManager>();
        reaction = GetComponentInChildren<Reaction>();

        order = GetComponentInChildren<Order>();
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
                    UI.RemoveOrder(order.orderNumber); // Update UI

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
            UI.OnCustomerHover(name, order.name, Input.mousePosition);
            
        }
    }

    private void OnMouseExit()
    {
        UI.OnCustomerCursorLeave();
    }

    // Called when spawned in Start()
    public void Spawn()
    {
        gameObject.transform.position = spawnPoint;
        StartCoroutine(OrderingSequence());
    }

    private IEnumerator OrderingSequence()
    {
        // Find table and update occupiancy 
        List<Table> tables = tableManager.OpenTables;

        chosenTable = tables[Random.Range(0, tables.Count)];
        tableManager.TableIsOccupied(chosenTable);

        // Find seat
        Vector2[] seatOptions = chosenTable.SeatPositions; // Get seat positions
        Vector2 seat = seatOptions[Random.Range(0, seatOptions.Length)];

        if (seat != null)
        {
            sittingPosition = seat;

            yield return new WaitUntil(() => isSitting);

            orderManager.GetNewOrder(order, name); // Assign Order

            if (order.name == "none") { Debug.Log("Error: Get order failed"); }
        }
        else 
        { 
            Debug.Log("Error: Could not find a seat");
            StartCoroutine(Leave()); // Leave the restaurant
        }
        
    }


    private IEnumerator DrinkAndLeave() // When customer gets a drink
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

    private IEnumerator Leave() // When customer doesn't get a drink
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


    // following functions are designed for easier and limited access to certain variables
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
