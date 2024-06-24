using UnityEngine;
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

	[SerializeField]
	protected BehaviourTreePicker m_behaviourTree = new BehaviourTreePicker();

	[SerializeField]
	private Blackboard m_blackboard;
}
