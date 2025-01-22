using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameGameState : GameState
{
	public override bool CanTransitionTo(State requestedNewState)
	{
		return true;
	}

	public override void OnEnter(State previousState)
	{

	}

	public override void OnExit(State nextState)
	{
	}

	public override void Tick(float deltaTime)
	{
	}
}
