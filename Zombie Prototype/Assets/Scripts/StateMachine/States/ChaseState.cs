using System;
using UnityEngine.AI;

public class ChaseState : BaseState
{
    private ZombieController _zombie;
    public ChaseState(ZombieController zombie) : base(zombie.gameObject)
    {
        _zombie = zombie;
    }
    public override Type Tick()
    {
        throw new NotImplementedException();
    }

}
