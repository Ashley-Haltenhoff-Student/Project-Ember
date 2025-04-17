
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;

public class Appliance : MonoBehaviour
{
    [SerializeField] private GameObject window; // gameplay window
    [SerializeField] private Player player;
    [SerializeField] private NavMeshAgent playerAgent;
    [SerializeField] private UIManager UI;
    [SerializeField] private GlobalEvents events;

    [Tooltip("The max distance the player needs to be to open the window")]
    [SerializeField] private float maxDistance;
    [SerializeField] private Vector2 hiddenPos = new(-30,0); // position when it's hidden from view

    private void Start()
    {
        window.transform.position = hiddenPos; // hide from sight

        playerAgent = player.GetComponent<NavMeshAgent>();

        events.GameEnd.AddListener(Hide);
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
        UI.applianceWindowOpen = false; // update UI so it knows if it needs to go to the pause menu
    }

    public void Show()
    {
        window.transform.position = Vector2.zero;
        UI.applianceWindowOpen = true; // update UI so it knows if it needs to go to the pause menu
    }
}
