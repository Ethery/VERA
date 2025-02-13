using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Game;

public class ClientWave : Wave<Entity>
{
	protected override bool CanSpawn()
	{
		if (GameManager.Instance.GameStates.CurrentState is StoreOpenGameState)
		{
			if (LastSpawned == null)
			{
				return true;
			}
			else
			{
				if (LastSpawned.TryGetProperty<ClientBehaviourEntityProperty>(out ClientBehaviourEntityProperty client))
				{
				return client.CurrentState != ClientBehaviourEntityProperty.ClientState.WaitingToBePlaced;
				}
			}
		}
		return false;
	}
}
