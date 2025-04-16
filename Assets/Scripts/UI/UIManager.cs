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
    [SerializeField] private GameObject journal;

    [Header("Other Objects")]
    [SerializeField] private GameObject generalUI;
    [SerializeField] private GameObject eToInteract;
    [SerializeField] private GameObject journalIcon;
    [SerializeField] private GameObject pauseMenu;

    [Header("Connections")] 
    [SerializeField] private SettingsManager settings;
    [SerializeField] private GlobalEvents events;

    [Header("Orders")]
    [SerializeField] private GameObject orderUIPrefab;
    [SerializeField] private GameObject orders;
    [SerializeField] private GameObject hoverObject;

    private Dictionary<int, GameObject> uiOrderObjs = new Dictionary<int, GameObject>();


    private void Start()
    {
        events.GameStart.AddListener(GameStart);
    }

    public void TogglePause()
    {
        if (Time.timeScale > 0)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void GameStart()
    {
        // Validate if journal is open for use or not
        if (!settings.allowJournal)
        {
            journalIcon.SetActive(false);
        }

        StartCoroutine(PauseMenuCoroutine());
    }

    private IEnumerator PauseMenuCoroutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (espressoWindow.transform.position != Vector3.zero &&
                    coffeeWindow.transform.position != Vector3.zero)
                {
                    TogglePause();
                }
            }
            yield return null;
        }
    }


    public UIOrder AddOrder(Order order, string customerName)
    {
        GameObject obj = Instantiate(orderUIPrefab, orders.transform);
        uiOrderObjs.Add(order.orderNumber, obj);

        UIOrder uiOrder = obj.GetComponent<UIOrder>();
        uiOrder.Initiatialize(customerName, order.name, order.orderNumber); // set values
        StartCoroutine(
                UpdateOrderPos());
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
            StartCoroutine(
                        UpdateOrderPos());
        }

    }

    // Method to update positions of the UI orders
    public IEnumerator UpdateOrderPos()
    { 
        yield return new WaitForSeconds(1);

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

    // Following functions are interactions of objects

    public void OnCustomerHover(string customerName, string orderName, Vector2 cursorPos)
    {
        hoverObject.transform.position = new Vector2(cursorPos.x + 50, cursorPos.y + 50);
        hoverObject.GetComponentInChildren<Text>().text = $"{customerName} wants {orderName}";

        RectTransform rect = hoverObject.GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 70);

        hoverObject.SetActive(true);
    }

    public void OnInventoryHover(string itemName, Vector2 cursorPos)
    {
        hoverObject.transform.position = new Vector2(cursorPos.x + 50, cursorPos.y + 50);
        hoverObject.GetComponentInChildren<Text>().text = itemName;

        RectTransform rect = hoverObject.GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);

        hoverObject.SetActive(true);
    }

    public void OnInventoryHoverLeave() { hoverObject.SetActive(false); }

    // When the mouse isn't hovering over the customer anymore
    public void OnCustomerCursorLeave()
    {
        hoverObject.SetActive(false);
    }

    public void EToInteract(Vector3 position)
    {
        eToInteract.transform.position = Camera.main.WorldToScreenPoint(position);
        eToInteract.SetActive(true);
    }

    public void HideEToInteract() { eToInteract.SetActive(false); }

    public void ToggleJournal()
    {
        if (journal.activeSelf)
        {
            journal.SetActive(false);
        }
        else
        {
            journal.SetActive(true);
        }
    }
}
