using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPoint = new Vector3 (-11.5f, -1f, 0);



    [SerializeField] private Vector3 sittingPosition = Vector3.zero;
    [SerializeField] private bool isSitting = false;


    [SerializeField] private Player player;
    [SerializeField] private TableManager tableManager;

    [SerializeField] private NavMeshAgent agent;

    private Order order;
    

    private void Start()
    {
        // Get Connections
        player = FindFirstObjectByType<Player>();
        tableManager = FindFirstObjectByType<TableManager>();
        agent = GetComponent<NavMeshAgent>();

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
    }

    private void OnMouseDown()
    {
        // Set the player's position to the customer
        //player.gameObject.transform.position = gameObject.transform.position;
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

        // Start Order
        StartCoroutine(OrderDrink());
    }

    private IEnumerator OrderDrink()
    {
        while (!isSitting)
        {
            yield return null;
        }

        // more code

    }

    public Order Order { get; set; }
}
