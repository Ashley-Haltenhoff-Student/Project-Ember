using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Pop Up Windows")]
    [SerializeField] private GameObject espressoWindow;
    [SerializeField] private GameObject coffeeWindow;
    [SerializeField] private GameObject sinkWindow;

    [Header("Other")]
    [SerializeField] private GameObject gameplaySettings;
    [SerializeField] private GameObject generalUI;
    [SerializeField] private GameObject journal;
    [SerializeField] private GameObject pauseMenu;

    [Header("Orders")]
    [SerializeField] private GameObject orders;
    [SerializeField] private GameObject orderPrefab;

    
    public void AddOrder(string name, string order)
    {
        //Instantiate(orderPrefab, orders.transform);


    }
}
