using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<STATE_TYPE> where STATE_TYPE : State
{
    public STATE_TYPE CurrentState => m_currentState;

    public StateMachine(STATE_TYPE defaultState)
    {
		defaultState.OnEnter(null);
        m_currentState = defaultState;
    }

    public bool PushState(STATE_TYPE newState)
    {
        if(m_currentState.CanTransitionTo(newState))
        {
            m_currentState.OnExit(newState);
            newState.OnEnter(m_currentState);
            m_currentState = newState;
            return true;
        }
        return false;
    }

    public void Tick(float deltaTime)
    {
        m_currentState.Tick(deltaTime);
    }

    [SerializeField]
    private STATE_TYPE m_currentState;
}
