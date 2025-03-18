using System.Collections;
using UnityEngine;

public class LockSpot : MonoBehaviour
{
    [SerializeField] protected GameObject window;
    [SerializeField] protected NotifyManager notifyManager;
    protected Animator animator; // Found at startup

    protected bool isWaiting = false;
    protected bool isLocked = false;
    protected bool canRemove = true; // not in works
    protected bool canBeLocked = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void BeginLockableState(GameObject lockableObject)
    {
        if (!isWaiting && canBeLocked) // Ensure there isn't already a coroutine in process
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
                

                // Ensure you're not doing the lock options more than once
                if (!isLocked)
                {
                    IsLockedActions();
                }
                isLocked = true;
            }
            else { isLocked = false; }

            yield return null;
        }

        isWaiting = false;
        animator.SetBool("isLockable", false);
    }

    public virtual void StopWaiting() { isWaiting = false; }

    //public bool IsLocked => isLocked;
    public bool IsLocked() {  return isLocked; }

    public bool CanRemove() { return canRemove; }

    protected virtual void IsLockedActions()
    {
        
    }
}
