using UnityEngine;
using UnityTools.AI.BehaviourTree;

public class AiEntityProperty : EntityProperty
{
	private void Update()
	{
		m_behaviourTree.GetBehaviourTree().Execute(m_blackboard);
	}

	[SerializeField]
	protected BehaviourTreePicker m_behaviourTree = new BehaviourTreePicker();

	[SerializeField]
	private Blackboard m_blackboard;
}
