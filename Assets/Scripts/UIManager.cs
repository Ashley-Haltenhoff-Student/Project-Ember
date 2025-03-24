using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Pop Up Windows")]
    [SerializeField] private GameObject espressoWindow;
    [SerializeField] private GameObject coffeeWindow;
    [SerializeField] private GameObject sinkWindow;

    [Header("Other")]
    [SerializeField] private GameObject gameplaySettings;
    [SerializeField] private GameObject generalUI;
    [SerializeField] private GameObject journal;
    [SerializeField] private GameObject pauseMenu;

    [Header("Orders")]
    [SerializeField] private GameObject orderUIPrefab;
    [SerializeField] private GameObject orders;



    public UIOrder AddOrder(Order order, string customerName, List<UIOrder> uiOrders)
    {
        GameObject obj = Instantiate(orderUIPrefab, this.orders.transform);
        
        UIOrder uiOrder = obj.GetComponent<UIOrder>();
        uiOrder.Initiatialize(customerName, order.Name, order.OrderNumber);

        UpdateOrderPos();

        return uiOrder;
    }

    public void RemoveOrder(Order order)
    {
        foreach (UIOrder o in orders.GetComponentsInChildren<UIOrder>())
        {
            if (o.orderNum == order.OrderNumber)
            {
                Debug.Log($"order number {order.OrderNumber} and ui order number {o.orderNum}");

                Destroy(o.gameObject);
                UpdateOrderPos();
                return;
            }
        }

        Debug.Log("No order matched found to be destroyed in the UI");
    }


    public void UpdateOrderPos()
    {
        UIOrder[] uiOrders = orders.GetComponentsInChildren<UIOrder>();

        Vector2 startPosition = new Vector2(1795, 980);

        for (int i = 0; i < uiOrders.Length; i++)
        {
            // If is on the first order in the array
            if (i == 0)
            {
                uiOrders[i].gameObject.GetComponent<RectTransform>().position = startPosition;
            }
            else
            {
                // Calculate the distance horizontally by the amount of gameobjects in the array
                float horizontalPos = startPosition.x + (-250 * i);
                Vector2 newPos = new Vector2(horizontalPos, 980);

                uiOrders[i].gameObject.GetComponent<RectTransform>().position = newPos;
            }

        }
    }
}
