using System;
using UnityEngine;

namespace UnityTools.AI.BehaviourTree.Tasks
{
	[Serializable]
	public class LogTask : Task
	{
		public override ETaskStatus Tick(Blackboard blackboard)
		{
			Debug.Log($"m_logMessage");
			return ETaskStatus.Success;
		}

		[SerializeField]
		private string m_logMessage;
	}
}
