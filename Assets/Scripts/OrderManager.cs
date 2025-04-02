using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private UIManager UI;

    [SerializeField] private Order[] possibleOrders;
    [SerializeField] private List<Order> orders;
    [SerializeField] private List<UIOrder> uiOrders;

    private int lastOrderNum = 1;

    public void GetNewOrder(Order order, string customerName)
    {
        Order chosenOrder = possibleOrders[Random.Range(0, possibleOrders.Length)];

        // Update Values
        order.orderNumber = lastOrderNum++;
        order.name = chosenOrder.name;
        order.sprite = chosenOrder.sprite;

        Add(order, customerName);

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
