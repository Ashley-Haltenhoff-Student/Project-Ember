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
    [SerializeField] private NotifyManager notifyManager;

    [Header("Customer Data")]
    [SerializeField] private List<Customer> customers;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private int maxCustomers;
    [SerializeField] private float secondsBetweenSpawn;
    [SerializeField] private string[] possibleNames;

    private List<string> customerTypes = new();
    private string lastCustomerTypeChosen;
    private int normalCustomerStreak = 0;
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

        // Notify business choice when game starts
        notifyManager.Notify("Business chosen: " + settings.customerBusiness);

        StartCoroutine(Spawning());
    }


    private IEnumerator Spawning()
    {

        while (true)
        {
            // Ensure there are open tables or too many customers
            if (tableManager.OpenTables.Count >= 1 || customers.Count < maxCustomers)
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
        // If there's been at 1 normal customer in between and there are unique types chosen from settings
        if (normalCustomerStreak > 1 && customerTypes.Count > 0) 
        {
            bool customerFound = false;
            string chosenCustomer = "normal";

            while (!customerFound) // Until a unique customer type is found that wasn't used last time
            {
                chosenCustomer = customerTypes[Random.Range(0, customerTypes.Count)];

                if (chosenCustomer != lastCustomerTypeChosen || customerTypes.Count == 1)
                {
                    customerFound = true;
                    lastCustomerTypeChosen = chosenCustomer; // Update the last chosen unique customer
                }
            }

            // Set the customer type
            customer.customerType = chosenCustomer;
            normalCustomerStreak = 0;
        }
        else // Else just be a normal customer
        { 
            customer.customerType = "normal"; 
            normalCustomerStreak++; 
        } 

    }
}
