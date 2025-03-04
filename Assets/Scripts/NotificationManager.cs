using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    [Tooltip("The distance between each notication vertically")]
    [SerializeField] private float distance;
    [SerializeField] private Vector3 topNotifPos;

    [SerializeField] private List<GameObject> notifications;
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private GameObject notificationParent;


    public void Notify(string message)
    {
        // Instantiate
        GameObject newNotif = Instantiate(notificationPrefab, topNotifPos,Quaternion.identity, notificationParent.transform);
        newNotif.GetComponentInChildren<Text>().text = message;
        notifications.Add(newNotif);

        // Move every notifcation down
        UpdatePositions();

        // Lifespan
        StartCoroutine(WaitToDestroy(newNotif));

    }

    // Called whenever a new notif is introduced
    private void UpdatePositions()
    {

        for (int i = 0; i < notifications.Count; i++)
        {
            if (i == notifications.Count - 1)
            {
                notifications[i].transform.position = topNotifPos;
            }
            else
            {
                // Calculate the distance vertically by the amount of gameobjects in the list
                float distanceFromTop = distance * (notifications.Count - i);
                Debug.Log(distanceFromTop);
                float verticalPos = topNotifPos.y - distanceFromTop;
                Debug.Log(verticalPos);
                // Math is wrong!

                // move down
                notifications[i].transform.position = new Vector3 
                    (notifications[i].transform.position.x, 
                    verticalPos, 0f);

            }


        }
    }

    private IEnumerator WaitToDestroy(GameObject notification)
    {
        yield return new WaitForSeconds(4);

        DeleteNotifcation(notification);
    }


    private void DeleteNotifcation(GameObject notification)
    {
        notifications.Remove(notification);
        Destroy(notification);
    }
}
