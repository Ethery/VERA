using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.AI.BehaviourTree.Tasks
{
	[Serializable]
	public class Sequence : Task
	{
		public sealed override ETaskStatus Tick(Blackboard blackboard)
		{
			for (int i = 0; i < SubTasks.Count; ++i)
			{
				ETaskStatus subTaskStatus = SubTasks[i].Tick(blackboard);

				if (!m_taskStatuses.ContainsKey(SubTasks[i]))
				{
					m_taskStatuses.Add(SubTasks[i], subTaskStatus);
				}
				else
				{
					m_taskStatuses[SubTasks[i]] = subTaskStatus;
				}
				if (subTaskStatus == ETaskStatus.Running)
				{
					return ETaskStatus.Running;
				}
				else if (subTaskStatus == ETaskStatus.Failed)
				{
					return ETaskStatus.Failed;
				}
			}
			return ETaskStatus.Success;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			UnityEditor.EditorGUI.indentLevel++;
			for (int i = 0; i < SubTasks.Count; ++i)
			{
				UnityEditor.EditorGUILayout.BeginHorizontal();
				if (m_taskStatuses.ContainsKey(SubTasks[i]))
				{
					UnityEditor.EditorGUILayout.LabelField(m_taskStatuses[SubTasks[i]].ToString());
				}
				else
				{
					UnityEditor.EditorGUILayout.LabelField($"notProcessed");
				}

				SubTasks[i].OnInspectorGUI();
				UnityEditor.EditorGUILayout.EndHorizontal();
			}
			UnityEditor.EditorGUI.indentLevel--;
		}
		[NonSerialized]
		private Dictionary<Task, ETaskStatus> m_taskStatuses = new Dictionary<Task, ETaskStatus>();
		[SerializeField]
		public List<Task> SubTasks = new List<Task>();
	}
}
