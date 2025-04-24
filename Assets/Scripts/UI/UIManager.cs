using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Pop Up Windows")]
    [SerializeField] private GameObject journal;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject scoreMenu;

    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject tutorialMenu;
    [SerializeField] private GameObject welcomeScreen;


    [SerializeField] private Text scoreMenuScore;

    [Header("Other Objects")]
    [SerializeField] private GameObject generalUI;
    [SerializeField] private GameObject eToInteract;
    [SerializeField] private GameObject journalIcon;
    [SerializeField] private Text minutesDisplay;
    [SerializeField] private Text secondsDisplay;
    [SerializeField] private Text scoreDisplay;

    public bool applianceWindowOpen;

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
        events.ShopOpen.AddListener(ShopOpen);
        events.GameStart.AddListener(GameStart);
        events.GameEnd.AddListener(GameEnd);

        Time.timeScale = 1; // In case it's paused from Game End
    }

    private void ShopOpen()
    {
        StartCoroutine(Timer()); // start timer

        
    }

    private void GameStart()
    {
        // Validate if journal is open for use or not
        if (!settings.allowJournal)
        {
            journalIcon.SetActive(false);
        }

        StartCoroutine(PauseMenuCoroutine()); // Wait for pause menu
    }

    public void GameRestart()
    {
        scoreMenu.SetActive(false);
        welcomeScreen.SetActive(false);
        tutorialMenu.SetActive(false);

        settingsCanvas.SetActive(true);
        settingsMenu.SetActive(true);

        Time.timeScale = 1; // In case it's paused from Game End
    }

    private void GameEnd()
    {
        Time.timeScale = 0;

        scoreMenuScore.text = scoreDisplay.text;
        scoreMenu.SetActive(true);
        
    }

    private IEnumerator Timer()
    {
        int minutesLeft = 1;
        int secondsLeft = 0;

        minutesDisplay.text = minutesLeft.ToString();

        while (true)
        {
            yield return new WaitForSeconds(1);
            secondsLeft--;

            // If it's passed zero, lower minutes
            if (secondsLeft == -1)
            {
                secondsLeft = 59;
                minutesLeft--;

                if (minutesLeft == -1) { break; } // If the timer has reached the 0:00, then stop

                minutesDisplay.text = minutesLeft.ToString();

            }

            // Display with a zero if it's less than 10
            if ( secondsLeft < 10 )
            {
                secondsDisplay.text = "0" + secondsLeft.ToString();
            }
            else
            {
                secondsDisplay.text = secondsLeft.ToString();
            }
        }

        // Game is over
        events.TriggerEvent(events.GameEnd);
    }

    private IEnumerator PauseMenuCoroutine()
    {
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Escape) && applianceWindowOpen == false)
            {

                TogglePause();
            }
            yield return null;
        }
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

    public UIOrder AddOrder(Order order, string customerName, string customerType)
    {
        GameObject obj = Instantiate(orderUIPrefab, orders.transform);
        uiOrderObjs.Add(order.orderNumber, obj);

        UIOrder uiOrder = obj.GetComponent<UIOrder>();
        uiOrder.Initiatialize(customerName, customerType, order.name, order.orderNumber); // set values
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

    public void OnCustomerHover(string customerName, string orderName, string customerType, int timerSecondsLeft, Vector2 cursorPos)
    {
        hoverObject.transform.position = new Vector2(cursorPos.x + 50, cursorPos.y + 50);

        // Display customer type unless normal 
        if (customerType != "normal")
        {
            hoverObject.GetComponentInChildren<Text>().text = $"{customerType} {customerName} wants {orderName} - seconds left: {timerSecondsLeft}";
        }
        else
        {
            hoverObject.GetComponentInChildren<Text>().text = $"{customerName} wants {orderName} - {timerSecondsLeft}";
        }

        RectTransform rect = hoverObject.GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);

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
            Time.timeScale = 1;
        }
        else
        {
            journal.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void UpdateScore(string score)
    {
        scoreDisplay.text = score;
    }
}
