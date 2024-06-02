﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityTools.AI.BehaviourTree;

namespace UnityTools.AI.BehaviourTree.Tasks
{
	[Serializable]
	public class Sequence : Task
	{
		public sealed override ETaskStatus Tick(Blackboard blackboard)
		{
			for(int i = 0; i < Subtasks.Count; ++i)
			{
				ETaskStatus subTaskStatus = Subtasks[i].Tick(blackboard);
				if(subTaskStatus == ETaskStatus.Running)
				{
					return ETaskStatus.Running;
				}
				else if(subTaskStatus == ETaskStatus.Failed)
				{
					return ETaskStatus.Failed;
				}
			}
			return ETaskStatus.Success;
		}

		[SerializeField]
		public List<Task> Subtasks = new List<Task>();
	}
}
