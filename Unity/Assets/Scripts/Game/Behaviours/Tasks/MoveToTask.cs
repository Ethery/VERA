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
        Entity target = blackboard.GetValue<Entity>(m_targetNameInBlackboard);

        bool moveToResult = thisEntity.MoveTo(target.gameObject.transform.position);
            
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

    private string m_targetNameInBlackboard;
}