using System.Collections;
using UnityEngine;

public class CoffeeBeans : MonoBehaviour
{
    [SerializeField] private EspressoGrinder espressoGrinder;

    private void OnMouseDown()
    {
        Debug.Log("Attempting to Fill");
        espressoGrinder.Fill(); // With coffee beans
    }
}
