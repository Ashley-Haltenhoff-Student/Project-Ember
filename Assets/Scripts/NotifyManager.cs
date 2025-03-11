using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NotifyManager : MonoBehaviour
{
    [Tooltip("The distance between each notication vertically")]
    [SerializeField] private float distance;
    [SerializeField] private Vector2 topNotifPos;

    [SerializeField] private List<GameObject> notifications;
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private GameObject notificationParent;


    public void Notify(string message)
    {
        // Instantiate
        GameObject newNotif = Instantiate(notificationPrefab, Vector3.zero, Quaternion.identity, notificationParent.transform);
        notifications.Add(newNotif);
        newNotif.GetComponentInChildren<Text>().text = message;
        Debug.Log(newNotif.transform.localPosition);

        // Move every notifcation down
        UpdatePositions();

        // Lifespan
        StartCoroutine(WaitToDestroy(newNotif));

        Debug.Log("Notifcation: " + message);
    }

    // Called whenever a new notif is introduced
    private void UpdatePositions()
    {

        for (int i = 0; i < notifications.Count; i++)
        {
            if (i > 3)
            {
                DeleteNotification(notifications[i]);
                UpdatePositions();
                return;
            }

            if (i == notifications.Count - 1)
            {
                notifications[i].transform.localPosition = topNotifPos;
            }
            else
            {
            // Calculate the distance vertically by the amount of gameobjects in the list
                float distanceFromTop = distance * (notifications.Count - i - 1);
                float verticalPos = topNotifPos.y - distanceFromTop;

            // move down
                notifications[i].transform.localPosition = new Vector3 
                    (notifications[i].transform.localPosition.x, 
                    verticalPos, 0f);

            }
        }
    }

    private IEnumerator WaitToDestroy(GameObject notification)
    {
        yield return new WaitForSeconds(4);

        if (notification)
        {
            DeleteNotification(notification);
        }
    }


    private void DeleteNotification(GameObject notification)
    {
        notifications.Remove(notification);
        Destroy(notification);
    }
}
