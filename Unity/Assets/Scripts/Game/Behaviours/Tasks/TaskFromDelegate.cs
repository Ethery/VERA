using System;
using UnityEngine;
using UnityTools.AI.BehaviourTree;

public class TaskFromDelegate : Task
{
	public delegate ETaskStatus ScriptableTask_delegate(Blackboard blackboard);

    ScriptableTask_delegate m_script;

    public TaskFromDelegate(ScriptableTask_delegate script)
    {
        m_script = script;
    }

    public override ETaskStatus Tick(Blackboard blackboard)
    {
        if (m_script != null)
        {
            return m_script.Invoke(blackboard);
        }
        Debug.LogError($"Scriptable task have no script to run.");
        return ETaskStatus.Failed;

    }

    private string m_targetNameInBlackboard;
}