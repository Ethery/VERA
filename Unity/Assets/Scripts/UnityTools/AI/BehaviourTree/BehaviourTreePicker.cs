using System;
using UnityEngine;

namespace UnityTools.AI.BehaviourTree
{
	[Serializable]
	public class BehaviourTreePicker
	{
		public const string BEHAVIOUR_TREE_FIELDNAME = nameof(m_behaviourTree);
		public BehaviourTree GetBehaviourTree()
		{
			return m_behaviourTree;
		}

		[SerializeField]
		private BehaviourTree m_behaviourTree = null;
	}
}
