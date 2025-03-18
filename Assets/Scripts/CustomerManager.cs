using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [Header("Connections")]
    [SerializeField] private UIManager UIManager;
    [SerializeField] private TableManager tableManager;
    [SerializeField] private OrderManager orderManager;

    [Header("Customer Data")]
    [SerializeField] private List<Customer> customers;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private int maxCustomers = 2;
    [SerializeField] private float secondsBetweenSpawn = 3f;

    private void Start()
    {
        GameObject newCustomer = Instantiate(customerPrefab);
        customers.Add(newCustomer.GetComponent<Customer>());

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
            if (customers.Count < maxCustomers)
            {
                yield return new WaitForSeconds(secondsBetweenSpawn);

                GameObject newCustomer = Instantiate(customerPrefab);
                Customer customer = newCustomer.GetComponent<Customer>(); // access the script

                customers.Add(customer); // Add to List
                customer.Order = orderManager.GetNewOrder(); // Assign Order
            }
            yield return null;
        }
    }
}
