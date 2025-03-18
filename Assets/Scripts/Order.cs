using UnityEngine;

public class Order : MonoBehaviour
{
    private new string name;   
    private SpriteRenderer spriteRenderer;

    private void Awake()
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
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    
}
