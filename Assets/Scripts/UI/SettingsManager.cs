using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject settings;

    [SerializeField] private Button[] businessOptions;
    [SerializeField] private Button[] typeOptions;
    [SerializeField] private Button[] journalOptions;

    [SerializeField] private GlobalEvents events;

    private void Update()
    {
        
    }

    public void Go()
    {
        events.TriggerEvent(events.GameStart); // Trigger Event

        settings.SetActive(false);
    }
}
