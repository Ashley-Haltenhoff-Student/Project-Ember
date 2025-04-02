using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Tilemaps;

public class InventorySpot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UIManager UI;
    public Image image;
    public string itemName;

    private Vector2 hoverObjPos;

    private void Start()
    {
        hoverObjPos = new Vector2(gameObject.transform.position.x - 50, gameObject.transform.position.y + 25);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        UI.OnInventoryHover(itemName, hoverObjPos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI.OnInventoryHoverLeave();
    }
}
