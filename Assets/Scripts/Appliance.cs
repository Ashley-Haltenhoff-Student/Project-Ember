using UnityEngine;
using UnityEngine.AI;

public class Appliance : MonoBehaviour
{
    [SerializeField] private GameObject window; // gameplay window

    [SerializeField] private Player player;
    public NavMeshAgent playerAgent;

    private void Start()
    {
        player.GetComponent<NavMeshAgent>();
    }

    private void OnMouseDown()
    {
        player.GetComponent<NavMeshAgent>().SetDestination(transform.position);
        
        if (window) { player.OpenWindow(gameObject.GetComponent<Appliance>()); }
    }

    public GameObject GetApplianceWindow() { return window; }
}
