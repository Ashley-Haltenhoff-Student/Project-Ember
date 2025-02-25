using UnityEngine;
using UnityEngine.AI;

public class Appliance : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnMouseDown()
    {
        player.GetComponent<NavMeshAgent>().SetDestination(transform.position);
    }
}
