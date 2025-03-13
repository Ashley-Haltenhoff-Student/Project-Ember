using System.Collections;
using UnityEngine;

public class EspressoGrinder : LockSpot
{

    private bool isFilled = false; // Filled with coffee beans

    [SerializeField] private EspressoControl control;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (!control)
        {
            control = FindFirstObjectByType<EspressoControl>();
        }
    }

    public override void StopWaiting()
    {
        base.StopWaiting();

        if (isLocked && !isFilled)
        {
            notifyManager.Notify("There are no coffee beans ready to grind!");
        }
    }

    protected override void IsLockedActions()
    {
        if (isFilled && canRemove && control.CanGrind())
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

        control.PortHasGrinds(true);
        canRemove = true;
        isFilled = false;
    }

    public bool IsFilled() {  return isFilled; }
}
