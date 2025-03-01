using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Game;

public class ClientWave : Wave<Entity>
{
	[SerializeField]
	private float m_timeBeforeClientSpawn;

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
					m_timeElapsedSinceLastClientIsGone += Time.deltaTime;
					if(CheckTimeBeforeSpawn())
					{ 
						return client.CurrentState != ClientBehaviourEntityProperty.ClientState.WaitingToBePlaced;
					}
				}
			}
		}
		return false;
	}

	private bool CheckTimeBeforeSpawn()
	{
		if (m_timeElapsedSinceLastClientIsGone > m_timeBeforeClientSpawn)
		{
			m_timeElapsedSinceLastClientIsGone = 0;
			return true;
		}
		return false;
	}

	private float m_timeElapsedSinceLastClientIsGone = 0;
}
