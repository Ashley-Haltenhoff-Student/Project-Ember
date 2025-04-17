
using UnityEngine;
using UnityEngine.UI;

public class UIOrder : MonoBehaviour
{
    public int orderNum;
    public string orderName;

    [SerializeField] private Text orderNumText;
    [SerializeField] private Text nameText;
    [SerializeField] private Text customerNameText;

    public void Initiatialize(string customerName, string customerType, string orderName, int orderNumber)
    {
        orderNumText.text = orderNumber.ToString();
        orderNum = orderNumber;

        this.orderName = orderName;
        if (customerType != "normal")
        {
            nameText.text = customerType + " " + orderName;
        }
        else
        {
            nameText.text = orderName;
        }

        customerNameText.text = customerName;
    }
}
