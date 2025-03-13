using System.Collections;
using UnityEngine;

public class EspressoBrewer : LockSpot
{
    [SerializeField] private EspressoControl control;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (!control)
        {
            control = FindFirstObjectByType<EspressoControl>();
        }
    }

    protected override void IsLockedActions()
    {

        if (control.CanBrew())
        {
            StartCoroutine(Brew());
        }
    }

    private IEnumerator Brew()
    {
        canRemove = false;

        notifyManager.Notify("Brewing...");
        yield return new WaitForSeconds(3);
        notifyManager.Notify("Brewing complete!");

        canRemove = true;
        control.CanBrew(false);
    }
}
