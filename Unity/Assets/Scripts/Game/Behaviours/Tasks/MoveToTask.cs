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