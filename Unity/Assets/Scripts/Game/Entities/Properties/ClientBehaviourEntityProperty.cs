using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityTools.AI.BehaviourTree;
using UnityTools.Game;

public class ClientBehaviourEntityProperty : EntityProperty
{
	public ClientState CurrentState => m_currentState;

	public enum ClientState
	{
		WaitingToBePlaced,
		FollowingPlayerToTable,
		ChoosingFood,
		WaitingToOrder,
		Eating,
		WaitingToPay,
		Leave,
	}


	private void Awake()
	{
		m_player = IdentifierEntityProperty.FindEntityWithIdentifier(IdentifierEntityProperty.Identifiers.PLAYER_BLACKBOARD_IDENTIFIER);
		m_table = IdentifierEntityProperty.FindEntityWithIdentifier("Table");
	}

	private void Start()
	{
		if (GameManager.Instance != null)
		{
			GetComponent<Usable>().OnUse += IsUsed;
			m_selectedDishId = UnityEngine.Random.Range(0, (GameManager.Instance.GameRules as GameRules).AvailableDishes.Count);
		}
	}

	private void Update()
	{
		if (m_timeLeft > 0)
		{
			m_timeLeft -= Time.deltaTime;
		}

		switch (m_currentState)
		{
			case ClientState.WaitingToBePlaced: break;
			case ClientState.FollowingPlayerToTable:
				Entity.MoveTo(m_player.transform.position);
				if (Entity.TryGetProperty(out UseObjectEntityProperty user))
				{
					if (user.Use(m_table))
					{
						GoToNextState();
						m_timeLeft = 1.5f; //1.5 second to select it's food.
					}
				}
				break;
			case ClientState.ChoosingFood:
				//TODO anim to select food. (already picked in Start())

				if (m_timeLeft <= 0)
				{
					GoToNextState();
				}
				break;
			case ClientState.WaitingToOrder: break;
			case ClientState.Eating:
				//TODO anim to eat food.
				if (m_timeLeft <= 0)
				{
					GoToNextState();
				}
				break;
			case ClientState.WaitingToPay: break;
			case ClientState.Leave:
				Entity.gameObject.SetActive(false);
				break;
		}
	}

	public void IsUsed(Entity user, bool isUsing)
	{
		if (user == m_player
			&& isUsing)
		{
			switch (m_currentState)
			{
				case ClientState.WaitingToBePlaced:
					GoToNextState();
					break;
				case ClientState.FollowingPlayerToTable: break;
				case ClientState.ChoosingFood: break;
				case ClientState.WaitingToOrder:
					GoToNextState();

					(VeraGameManager.Instance.GameStates.CurrentState as StoreOpenGameState).Kitchen.PlaceOrder(m_selectedDishId);
					m_timeLeft = 20f; //20 seconds to eat.
					break;
				case ClientState.Eating: break;
				case ClientState.WaitingToPay:
					GoToNextState();
					break;
				case ClientState.Leave: break;
			}
		}
	}

	public void GoToNextState()
	{
		m_currentState += 1;
		Debug.Log($"{Entity.name} Is now on State {(ClientState)m_currentState}");
	}

	public override bool Stop()
	{
		if (TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
		{
			agent.isStopped = true;
		}
		return true;
	}

	private void OnDrawGizmos()
	{
		if (m_table != null)
		{
			Gizmos.DrawLine(m_table.transform.position, Entity.transform.position);
		}
	}

	private Dish SelectedDish => (GameManager.Instance.GameRules as GameRules).AvailableDishes[m_selectedDishId];

	private Entity m_table = null;
	private Entity m_player = null;

	private ClientState m_currentState;

	private int m_selectedDishId;

	private float m_timeLeft;
}
