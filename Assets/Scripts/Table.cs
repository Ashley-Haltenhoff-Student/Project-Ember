using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private Vector2[] seatPositions;
    [SerializeField] private bool isOccupied = false;


    public Vector2[] GetSeatPositions() { return seatPositions; }

    public bool IsOccupied() { return isOccupied; }

    public void SetIsOccupied(bool isBusy) { isOccupied = isBusy; }
}
