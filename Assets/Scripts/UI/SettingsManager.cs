using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Gameobject Connections")]
    [SerializeField] private GameObject settings;
    [SerializeField] private SettingsOption[] businessOptions;
    [SerializeField] private GameObject journalCheckMark;

    [SerializeField] private GlobalEvents events;
    [SerializeField] private AudioManager audioManager;

    [Header("Settings")]
    public string customerBusiness = "steady";
    public bool allowJournal = true;




    // The following functions are called from buttons within the scene

    public void Go()
    {

        settings.SetActive(false);
        //audioManager.ShopOpen(); // Update this AT HOME

        events.TriggerEvent(events.GameStart); // Trigger Event
    }

    public void CustomerBusiness(string choice) 
    {
        choice = choice.ToLower();

        // Verify if the given choice is one of the valid choices
        if (choice == "slow" || choice == "steady" || choice == "busy")
        {
            customerBusiness = choice;

            
        }
        else { Debug.Log("Cannot update the customer business to value " + choice); }
    }


    public void AllowJournal(bool allow) 
    {
        allowJournal = allow;

        if (allowJournal)
        {
            journalCheckMark.transform.localPosition = new Vector2(-186, -25); 
        }
        else
        {
            journalCheckMark.transform.localPosition = new Vector2(-186, -72);
        }
    }

}
