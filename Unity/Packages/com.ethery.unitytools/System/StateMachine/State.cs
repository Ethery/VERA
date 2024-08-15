using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract bool CanTransitionTo(State requestedNewState);
    public abstract void OnEnter(State previousState);
    public abstract void OnExit(State nextState);
    public abstract void Tick(float deltaTime);
}
