using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomerManager : MonoBehaviour
{
    
    [Header("Connections")]
    [SerializeField] private TableManager tableManager;
    [SerializeField] private GlobalEvents events;
    [SerializeField] private SettingsManager settings;

    [Header("Customer Data")]
    [SerializeField] private List<Customer> customers;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private int maxCustomers = 2;
    [SerializeField] private float secondsBetweenSpawn = 3f;
    [SerializeField] private string[] possibleNames;


    private void Start()
    {
        events.GameStart.AddListener(StartSpawning);

        events.CustomerLeft.AddListener(DestroyCustomer);
    }

    public void StartSpawning()
    {
        

        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {

        while (true)
        {
            
            // Ensure there are open tables
            if (tableManager.OpenTables.Count < 1 && customers.Count > maxCustomers)
            {
                break;
            }
            else
            {
                // Find current spawn rate
                yield return new WaitForSeconds(CalculateSpawnRate());

                CreateAndSpawnCustomer();
            }

            yield return null;
        }
    }

    private void DestroyCustomer()
    {
        foreach (Customer c in customers)
        {
            if (c.IsGone)
            {
                customers.Remove(c);
                Destroy(c.gameObject);
                break;
            }
        }

        StartCoroutine(Spawning());
    }

    private float CalculateSpawnRate()
    {
        // 

        return 0;
    }

    private void CreateAndSpawnCustomer()
    {
        GameObject newCustomer = Instantiate(customerPrefab);
        Customer customer = newCustomer.GetComponent<Customer>(); // access the script

        // Update values
        customer.Name = possibleNames[Random.Range(0, possibleNames.Length)];
        customer.events = events; // Global Unity Events
        customer.tableManager = tableManager;

        customers.Add(customer); // Add to list


        customer.Spawn(); // Initialize spawning in script
    }
}
