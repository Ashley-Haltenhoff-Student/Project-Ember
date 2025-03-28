
using UnityEngine;
using UnityEngine.AI;

public class Appliance : MonoBehaviour
{
    [SerializeField] private GameObject window; // gameplay window
    [SerializeField] private Player player;
    [SerializeField] private NavMeshAgent playerAgent;

    [Tooltip("The max distance the player needs to be to open the window")]
    [SerializeField] private float maxDistance;
    [SerializeField] private Vector2 hiddenPos = new(-30,0); // position when it's hidden from view



    private void Start()
    {
        window.transform.position = hiddenPos; // hide from sight

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

    public void Hide()
    {
        window.transform.position = hiddenPos;
    }

    public void Show()
    {
        window.transform.position = Vector2.zero;
    }
}
