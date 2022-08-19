using System;
using UnityEngine;

public class ChaseState : BaseState
{
    private ZombieController _zombie;
    public ChaseState(ZombieController zombie) : base(zombie.gameObject)
    {
        _zombie = zombie;
    }
    public override Type Tick()
    {
        Debug.Log("Chasing Player");
        if (_zombie.Target == null)
        {
            return typeof(WanderState);
        }
        transform.LookAt(_zombie.Target);
        _zombie._navMeshAgent.SetDestination(_zombie.Target.position);
        return null;
    }

}
