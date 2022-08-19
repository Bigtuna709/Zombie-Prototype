using UnityEngine;
using System;

public class WanderState : BaseState
{
    private Vector3? _destination;
    private ZombieController _zombie;
    public WanderState(ZombieController zombie) : base(zombie.gameObject)
    {
        _zombie = zombie;
    }
    public override Type Tick()
    {
        var chasePlayer = CheckForAggro();
        if(chasePlayer != null)
        {
            _zombie.SetTarget(chasePlayer);
            return typeof(ChaseState);
        }
        if (_destination.HasValue == false || Vector3.Distance(transform.position, _destination.Value)
            <= _zombie._navMeshAgent.stoppingDistance)
        {
            FindRandomDestination();
        }
        return null;
    }

    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion angleStep = Quaternion.AngleAxis(5, Vector3.up);
    private Transform CheckForAggro()
    {
        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for(var i = 0; i < 24; i++)
        {
            if(Physics.Raycast(pos, direction, out hit, ZombieSettings.ZombieAggroDistance))
            {
                var player = hit.collider.GetComponent<PlayerController>();
                if(player != null)
                {
                    return player.transform;
                }
            }
            direction = angleStep * direction;
        }
        return null;
    }

    private void FindRandomDestination()
    {
        Vector3 newPosition = (transform.position + (transform.forward * 4f))
            + new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f, UnityEngine.Random.Range(-4.5f, 4.5f));

        _destination = new Vector3(newPosition.x, 1f, newPosition.z);
        
        _zombie._navMeshAgent.SetDestination(_destination.Value);
    }
}
