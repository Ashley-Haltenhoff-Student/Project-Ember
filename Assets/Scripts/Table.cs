using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private Vector2[] seatPositions;
    [SerializeField] private bool isOccupied = false;

    public bool IsOccupied
    {
        get { return isOccupied; }
        set { isOccupied = value; }
    }

    public Vector2[] SeatPositions => seatPositions;

}
