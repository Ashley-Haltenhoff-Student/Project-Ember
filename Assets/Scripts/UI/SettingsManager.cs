using NUnit.Framework.Constraints;
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
    [SerializeField] private CustomerTypeOption[] customerOptions;
    [SerializeField] private GameObject journalCheckMark;

    [SerializeField] private GlobalEvents events;

    [Header("Settings")]
    public string customerBusiness = "steady";
    public bool allowJournal = true;
    public int maxCustomers;
    public int spawnRate;
    public bool impatientCustomers = false;
    public bool confusedCustomers = false;
    public bool uncertainCustomers = false;



    // The following functions are called from buttons within the scene

    public void Go()
    {
        // Find which customer types were checked off and set them as true
        foreach (CustomerTypeOption c in customerOptions)
        {
            if (c.CheckMarked)
            {
                switch (c.CustomerType)
                {
                    case "impatient":
                        impatientCustomers = true; break;
                    case "confused":
                        confusedCustomers = true; break;
                    case "uncertain":
                        uncertainCustomers = true; break;
                    default: break;
                }
            }
            
        }

        CustomerBusiness(customerBusiness); // Call it again to ensure one is picked

        settings.SetActive(false); // Hide settings
        events.TriggerEvent(events.GameStart); // Trigger Event
    }

    public void CustomerBusiness(string choice) 
    {
        choice = choice.ToLower();

        // Verify if the given choice is one of the valid choices
        if (choice == "slow" || choice == "steady" || choice == "busy")
        {
            customerBusiness = choice;

            // Update max customers based on the chosen business by the player
            switch (customerBusiness)
            {
                case "slow":
                    maxCustomers = 2;
                    spawnRate = 10;
                    break;
                case "steady":
                    maxCustomers = 3; 
                    spawnRate = 5;
                    break;
                case "busy":
                    maxCustomers = 5;
                    spawnRate = 3;
                    break;
            }
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
