using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private UIManager UI;

    [SerializeField] private List<Order> orders;
    [SerializeField] private List<UIOrder> uiOrders;

    public Order GetNewOrder()
    {
        // Random orders code here
        return null;
    }

    public void Add(Order order, string customerName)
    {
        orders.Add(order);
        // More code here


        UI.AddOrder(order, customerName);
    }

}
