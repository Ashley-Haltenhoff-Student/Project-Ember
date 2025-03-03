using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In the context of an object within a window
public class DraggableObject : MonoBehaviour
{
    [Tooltip("The lock spots in the window this object can lock into")]
    [SerializeField] private LockSpot[] lockSpots;

    [Tooltip("The position the object returns to if it cannot lock onto another object")]
    [SerializeField] protected Vector3 returnPoint;


    [SerializeField] private bool canMove = true;

    private void Start()
    {
        returnPoint = transform.position;
    }

    private void OnMouseDrag()
    { 
        foreach (LockSpot s in lockSpots)
        {
            if (!s.CanRemove())
            {
                canMove = false; break;
            }
            else
            {
                canMove = true;
            }
        }

        if (canMove)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
            // Clamp between the windows borders
            mousePos.x = Mathf.Clamp(mousePos.x, -8.5f, 8.5f);
            mousePos.y = Mathf.Clamp(mousePos.y, -5, 5);

            transform.position = new Vector3(mousePos.x, mousePos.y, 0);

            foreach (LockSpot s in lockSpots)
            {
                s.BeginLockableState(gameObject);
            }
        }
    }

    private void OnMouseUp()
    {
        bool isLocked = false;

        foreach (LockSpot s in lockSpots)
        {
            s.StopWaiting();

            // Verify if it's locked
            if (s.IsLocked())
            {
                isLocked = true;
            }
        }

        if (!isLocked) { transform.position = returnPoint; }
    }
}
