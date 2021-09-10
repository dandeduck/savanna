using UnityEngine;

public abstract class Lockable : MonoBehaviour
{
    private bool isLocked;

    private void Start()
    {
        isLocked = false;

        OnStart();
    }

    private void Update()
    {
        if (!isLocked)
            OnUnlockedUpdate();
        else
            OnLockedUpdate();
    }

    public virtual void Lock()
    {
        isLocked = true;
    }

    public virtual void UnLock()
    {
        isLocked = false;
    }

    protected virtual void OnStart() {}
    protected virtual void OnUnlockedUpdate() {}
    protected virtual void OnLockedUpdate() {}
}
