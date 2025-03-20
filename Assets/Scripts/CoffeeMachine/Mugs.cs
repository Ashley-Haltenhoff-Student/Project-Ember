using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Mugs : MonoBehaviour
{
    [SerializeField] private NotifyManager notifyManager;
    [SerializeField] private LockSpot[] mugLockSpots;
    [SerializeField] private GameObject mugPrefab;
    [SerializeField] private Vector2 mugSpawnPoint;

    private GameObject currentMug;
    private bool mugSpawned = false;

    private void OnMouseDown()
    {
        SpawnMug();
    }

    private void SpawnMug()
    {
        if (!mugSpawned)
        {
            GameObject mug = Instantiate(mugPrefab, gameObject.transform.parent);
            mug.transform.localPosition = mugSpawnPoint;
            currentMug = mug;
            mug.GetComponent<Mug>().LockSpots = mugLockSpots; // assign locks spots

            mugSpawned = true;
        }
        else
        {
            notifyManager.Notify("A mug is already available");
        }

    }

    public void RefreshMug()
    {
        Destroy(currentMug); currentMug = null; // Remove mug
        mugSpawned = false; // Allow new mug
    }
}
