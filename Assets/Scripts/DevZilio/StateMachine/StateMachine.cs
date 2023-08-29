using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;


namespace DevZilio.StateMachine
{
    
public class StateMachine<T> where T : System.Enum
{
    //Key
    public Dictionary<T, StateBase> dictionaryState;

    private StateBase _currentState;

    public float timeToStartGame = 1f;

    public StateBase CurrentState
    {
        get
        {
            return _currentState;
        }
    }

    public void Init()
    {
        dictionaryState = new Dictionary<T, StateBase>();
    }

    public void RegisterStates(T typeEnum, StateBase state)
    {
        dictionaryState.Add (typeEnum, state);
    }

    public void SwitchState(T state)
    {
        {
            if (_currentState != null) _currentState.OnStateExit();
            _currentState = dictionaryState[state];
            _currentState.OnStateEnter();
        }
    }

    public void Update()
    {
        if (_currentState != null) _currentState.OnStateStay();
    }
}

}