using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGameState : MonoBehaviour
{
	public void PushGameState(string gameStateToPush)
	{
		GameState newGameState = null;

		switch (gameStateToPush)
		{
			case nameof(StoreOpenGameState):
				newGameState = new StoreOpenGameState();
				break;
		}

		VeraGameManager.Instance.GameStates.PushState(newGameState);
	}

}
