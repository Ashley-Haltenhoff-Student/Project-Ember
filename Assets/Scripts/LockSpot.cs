using System.Collections;
using UnityEngine;

public class LockSpot : MonoBehaviour
{
    [SerializeField] protected GameObject window;
    [SerializeField] protected Animator animator;

    protected bool isWaiting = false;
    protected bool isLocked = false;
    protected bool canRemove = true; // not in work

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void BeginLockableState(GameObject lockableObject)
    {
        if (!isWaiting) // Ensure there isn't already a coroutine in process
        {
            StartCoroutine(WaitForLock(lockableObject));
        }
    }

    protected virtual IEnumerator WaitForLock(GameObject lockableObject)
    {
        isWaiting = true;
        animator.SetBool("isLockable", true);

        // While the window is open and the object is being dragged
        while (window.activeSelf && isWaiting)
        {

            // If near enough, object will lock into place
            if (Vector2.Distance(lockableObject.transform.position, transform.position) <= 0.5f)
            {
                lockableObject.transform.position = transform.position;
                isLocked = true;
                IsLockedActions();
            }
            else { isLocked = false; }

            yield return null;
        }

        isWaiting = false;
        animator.SetBool("isLockable", false);
    }

    public virtual void StopWaiting() { isWaiting = false; }

    public bool IsLocked() {  return isLocked; }

    public bool CanRemove() { return canRemove; }

    protected virtual void IsLockedActions()
    {

    }
}
