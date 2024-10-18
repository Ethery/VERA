using System;
using UnityEngine;
using UnityTools.AI.BehaviourTree;

public class ScriptableTask : Task
{
    Action m_script; 
    public ScriptableTask(Action script)
    {
        m_script = script;
    }

    public override ETaskStatus Tick(Blackboard blackboard)
    {
        Entity thisEntity = blackboard.GetValue<Entity>(AIEntityProperty.THIS_BLACKBOARD_IDENTIFIER);
        AIEntityProperty aiProperty = thisEntity?.GetComponent<AIEntityProperty>();
        if (aiProperty != null)
        {
            Entity target = blackboard.GetValue<Entity>(m_targetNameInBlackboard);

            bool moveToResult = aiProperty.MoveTo(target.gameObject.transform.position);
            Debug.Log($"{nameof(MoveToTask)} : {moveToResult}");
            if (moveToResult)
            {
                return ETaskStatus.Success;
            }
            else
            {
                return ETaskStatus.Running;
            }
        }
        else
        {
            return ETaskStatus.Failed;
        }
    }

    private string m_targetNameInBlackboard;
}