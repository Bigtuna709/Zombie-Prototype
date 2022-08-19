using UnityEngine;
using System;

public class WanderState : BaseState
{
    private float idleTime = 1f;
    private Vector3? _destination;
    private ZombieController _zombie;
    public WanderState(ZombieController zombie) : base(zombie.gameObject)
    {
        _zombie = zombie;
    }
    private bool IsZombieIdle() => Time.time >= idleTime;
    public override Type Tick()
    {
        var chasePlayer = CheckForAggro();
        if (chasePlayer != null)
        {
            _zombie.SetTarget(chasePlayer);
            return typeof(ChaseState);
        }

        if (_destination.HasValue == false || Vector3.Distance(transform.position, _destination.Value)
            <= _zombie._navMeshAgent.stoppingDistance)
        {
            if (IsZombieIdle())
            {
                FindRandomDestination();
            }
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
        for(var i = 0; i < ZombieSettings.ZombieAggroRadius; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, ZombieSettings.ZombieAggroDistance))
            {
                var player = hit.collider.GetComponent<PlayerController>();
                if(player != null)
                {
                    Debug.DrawRay(pos, direction * ZombieSettings.ZombieAggroDistance, Color.red);
                    return player.transform;
                }
            }
            else
                Debug.DrawRay(pos, direction * ZombieSettings.ZombieAggroDistance, Color.blue);

            direction = angleStep * direction;
        }
        return null;
    }

    private void FindRandomDestination()
    {
        RaycastHit hit;
        var pos = transform.position;

        Debug.DrawRay(pos, transform.forward * 5f, Color.blue);
        if (Physics.Raycast(pos, transform.forward, out hit, 5f))
        {
            Debug.Log("Wall");
            if (hit.collider.CompareTag("Wall"))
            {
                GetRandomPosition(-transform.forward);
            }
            else
            {
                GetRandomPosition(transform.forward);
            }
        }
        else
        {
            GetRandomPosition(transform.forward);
        }
    }

    void GetRandomPosition(Vector3 newLocationDirection)
    {
        Vector3 newPosition = (transform.position + (newLocationDirection * 2f))
            + new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0f, UnityEngine.Random.Range(-1.5f, 1.5f));
        _destination = new Vector3(newPosition.x, 1f, newPosition.z);
        _zombie._navMeshAgent.SetDestination(_destination.Value);
        idleTime = Time.time + UnityEngine.Random.Range(0.5f, 2f);
    }
}
