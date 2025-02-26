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

    public void OpenWindow(Appliance appliance)
    {
        StartCoroutine(WaitToOpenWindow(appliance));
    }

    private IEnumerator WaitToOpenWindow(Appliance appliance)
    {
        while (new Vector3(agent.destination.x, agent.destination.y,0) == appliance.gameObject.transform.position)
        {
            // doesn't work
            if (Vector2.Distance(transform.position, new Vector3(agent.destination.x, agent.destination.y, 0)) <= 0.5f + agent.stoppingDistance)
            {
                appliance.GetApplianceWindow().SetActive(true);
            }
            yield return null;
        }

        appliance.GetApplianceWindow().SetActive(false);
    }
}
