using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreOpenGameState : GameState
{
	public Kitchen Kitchen => m_kitchen;
	Kitchen m_kitchen;
	public override bool CanTransitionTo(State requestedNewState)
	{
		return true;
	}

	public override void OnEnter(State previousState)
	{
		m_kitchen = GameObject.FindObjectOfType<Kitchen>();
	}

	public override void OnExit(State nextState)
	{
	}

	public override void Tick(float deltaTime)
	{
	}
}
