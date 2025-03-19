using System.Collections;
using System.Collections.Generic;
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

        UpdateOrderPos(uiOrders);

        return uiOrder;
    }

    public void RemoveOrder(UIOrder order)
    {
        foreach (UIOrder o in orders.GetComponentsInChildren<UIOrder>())
        {
            if (o ==  order)
            {
                Destroy(o);
                return;
            }
        }

        Debug.Log("No order matched found to be destroyed in the UI");
    }

    public void UpdateOrderPos(List<UIOrder> uiOrders)
    {
        for (int i = 0; i < uiOrders.Count; i++)
        {
            if (i == uiOrders.Count - 1)
            {
                uiOrders[i].transform.localPosition = new Vector2(-100, -75);
            }
            else
            {
                // Calculate the distance horizontally by the amount of gameobjects in the list
                float distanceFromTop = -250 * (uiOrders.Count - i - 1);
                float horizontalPos = -100 - distanceFromTop;

                // move down
                uiOrders[i].transform.localPosition = new Vector3
                    (horizontalPos,
                    uiOrders[i].transform.localPosition.y, 0f);

            }
        }
    }
}
