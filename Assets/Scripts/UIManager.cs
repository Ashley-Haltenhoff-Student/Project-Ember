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
    [SerializeField] private GameObject customerHoverObject;

    private Dictionary<int, GameObject> uiOrderObjs = new Dictionary<int, GameObject>();



    public UIOrder AddOrder(Order order, string customerName)
    {
        GameObject obj = Instantiate(orderUIPrefab, orders.transform);
        uiOrderObjs.Add(order.OrderNumber, obj);

        UIOrder uiOrder = obj.GetComponent<UIOrder>();
        uiOrder.Initiatialize(customerName, order.Name, order.OrderNumber); // set values

        UpdateOrderPos();

        return uiOrder;
    }


    public void RemoveOrder(int orderNum)
    {
        StartCoroutine(RemoveOrderCoroutine(orderNum));
    }

    private IEnumerator RemoveOrderCoroutine(int orderNum)
    {
        if (uiOrderObjs.ContainsKey(orderNum))
        {
            Destroy(uiOrderObjs[orderNum]); // Destroy order
            uiOrderObjs.Remove(orderNum); // Remove from Dictionary

            // Wait until the object is truly destroyed
            yield return new WaitUntil(() =>  !uiOrderObjs.ContainsKey(orderNum));

            UpdateOrderPos(); // Update positions
            
        }

    }

    // Method to update positions of the UI orders
    public void UpdateOrderPos()
    { 

        Vector2 startPosition = new Vector2(1795, 980);

        int i = 0;
        foreach (var orderEntry in uiOrderObjs)
        {
            GameObject orderGameObject = orderEntry.Value;

            // Assuming the GameObject has a RectTransform (because it's a UI element)
            if (orderGameObject != null)
            {
                if (orderGameObject.TryGetComponent<RectTransform>(out var rectTransform))
                {
                    Vector2 newPos = new Vector2(startPosition.x + (-250 * i), 980);
                    rectTransform.position = newPos;
                    i++;
                }
            }
        }
    }

    public void OnCustomerHover(string customerName, string orderName, Vector2 cursorPos)
    {
        customerHoverObject.transform.position = cursorPos;
        customerHoverObject.GetComponentInChildren<Text>().text = $"{customerName} wants a(n) {orderName}";

        customerHoverObject.SetActive(true);
    }

    // When the mouse isn't hovering over the customer anymore
    public void OnCustomerCursorLeave()
    {
        customerHoverObject.SetActive(false);
    }
}
