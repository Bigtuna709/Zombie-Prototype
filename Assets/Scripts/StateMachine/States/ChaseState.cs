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
        if (_zombie._navMeshAgent.speed != ZombieSettings.ZombieRunSpeed)
        {
            _zombie._navMeshAgent.speed = ZombieSettings.ZombieRunSpeed;
        }

        if (_zombie.Target == null)
        {
            return typeof(WanderState);
        }
        transform.LookAt(_zombie.Target);
        _zombie._navMeshAgent.SetDestination(_zombie.Target.position);

        var _distance = Vector3.Distance(transform.position, _zombie.Target.position);
        if(_distance <= _zombie._navMeshAgent.stoppingDistance)
        {
            return typeof(AttackState);
        }
        return null;
    }

}
