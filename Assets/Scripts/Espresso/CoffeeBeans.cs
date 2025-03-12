using System.Collections;
using UnityEngine;

public class CoffeeBeans : MonoBehaviour
{
    [SerializeField] private EspressoGrinder espressoGrinder;

    private void OnMouseDown()
    {
        espressoGrinder.Fill(); // With coffee beans
    }
}
