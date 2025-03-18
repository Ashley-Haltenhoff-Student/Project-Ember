using UnityEngine;

public class Order : MonoBehaviour
{
    private new string name;
    private int orderNumber;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (!spriteRenderer)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.Log("There is no sprite renderer on  " + name);
        }
    }

    public string Name
    {
        get { return name; } 
        set { name = value; }
    }

    public int OrderNumber { get; set; }

    public SpriteRenderer SpriteRenderer => spriteRenderer;
    
}
