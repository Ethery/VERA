using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace UnityTools.AI.BehaviourTree
{
	[CreateAssetMenu(fileName = "Base behaviour tree", menuName = "AI/BehaviourTrees/Base")]
	[Serializable]
	public abstract class BehaviourTree : ScriptableObject
	{
		public abstract Task Root { get; }

		public Blackboard Blackboard;

		public ETaskStatus Execute()
		{
			if(Root == null)
			{
				Debug.LogError($"{nameof(Root)} is not initialized. Make sure to override {this.GetType()}.{nameof(Root)}.");
				return ETaskStatus.Failed;
			}
			return Root.Tick(Blackboard);
		}
	}
}
