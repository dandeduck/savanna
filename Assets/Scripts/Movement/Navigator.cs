using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Navigator : Moveable
{
    private NavMeshAgent agent;
    private IEnumerator currentRoutine;
    private PlayerAiming aiming;
    private Gravity gravity;

    protected override void OnStart()
    {
        agent = GetComponent<NavMeshAgent>();
        aiming = GetComponent<PlayerAiming>();
        gravity = GetComponent<Gravity>();
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

    private IEnumerator Navigation(Vector3 destination)
    {
        Lock();
        aiming.Lock();
        gravity.Lock();
        agent.isStopped = false;
        agent.SetDestination(destination);

        do
        {
            yield return null;
        } while (agent.remainingDistance > agent.stoppingDistance);

        UnLock();
        aiming.UnLock();
        gravity.UnLock();
        agent.isStopped = true;
    }


    private bool IsNavigating()
    {
        return currentRoutine != null && !agent.isStopped;
    }
}
