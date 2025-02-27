using UnityEngine;
using UnityEngine.AI;

public class Appliance : MonoBehaviour
{
    [SerializeField] private GameObject window; // gameplay window

    [SerializeField] private Player player;
    public NavMeshAgent playerAgent;

    private void Start()
    {
        if (window.activeSelf) { window.SetActive(false); }

        playerAgent = player.GetComponent<NavMeshAgent>();
    }

    private void OnMouseDown()
    {
        playerAgent.SetDestination(transform.position);
        Vector3 destination = playerAgent.destination;
        
        if (window) { player.OpenWindow(this, destination); }
    }

    public GameObject GetApplianceWindow() { return window; }
}
