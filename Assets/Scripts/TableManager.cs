using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    [SerializeField] private Table[] tables;
    [SerializeField] private List<Table> openTables;
    [SerializeField] private List<Table> busyTables;


    private void Start()
    {
        // Add all tables to list of open tables
        foreach (Table t in tables)
        {
            openTables.Add(t);
        }
    }

    public void TableIsOccupied(Table table)
    {
        foreach (Table t in tables)
        {
            if (t == table)
            {
                t.IsOccupied = true;
                break;
            }
        }

        RefreshTables(); // Refresh lists
    }

    
    private void RefreshTables()
    {
        foreach (Table t in tables)
        {
            if (t.IsOccupied) // if it's occupied
            {
                if (!busyTables.Contains(t)) // Not already in busy tables list
                {
                    busyTables.Add(t);
                }
                if (openTables.Contains(t)) // Is in open tables list
                {
                    openTables.Remove(t);
                }
            }
            else // if it's not occupied
            {
                if (!openTables.Contains(t)) // Not already in open tables list
                {
                    openTables.Add(t);
                }
                if (busyTables.Contains(t)) // Is in busy tables list
                {
                    busyTables.Remove(t);
                }
            }
        }
    }

    public List<Table> OpenTables => openTables;
}
