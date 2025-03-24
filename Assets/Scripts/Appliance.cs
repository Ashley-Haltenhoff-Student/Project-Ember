
using UnityEngine;
using UnityEngine.AI;

public class Appliance : MonoBehaviour
{
    [SerializeField] private GameObject window; // gameplay window
    [SerializeField] private Player player;
    [SerializeField] private NavMeshAgent playerAgent;

    [Tooltip("The max distance the player needs to be to open the window")]
    [SerializeField] private float maxDistance;



    private void Start()
    {
        if (window.activeSelf) { window.SetActive(false); }

        playerAgent = player.GetComponent<NavMeshAgent>();
    }

    private void OnMouseDown()
    {
        playerAgent.SetDestination(transform.position);
        Vector3 destination = playerAgent.destination;

        if (window) 
        { 
            player.OpenWindow(this, destination, maxDistance);
        }
    }

    public GameObject ApplianceWindow { get { return window; }}

}
