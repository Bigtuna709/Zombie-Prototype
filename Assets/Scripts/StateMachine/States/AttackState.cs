using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private ZombieController _zombie;
    private float _attackReadyTimer;
    public AttackState(ZombieController zombie) : base(zombie.gameObject)
    {
        _zombie = zombie;
    }
    private bool IsZombieReadyToAttack() => Time.time >= _attackReadyTimer;
    public override Type Tick()
    {
        if(_zombie.Target == null)
        {
            return typeof(WanderState);
        }

        var _distance = Vector3.Distance(transform.position, _zombie.Target.position);
        if(_distance > _zombie._navMeshAgent.stoppingDistance)
        {
            return typeof(ChaseState);
        }

        transform.LookAt(_zombie.Target.position);
        if(IsZombieReadyToAttack())
        {
            Debug.Log("Attack");
            _zombie.PerformAttack();
            _attackReadyTimer = Time.time + ZombieSettings.ZombieAttackSpeeed;
        }
        return null;
    }
}
