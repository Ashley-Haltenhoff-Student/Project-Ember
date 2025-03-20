using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent customerLeft = new UnityEvent();

    public UnityEvent CustomerLeft
    {
        get { return customerLeft; }
    }

    public void TriggerEvent(UnityEvent givenEvent)
    {
        givenEvent?.Invoke();
    }
}
