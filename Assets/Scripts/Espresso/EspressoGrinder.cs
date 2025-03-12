using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressoGrinder : LockSpot
{

    private bool isFilled = false; // Filled with coffee beans
    private bool canGrind = true;

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
        if (isFilled && canRemove && canGrind)
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
            notifyManager.Notify("There's already coffee beans in the grinder");
        }
        else
        {
            isFilled = true;
            notifyManager.Notify("Espresso machine filled");

            if (isLocked)
            {
                IsLockedActions();
            }
        }
    }

    private IEnumerator Grind()
    {  

        notifyManager.Notify("Grinding...");
        yield return new WaitForSeconds(4);
        notifyManager.Notify("Grinding complete!");

        canRemove = true;
        isFilled = false;
    }

    public bool IsFilled() {  return isFilled; }

    public void SetCanGrind(bool canGrind)
    {
        this.canGrind = canGrind;
    }
}
