using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressoGrinder : LockSpot
{

    private bool isFilled = false; // Filled with coffee beans

    public override void StopWaiting()
    {
        base.StopWaiting();

        if (isLocked && !isFilled)
        {
            Debug.Log("There are no coffee beans ready to grind!");
        }
    }

    protected override void IsLockedActions()
    {
        if (isFilled)
        {
            canRemove = false;
            StartCoroutine(Grind());
        }
        else { canRemove = true; }
    }


    public void Fill() 
    { 
        if (isFilled)
        {
            Debug.Log("There's already coffee beans in the grinder");
        }
        else
        {
            isFilled = true;
            Debug.Log("Espresso machine filled");
        }
    }

    private IEnumerator Grind()
    {
        Debug.Log("Grinding...");
        yield return new WaitForSeconds(4);
        Debug.Log("Grinding complete!");

        canRemove = true;
        isFilled = false;
    }
}
