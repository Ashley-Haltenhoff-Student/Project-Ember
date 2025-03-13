
using System;
using UnityEngine;

public class Portafilter : DraggableObject
{

    [Header("Connections")]
    [SerializeField] private EspressoBrewer brewer;
    [SerializeField] private EspressoGrinder grinder;
    [SerializeField] private EspressoControl control;

    private void Start()
    {
        base.Start();

        if (!brewer)
        {
            brewer = FindFirstObjectByType<EspressoBrewer>();
        }

        if (!grinder)
        {
            grinder = FindFirstObjectByType<EspressoGrinder>();
        }

        if (!control)
        {
            control = FindFirstObjectByType<EspressoControl>();
        }
    }

    private void OnMouseDrag()
    {
        base.OnMouseDrag();

        if (!control.PortHasGrinds())
        {
            control.CanGrind(true);
            control.CanBrew(false);

        }
        else
        {
            control.CanGrind(false);
            control.CanBrew(true);
        }
    }

    private void Update()
    {

        // Verify if the portafilter is locked into the coffee grinder
        if (lastLockPoint == brewer.gameObject && control.PortHasGrinds())
        {
            control.CanBrew(true);
            control.PortHasGrinds(false);
        }

        if (lastLockPoint == grinder.gameObject && !control.PortHasGrinds())
        {
            control.PortHasGrinds(true);
        }
    }
}
