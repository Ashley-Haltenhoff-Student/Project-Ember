using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomerManager : MonoBehaviour
{
    

    [Header("Connections")]
    [SerializeField] private UIManager UI;
    [SerializeField] private TableManager tableManager;
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private GlobalEvents events;

    [Header("Customer Data")]
    [SerializeField] private List<Customer> customers;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private int maxCustomers = 2;
    [SerializeField] private float secondsBetweenSpawn = 3f;
    [SerializeField] private string[] possibleNames;

    private void Start()
    {
        events.CustomerLeft.AddListener(DestroyCustomer);
    }

    public void StartSpawning()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        bool spawning = true; // Change spawning control from another script
        while (spawning)
        {
            // Ensure there are open tables
            if (tableManager.OpenTables.Count <= 0)
            {
                spawning = false;
            }

            // If there are less than the max amount of customers
            if (customers.Count <= maxCustomers)
            {
                yield return new WaitForSeconds(secondsBetweenSpawn);

                GameObject newCustomer = Instantiate(customerPrefab);
                Customer customer = newCustomer.GetComponent<Customer>(); // access the script
                
                // Update values
                customer.OrderManager = orderManager;
                customer.Name = GenerateName();
                customer.events = events; // Unity Events

                customers.Add(customer); // Add to list

                customer.SitAndOrderDrink(); // Wait until seated and order

            }
            else { spawning = false; }
            yield return null;
        }
    }

    private string GenerateName()
    {
        return possibleNames[Random.Range(0, possibleNames.Length)];
    }

    private void DestroyCustomer()
    {
        foreach (Customer c in customers)
        {
            if (c.IsGone)
            {
                UI.RemoveOrder(c.Order);
                //orderManager.UpdateOrders();
                customers.Remove(c);
                Destroy(c.gameObject);
                break;
            }
        }

        StartCoroutine(Spawning());
    }
}
