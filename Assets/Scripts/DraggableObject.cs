using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In the context of an object within a window
public class DraggableObject : MonoBehaviour
{
    [Tooltip("The lock spots in the window this object can lock into")]
    [SerializeField] protected LockSpot[] lockSpots;

    [Tooltip("The position the object returns to if it cannot lock onto another object (automatically set at start-up)")]
    [SerializeField] protected Vector3 returnPoint;


    [SerializeField] protected bool canMove = true;

    protected GameObject lastLockPoint;

    protected void Start()
    {
        returnPoint = transform.position;
    }

    protected void OnMouseDrag()
    { 
        // Check if object is locked on any object to determine if it can move
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
            // Get mouse position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
            // Clamp between the windows borders
            mousePos.x = Mathf.Clamp(mousePos.x, -8.5f, 8.5f);
            mousePos.y = Mathf.Clamp(mousePos.y, -5, 5);

            transform.position = new Vector3(mousePos.x, mousePos.y, 0);

            foreach (LockSpot s in lockSpots)
            {
                // Start lock spots' ability to lock the object in place
                s.BeginLockableState(gameObject);
            }
        }
    }

    protected void OnMouseUp()
    {
        bool isLocked = false;

        foreach (LockSpot s in lockSpots)
        {
            s.StopWaiting();

            // Verify if it's locked
            if (s.IsLocked())
            {
                isLocked = true;
                lastLockPoint = s.gameObject;
            }
        }

        if (!isLocked) { transform.position = returnPoint; }
    }
}
