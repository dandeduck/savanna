using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Navigator : Moveable
{
    private NavMeshAgent agent;
    private IEnumerator currentRoutine;
    private PlayerAiming aiming;

    protected override void OnStart()
    {
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

    private IEnumerator Navigation(Vector3 destination)
    {
        Lock();
        aiming.Lock();
        agent.isStopped = false;
        agent.SetDestination(destination);
        Debug.Log(Velocity());

        do
        {
            yield return null;
        } while (agent.remainingDistance > agent.stoppingDistance);

        UnLock();
        Debug.Log(Velocity());
        Debug.Log(Direction());
        yield return null;
        Debug.Log(Velocity());
        Debug.Log(Direction());
        aiming.UnLock();
        agent.isStopped = true;
    }


    private bool IsNavigating()
    {
        return currentRoutine != null && !agent.isStopped;
    }
}
