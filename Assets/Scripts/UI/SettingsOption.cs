using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string description;

    //[SerializeField] private Button button;
    [SerializeField] private GameObject hoverObj;
    private Text hoverText;

    private void Start()
    {
        hoverText = hoverObj.GetComponentInChildren<Text>();
        //button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverObj.transform.position = new(transform.position.x + 150, transform.position.y + 75);
        hoverText.text = description;
        hoverObj.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverObj.SetActive(false);
        hoverText.text = "";
    }
}
