using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerTypeOption : SettingsOption
{
    [SerializeField] private GameObject checkMark;

    [SerializeField] private string customerType;
    private bool checkMarked = false;

    public void ToggleCheckMark()
    {
        if (checkMark.activeSelf)
        {
            checkMark.SetActive(false);
            checkMarked = false;
        }
        else
        {
            checkMark.SetActive(true);
            checkMarked = true;
        }
    }

    public bool CheckMarked {  get { return checkMarked; } }
    public string CustomerType { get { return customerType; } }
}
