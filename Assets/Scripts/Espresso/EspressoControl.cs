
using UnityEngine;

public class EspressoControl : MonoBehaviour
{
    [SerializeField] private bool canBrew = false;
    [SerializeField] private bool canGrind = false;
    [SerializeField] private bool portHasGrinds = false;
    

    public bool CanBrew() { return canBrew; }
    public void CanBrew(bool canBrew) { this.canBrew = canBrew; }

    public bool PortHasGrinds() {  return portHasGrinds; }
    public void PortHasGrinds(bool hasGrinds) { this.portHasGrinds = hasGrinds; }

    public bool CanGrind() { return canGrind; }
    public void CanGrind(bool canGrind) { this.canGrind = canGrind; }
}
