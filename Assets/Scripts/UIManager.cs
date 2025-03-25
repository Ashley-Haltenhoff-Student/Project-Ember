using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Pop Up Windows")]
    [SerializeField] private GameObject espressoWindow;
    [SerializeField] private GameObject coffeeWindow;

    [Header("Other")]
    [SerializeField] private GameObject gameplaySettings;
    [SerializeField] private GameObject generalUI;
    [SerializeField] private GameObject journal;
    [SerializeField] private GameObject pauseMenu;

    [Header("Orders")]
    [SerializeField] private GameObject orderUIPrefab;
    [SerializeField] private GameObject orders;

    private List<GameObject> uiOrderObjs = new List<GameObject>();



    public UIOrder AddOrder(Order order, string customerName, List<UIOrder> uiOrders)
    {
        GameObject obj = Instantiate(orderUIPrefab, this.orders.transform);
        uiOrderObjs.Add(obj);
        
        UIOrder uiOrder = obj.GetComponent<UIOrder>();
        uiOrder.Initiatialize(customerName, order.Name, order.OrderNumber);

        UpdateOrderPos();

        return uiOrder;
    }


    public void RemoveOrder(Order orderToRemove)
    {
        foreach (GameObject obj in uiOrderObjs)
        {
            if (obj.GetComponent<UIOrder>().orderNum == orderToRemove.OrderNumber)
            {
                uiOrderObjs.Remove(obj);
                obj.SetActive(false);
                Destroy(obj);
                UpdateOrderPos() ;
                return;
            }
        }
    }

    // Method to update positions of the UI orders
    public void UpdateOrderPos()
    {
        Vector2 startPosition = new Vector2(1795, 980);

        for (int i = 0; i < uiOrderObjs.Count; i++)
        {
            GameObject orderGameObject = uiOrderObjs[i];

            // Assuming the GameObject has a RectTransform (because it's a UI element)
            if (orderGameObject != null)
            {
                RectTransform rectTransform = orderGameObject.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    Vector2 newPos = new Vector2(startPosition.x + (-250 * i), 980);
                    rectTransform.position = newPos;
                }
            }
        }
    }

    //public void RemoveOrder(Order order)
    //{
    //    foreach (UIOrder o in orders.GetComponentsInChildren<UIOrder>())
    //    {
    //        if (o.orderNum == order.OrderNumber && o.orderName == order.Name)
    //        {
    //            Debug.Log($"order {o.orderName} with order number {order.OrderNumber} and ui order {order.Name} with order number {o.orderNum} matched");

    //            Debug.Log("Destroying " +  o.orderNum);
    //            Destroy(o.gameObject);
    //            UpdateOrderPos();
    //            return;
    //        }
    //    }

    //    Debug.Log("No order matched found to be destroyed in the UI");
    //}


    //public void UpdateOrderPos()
    //{
    //    UIOrder[] uiOrders = orders.GetComponentsInChildren<UIOrder>();

    //    Vector2 startPosition = new Vector2(1795, 980);

    //    for (int i = 0; i < uiOrders.Length; i++)
    //    {
    //        // If is on the first order in the array
    //        if (i == 0)
    //        {
    //            uiOrders[i].gameObject.GetComponent<RectTransform>().position = startPosition;
    //        }
    //        else
    //        {
    //            // Calculate the distance horizontally by the amount of gameobjects in the array
    //            float horizontalPos = startPosition.x + (-250 * i);
    //            Vector2 newPos = new Vector2(horizontalPos, 980);

    //            uiOrders[i].gameObject.GetComponent<RectTransform>().position = newPos;
    //        }

    //    }
    //}
}
