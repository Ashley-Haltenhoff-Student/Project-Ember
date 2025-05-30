using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private UIManager UI;

    [SerializeField] private Order[] possibleOrders;
    [SerializeField] private List<Order> orders;
    [SerializeField] private List<UIOrder> uiOrders;

    private int lastOrderNum = 1;

    public void GetNewOrder(Order order, string customerName, string customerType)
    {
        Order chosenOrder = possibleOrders[Random.Range(0, possibleOrders.Length)];

        // Update Values
        order.orderNumber = lastOrderNum++;
        order.sprite = chosenOrder.sprite;

        // If they're confused, choose a the chosen orders confused name
        if (customerType == "confused")
        {
            order.name = chosenOrder.confusedName;
        }
        else
        {
            order.name = chosenOrder.name;
        }

        Add(order, customerName, customerType);

    }

    public void Add(Order order, string customerName, string customerType)
    {
        // Add to Lists
        orders.Add(order);
        UIOrder uiOrder = UI.AddOrder(order, customerName, customerType); // Update UI
        uiOrders.Add(uiOrder);
    }

    public void UpdateOrders()
    {
        foreach (UIOrder order in uiOrders)
        {
            if (!order)
            {
                uiOrders.Remove(order);
            }
        }

        foreach (Order order in orders)
        {
            if (!order)
            {
                orders.Remove(order);
            }
        }
    }
}
