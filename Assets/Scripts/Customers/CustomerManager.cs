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
    [SerializeField] private int maxCustomers;
    [SerializeField] private float secondsBetweenSpawn;
    [SerializeField] private string[] possibleNames;

    private List<string> customerTypes = new();
    private string lastCustomerTypeChosen;
    private int customersSinceNormal = 0;
    private int spawnRate = 5;


    private void Start()
    {
       
        // Event listeners
        events.GameStart.AddListener(StartSpawning);
        events.CustomerLeft.AddListener(DestroyCustomer);
    }

    public void StartSpawning()
    {
        // Update Settings
        maxCustomers = settings.maxCustomers;
        spawnRate = settings.spawnRate;

        // Add to customer types to know which ones are available to choose from when spawning
        if (settings.impatientCustomers) { customerTypes.Add("impatient"); }
        if (settings.confusedCustomers) { customerTypes.Add("confused"); }
        if (settings.uncertainCustomers) { customerTypes.Add("uncertain"); }

        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {

        while (true)
        {
            // Ensure there are open tables or too many customers
            if (tableManager.OpenTables.Count < 1 || customers.Count >= maxCustomers)
            {
                break;
            }
            else
            {
                // Find current spawn rate
                yield return new WaitForSeconds(spawnRate);

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


    private void CreateAndSpawnCustomer()
    {
        GameObject newCustomer = Instantiate(customerPrefab);
        Customer customer = newCustomer.GetComponent<Customer>(); // access the script

        SetCustomerType(customer);

        // Update values
        customer.Name = possibleNames[Random.Range(0, possibleNames.Length)];
        customer.events = events; // Global Unity Events
        customer.tableManager = tableManager;

        customers.Add(customer); // Add to list


        customer.Spawn(); // Initialize spawning in script
    }

    private void SetCustomerType(Customer customer)
    {
        
        if (customersSinceNormal >= Random.Range(1, 2)) // If there's been at 1-2 normal customers in between
        {
            bool customerFound = false;
            string chosenCustomer = "normal";

            while (!customerFound) // Until a unique customer type is found that wasn't used last time
            {
                chosenCustomer = customerTypes[Random.Range(0, customerTypes.Count - 1)];

                if (chosenCustomer != lastCustomerTypeChosen)
                {
                    customerFound = true;
                    lastCustomerTypeChosen = chosenCustomer; // Update the last chosen unique customer
                }
            }

            // Set the customer type
            customer.customerType = chosenCustomer;
        }
        else { customer.customerType = "normal"; customersSinceNormal++; } // Else just be a normal customer


        // A switch statement that updates values specifically for the customer type
        // Update the timer, 
        switch (customer.customerType)
        {
            case "impatient":
                customer.timer = 30.0f;
                //
                break;
            case "confused":
                //
                break;
            case "uncertain":
                //
                break;
            case "normal":
            default:
                customer.timer = 60.0f;
                //
                break;
        }
    }
}
