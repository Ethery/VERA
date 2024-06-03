using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using UnityTools.AI.BehaviourTree;
using UnityTools.AI.BehaviourTree.Tasks;

public class AiEntityProperty : EntityProperty
{
	private void Update()
	{
		m_behaviourTree.Execute(m_blackboard);
	}

	[SerializeField]
	protected BehaviourTree m_behaviourTree = null;

	[SerializeField]
	private Blackboard m_blackboard;
}
