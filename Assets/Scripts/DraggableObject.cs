using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{


    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }
}
