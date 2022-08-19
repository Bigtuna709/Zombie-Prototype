using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> _availableStates;

    public BaseState CurrentState { get; private set; }
    public event Action<BaseState> OnStateChange;

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        _availableStates = states;
    }
    private void Update()
    {
        if(CurrentState == null)
        {
            CurrentState = _availableStates.Values.First();
        }

        var newState = CurrentState?.Tick();

        if(newState != null && newState != CurrentState.GetType())
        {
            SwitchToNewState(newState);
        }
    }
    private void SwitchToNewState(Type newState)
    {
        CurrentState = _availableStates[newState];
        OnStateChange?.Invoke(CurrentState);
    }
}
