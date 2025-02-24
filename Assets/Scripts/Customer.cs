using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPoint = new Vector3 (-7.5f, -2.5f, 0);

    [SerializeField] private Player player;
    // table manager connection

    private bool isSitting;


    private void Start()
    {
        //Spawn();
    }

    private void Spawn()
    {
        gameObject.transform.position = spawnPoint;

        OrderingSequence();
    }

    private void OrderingSequence()
    {
        // Find seat // Table Manager
        // Decide on order
        // Display order
    }

    private void OnMouseDown()
    {
        // Set the player's position to the customer
        //player.gameObject.transform.position = gameObject.transform.position;
        player.GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
        Debug.Log("Mouse Down");
    }
}
