using System;
using UnityEngine;
using UnityEngine.AI;
using UnityTools.AI.BehaviourTree;

public class AiEntityProperty : EntityProperty
{
	public const string THIS_BLACKBOARD_IDENTIFIER = "ThisEntity";

	public BehaviourTree BehaviourTree => m_behaviourTree.GetBehaviourTree();

	private void Start()
	{
		m_blackboard = new Blackboard();
		m_blackboard.Values.Add(THIS_BLACKBOARD_IDENTIFIER, Entity);
	}

	private void Update()
	{
		m_behaviourTree.GetBehaviourTree().Execute(m_blackboard);
	}

    public bool MoveTo(Vector3 target)
    {
		if(TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
		{
			agent.SetDestination(target);
			if(agent.stoppingDistance > Vector3.Distance(agent.transform.position,target))
			{
				return true;			
			}
		}
		return false;
    }

    [SerializeField]
    protected BehaviourTreePicker m_behaviourTree = new BehaviourTreePicker();

    [SerializeField]
	private Blackboard m_blackboard;
}
