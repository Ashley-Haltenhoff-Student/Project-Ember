
using System;
using UnityEngine;

public class Portafilter : DraggableObject
{
    // Use this class for animations and sprite changes
    // deprives from draggable object so do not delete

    [SerializeField] private GlobalEvents events;

    private void Start()
    {
        base.Start();

        events.EspressoMade.AddListener(ResetPosition);
    }
}
