using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private UIManager UI;

    [SerializeField] private Order[] possibleOrders;
    [SerializeField] private List<Order> orders;
    [SerializeField] private List<UIOrder> uiOrders;

    private int lastOrderNum = 1;

    public Order GetNewOrder(string customerName)
    {
        Order order = possibleOrders[Random.Range(0, possibleOrders.Length)];
        order.OrderNumber = lastOrderNum++;

        Add(order, customerName);

        return order;
    }

    public void Add(Order order, string customerName)
    {
        // Add to Lists
        orders.Add(order);
        UIOrder uiOrder = UI.AddOrder(order, customerName); // Update UI
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
