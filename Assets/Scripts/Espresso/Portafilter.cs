
using System;
using UnityEngine;

public class Portafilter : DraggableObject
{
    private bool hasCoffeeGrinds = false;

    [SerializeField] private EspressoBrewer brewer;
    [SerializeField] private EspressoGrinder grinder;


    private void Start()
    {
        base.Start();

        if (!brewer)
        {
            Debug.Log("Component Error: There is no brewer available");
        }

        if (!grinder)
        {
            Debug.Log("Component Error: There is no grinder available");
        }
    }

    private void OnMouseDrag()
    {
        base.OnMouseDrag();

        if (!hasCoffeeGrinds)
        {
            brewer.SetCanBrew(false);
            grinder.SetCanGrind(true);
        }
        else
        {
            brewer.SetCanBrew(true);
            grinder.SetCanGrind(false);
        }
    }

    private void OnMouseUp()
    {
        base.OnMouseUp();


        // Verify if the portafilter is locked into the coffee grinder
        if (lastLockPoint == brewer.gameObject && hasCoffeeGrinds)
        {
            brewer.SetCanBrew(true);
            hasCoffeeGrinds = false;
        }

        if (lastLockPoint == grinder.gameObject && grinder.IsFilled())
        {
            hasCoffeeGrinds = true;
        }
    }
}
