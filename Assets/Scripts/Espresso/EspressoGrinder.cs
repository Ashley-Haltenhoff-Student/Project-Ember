using System.Collections;
using UnityEngine;

public class EspressoGrinder : LockSpot
{

    private bool isFilled = false; // Filled with coffee beans

    [SerializeField] private EspressoControl control;
    [SerializeField] private Animator espressoAnimator;

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
        if (isFilled)
        {
            canRemove = false;
            control.CanGrind(true);
            StartCoroutine(Grind());
        }
        else 
        { 
            canRemove = true; 
            control.CanGrind(false);
        }
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

            control.CanGrind();
            

            if (isLocked)
            {
                IsLockedActions();
            }
        }
    }

    private IEnumerator Grind()
    {  

        notifyManager.Notify("Grinding...");
        espressoAnimator.SetBool("isGrinding", true);

        yield return new WaitForSeconds(4);

        notifyManager.Notify("Grinding complete!");
        espressoAnimator.SetBool("isGrinding", false);

        control.ToggleEspressoCup(true); // Visually

        control.CanBrew(true);
        control.CanGrind(false);

        canRemove = true;
        isFilled = false;
    }

    public bool IsFilled() {  return isFilled; }
}
