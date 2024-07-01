using UnityEngine;
using UnityTools.AI.BehaviourTree;

public class MoveToTask : Task
{
    public MoveToTask(string targetNameInBlackboard)
    {
        m_targetNameInBlackboard = targetNameInBlackboard;
    }

    public override ETaskStatus Tick(Blackboard blackboard)
    {
        Entity thisEntity = blackboard.GetValue<Entity>(AiEntityProperty.THIS_BLACKBOARD_IDENTIFIER);
        AiEntityProperty aiProperty = thisEntity?.GetComponent<AiEntityProperty>();
        if (aiProperty != null)
        {
            if (aiProperty.MoveTo(blackboard.GetValue<Vector3>(m_targetNameInBlackboard)))
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