using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Navigator : Moveable
{
    private NavMeshAgent agent;
    private IEnumerator currentRoutine;
    private Lockable aiming;
    private Lockable gravity;

    protected override void OnStart()
    {
        base.OnStart();

        agent = GetComponent<NavMeshAgent>();
        aiming = GetComponent<PlayerAiming>();
    }

    public void Navigate(Transform transform)
    {
        Navigate(transform.position);
    }

    public override Vector3 Velocity()
    {
        if (IsNavigating())
            return agent.velocity;
        else
            return base.Velocity();
    }

    public void Navigate(Vector3 destination)
    {
        if (IsNavigating())
            StopCoroutine(currentRoutine);

        currentRoutine = Navigation(destination);
        StartCoroutine(currentRoutine);
    }

    public bool HasReachedDestination()
    {
        return agent.remainingDistance > agent.stoppingDistance;
    }

    private IEnumerator Navigation(Vector3 destination)
    {
        EnableNavigation(destination);

        do
        {
            yield return null;
        } while (HasReachedDestination());

        DisableNavigation();
    }

    private void EnableNavigation(Vector3 destination)
    {
        Lock();
        aiming.Lock();
        agent.isStopped = false;
        agent.SetDestination(destination);
    }

    private void DisableNavigation()
    {
        UnLock();
        aiming.UnLock();
        agent.isStopped = true;
    }

    private bool IsNavigating()
    {
        return currentRoutine != null && !agent.isStopped;
    }
}
