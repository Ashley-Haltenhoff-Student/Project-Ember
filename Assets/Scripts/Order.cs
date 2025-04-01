using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private new string name;
    [SerializeField] private int orderNumber;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (!spriteRenderer)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
    }

    public string Name
    {
        get { return name; } 
        set { name = value; }
    }

    public int OrderNumber
    {
        get { return orderNumber; }
        set { orderNumber = value; }
    }

    public SpriteRenderer SpriteRenderer
    {
        get { return spriteRenderer; }
        set { spriteRenderer = value; }
    }
    
}
