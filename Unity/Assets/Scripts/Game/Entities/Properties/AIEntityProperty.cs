using System;
using UnityEngine;
using UnityEngine.AI;
using UnityTools.AI.BehaviourTree;

public class AIEntityProperty : EntityProperty
{
    public const string THIS_BLACKBOARD_IDENTIFIER = "ThisEntity";
    public BehaviourTree BehaviourTree => m_behaviourTree.GetBehaviourTree();

	private void Start()
	{
		m_blackboard = new Blackboard();
		m_blackboard.Values.Add(IdentifierEntityProperty.Identifiers.PLAYER_BLACKBOARD_IDENTIFIER
			, IdentifierEntityProperty.FindEntityWithIdentifier(IdentifierEntityProperty.Identifiers.PLAYER_BLACKBOARD_IDENTIFIER));
		m_blackboard.Values.Add(AIEntityProperty.THIS_BLACKBOARD_IDENTIFIER, Entity);
	}

	private void Update()
	{
		m_behaviourTree.GetBehaviourTree().Execute(m_blackboard);
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
