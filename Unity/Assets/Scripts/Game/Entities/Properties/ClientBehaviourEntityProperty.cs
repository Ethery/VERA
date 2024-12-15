using System;
using UnityEngine;
using UnityEngine.AI;
using UnityTools.AI.BehaviourTree;

public class ClientBehaviourEntityProperty : EntityProperty
{
	Entity m_table = null;
	Entity m_player = null;

	private void Start()
	{
		m_player = IdentifierEntityProperty.FindEntityWithIdentifier(IdentifierEntityProperty.Identifiers.PLAYER_BLACKBOARD_IDENTIFIER);
		m_table = IdentifierEntityProperty.FindEntityWithIdentifier("Table");
	}

	private void Update()
	{
		
	}

    public override bool Stop()
    {
		if(TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
		{
			agent.isStopped = true;
		}
		return true;
    }

    [SerializeField]
    protected BehaviourTreePicker m_behaviourTree = new BehaviourTreePicker();

    [SerializeField]
	private Blackboard m_blackboard;
}
