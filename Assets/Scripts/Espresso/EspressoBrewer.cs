using System.Collections;
using UnityEngine;

public class EspressoBrewer : LockSpot
{
    [SerializeField] private bool canBrew = false;
    

    protected override void IsLockedActions()
    {

        if (canBrew)
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
        canBrew = false;
    }

    public void SetCanBrew(bool canBrew)
    {
        this.canBrew = canBrew;
    }

}
