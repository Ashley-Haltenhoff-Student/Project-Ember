
using UnityEngine;
using UnityEngine.UI;

public class UIOrder : MonoBehaviour
{
    public int orderNum;

    [SerializeField] private Text orderNumText;
    [SerializeField] private Text nameText;
    [SerializeField] private Text customerNameText;

    public void Initiatialize(string customerName, string orderName, int orderNumber)
    {
        orderNumText.text = orderNumber.ToString();
        orderNum = orderNumber;
        nameText.text = orderName;
        customerNameText.text = customerName;
    }
}
