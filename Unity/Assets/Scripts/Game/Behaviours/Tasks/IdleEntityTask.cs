using UnityEngine;
using UnityTools.AI.BehaviourTree;

public class IdleEntityTask : Task
{
    public IdleEntityTask(string targetNameInBlackboard)
    {
        m_targetNameInBlackboard = targetNameInBlackboard;
    }

    public override ETaskStatus Tick(Blackboard blackboard)
    {
        Entity target = blackboard.GetValue<Entity>(m_targetNameInBlackboard);
        if (target == null)
            return ETaskStatus.Failed;

        if(target.GoIdle())
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