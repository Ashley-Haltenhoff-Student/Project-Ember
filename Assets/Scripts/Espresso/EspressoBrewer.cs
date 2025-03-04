using System.Collections;
using UnityEngine;

public class EspressoBrewer : LockSpot
{
    private bool canBrew = false;

    protected override void IsLockedActions()
    {
        if (canBrew)
        {
            Debug.Log("Brewing...");
        }
    }
}
