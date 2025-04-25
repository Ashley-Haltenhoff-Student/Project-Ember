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

    private string emotion = "happy";
    public int timer = 60;
    public string customerType = "normal";
    private bool gotDrink = false;

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
    private ScoreManager scoreManager;

    [Header("Booleans")]
    [SerializeField] private bool isSitting = false;
    [SerializeField] private bool isDrinking = false;
    [SerializeField] private bool isGone = false;
    

    private void Start()
    {
        // Get Connections
        player = FindFirstObjectByType<Player>();
        agent = GetComponent<NavMeshAgent>();
        notifyManager = FindAnyObjectByType<NotifyManager>();
        inventory = FindFirstObjectByType<InventoryManager>();
        UI = FindFirstObjectByType<UIManager>();
        orderManager = FindFirstObjectByType<OrderManager>();
        reaction = GetComponentInChildren<Reaction>();
        order = GetComponentInChildren<Order>();
        scoreManager = FindFirstObjectByType<ScoreManager>();

        events.GameEnd.AddListener(End);
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
                    

                    StartCoroutine(DrinkAndLeave());
                    isDrinking = true;
                }
                else
                {
                    notifyManager.Notify("You don't have the correct order in your inventory.");
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
            UI.OnCustomerHover(name, order.name, customerType, timer, Input.mousePosition);
            
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

        // Find seat from table given from customer manager
        Vector2[] seatOptions = chosenTable.SeatPositions; // Get seat positions
        Vector2 seat = seatOptions[Random.Range(0, seatOptions.Length)];

        if (seat != null)
        {
            sittingPosition = seat;

            yield return new WaitUntil(() => isSitting);

            
            orderManager.GetNewOrder(order, name, customerType); // Assign Order
            StartCoroutine(Timer()); // Start timer

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
        inventory.Remove(order); // Update Inventory
        UI.RemoveOrder(order.orderNumber); // Update UI

        reaction.React(emotion);


        notifyManager.Notify($"{name} got their order!");
        scoreManager.AddToScore(); // Update Score
        gotDrink = true;

        UI.secondsLeft += 5; // Add more time to the timer

        yield return new WaitForSeconds(4);

        notifyManager.Notify($"{name} is leaving...");
        tableManager.TableIsOpen(chosenTable);

        // Start leaving
        StartCoroutine(Leave());
    }

    private void End()
    {
        UI.RemoveOrder(order.orderNumber); // Update UI

        isGone = true;
        events.TriggerEvent(events.CustomerLeft);
    }

    private IEnumerator Leave()
    {
        // Start leaving
        while (Vector2.Distance(spawnPoint, transform.position) > 0.75f)
        {
            agent.SetDestination(spawnPoint); // Continue walking
            yield return null;
        }

        // Customer is now gone
        isGone = true;
        events.TriggerEvent(events.CustomerLeft); 
    }


    private IEnumerator Timer()
    {
        if (customerType == "impatient")
        {
            timer = 45;
        }

        while (timer > 0)
        {
            // If they received a drink, don't do the actions following the timer
            if (gotDrink) yield break; 

            yield return new WaitForSeconds(1);
            timer--;
        }

        reaction.React("angry");
        UI.secondsLeft -= 5; // remove from overall timer
        UI.RemoveOrder(order.orderNumber); // Update UI Orders

        StartCoroutine(Leave());

        Debug.Log($"Timer ran out for {customerType} {name}");
    }

    public void AssignTable(Table table)
    {
        chosenTable = table;
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
