using System;

namespace UnityTools.AI.BehaviourTree.Tasks
{
	[Serializable]
	public abstract class IfElseCondition : Task
	{
		public IfElseCondition(Task trueTask, Task falseTask)
		{
			TrueTask = trueTask;
			FalseTask = falseTask;
		}

		protected abstract bool CheckCondition(Blackboard blackboard);

		public sealed override ETaskStatus Tick(Blackboard blackboard)
		{
			Task taskToRun = null;
			m_lastValue = CheckCondition(blackboard);

			if (m_lastValue)
			{
				taskToRun = TrueTask;
			}
			else
			{
				taskToRun = FalseTask;
			}
			if (taskToRun != null)
			{
				return taskToRun.Tick(blackboard);
			}
			return ETaskStatus.Success;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			UnityEditor.EditorGUILayout.BeginVertical();
			UnityEditor.EditorGUILayout.LabelField($"LastValue = {m_lastValue}");
			UnityEditor.EditorGUILayout.LabelField("True");

			UnityEditor.EditorGUI.BeginDisabledGroup(m_lastValue == false);
			UnityEditor.EditorGUI.indentLevel++;
			{
				TrueTask.OnInspectorGUI();
			}
			UnityEditor.EditorGUI.indentLevel--;
			UnityEditor.EditorGUI.EndDisabledGroup();

			UnityEditor.EditorGUILayout.LabelField("False");

			UnityEditor.EditorGUI.BeginDisabledGroup(m_lastValue == true);
			UnityEditor.EditorGUI.indentLevel++;
			{
				FalseTask.OnInspectorGUI();
			}
			UnityEditor.EditorGUI.indentLevel--;
			UnityEditor.EditorGUI.EndDisabledGroup();
			UnityEditor.EditorGUILayout.EndVertical();
		}

		private bool m_lastValue;

		public delegate bool ConditionToCheckEvaluator(Blackboard blackboard);
		public ConditionToCheckEvaluator ConditionFunction;
		public Task TrueTask = null;
		public Task FalseTask = null;
	}
}
