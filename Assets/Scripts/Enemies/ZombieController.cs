using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth;
    [SerializeField] int currentHeath;
    public Transform Target { get; private set; }
    public NavMeshAgent _navMeshAgent { get; private set; }
    private StateMachine _stateMachine;
    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        InitializeStateMachine();
    }
    public void SetTarget(Transform target)
    {
        Target = target;
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(WanderState), new WanderState(this) },
            {typeof(ChaseState), new ChaseState(this) }
        };
        GetComponent<StateMachine>().SetStates(states);
    }

    public void IsDamaged(int damage)
    {
        currentHeath -= damage;
        if (currentHeath <= 0)
        {
            Destroy(gameObject);
        }
    }
}
