using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityTools.AI.BehaviourTree;

namespace UnityTools.AI.BehaviourTree.Tasks
{
	[Serializable]
	public class Selector : Task
	{
		public List<Task> Subtasks = new List<Task>();

		public sealed override ETaskStatus Tick(Blackboard blackboard)
		{
			for(int i = 0; i < Subtasks.Count; ++i)
			{
				ETaskStatus subTaskStatus = Subtasks[i].Tick(blackboard);
				if(subTaskStatus == ETaskStatus.Running)
				{
					return ETaskStatus.Running;
				}
				else if(subTaskStatus == ETaskStatus.Success)
				{
					return ETaskStatus.Success;
				}
			}
			return ETaskStatus.Failed;
		}
	}
}
