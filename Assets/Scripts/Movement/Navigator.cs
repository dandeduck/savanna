using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Navigator : Moveable
{
    private NavMeshAgent agent;
    private IEnumerator currentRoutine;

    protected override void OnStart()
    {
        agent = GetComponent<NavMeshAgent>();
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
        agent.isStopped = false;

        do
        {
            agent.SetDestination(destination);

            yield return null;
        } while (agent.remainingDistance > agent.stoppingDistance);

        agent.isStopped = true;
        UnLock();
    }


    private bool IsNavigating()
    {
        return currentRoutine != null && !agent.isStopped;
    }
}
