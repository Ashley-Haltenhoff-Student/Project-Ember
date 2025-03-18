using System.Collections;
using UnityEngine;

public class Mug : DraggableObject
{
    
    public LockSpot[] LockSpots
    {
        get { return lockSpots; }
        set { lockSpots = value; }
    }
}
