using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent customerLeft = new();
    [SerializeField] private UnityEvent espressoMade = new();
    [SerializeField] private UnityEvent coffeeMade = new();

    public UnityEvent CustomerLeft
    {
        get { return customerLeft; }
    }

    public UnityEvent EspressoMade
    {
        get { return espressoMade; }
    }

    public UnityEvent CoffeeMade
    {
        get { return coffeeMade; }
    }

    public void TriggerEvent(UnityEvent givenEvent)
    {
        givenEvent?.Invoke();
    }
}
