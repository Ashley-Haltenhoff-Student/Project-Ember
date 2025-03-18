using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void OpenWindow(Appliance appliance, Vector3 applianceDestination, float minimalDistance)
    {
        StartCoroutine(WaitToOpenWindow(appliance, applianceDestination, minimalDistance));
    }

    private IEnumerator WaitToOpenWindow(Appliance appliance, Vector3 applianceDestination, float maxDistance)
    {
        // While the agent is still pursing the specific appliance
        while (applianceDestination == agent.destination)
        {

            // if the player has reached a specific distance from the appliance
            if (Vector2.Distance(transform.position, agent.destination) <= maxDistance)
            {
                appliance.ApplianceWindow.SetActive(true); // open appliance window
                break;
            }
            yield return null;
        }

        // Wait for escape to be pressed to hide the appliance window
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape));
        appliance.ApplianceWindow.SetActive(false);
    }
}
