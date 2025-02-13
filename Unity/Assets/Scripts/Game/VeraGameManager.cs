using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Game;

public class VeraGameManager : GameManager
{
	private void Awake()
	{
		base.Awake();

		GameStates = new StateMachine.StateMachine<GameState>(new InGameGameState());
	}
}
