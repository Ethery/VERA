using UnityTools.AI.BehaviourTree;
using UnityTools.AI.BehaviourTree.Tasks;

public class WaitForPlayerInteraction : IfElseCondition
{
    public WaitForPlayerInteraction(Task trueTask, Task falseTask) : base(trueTask, falseTask)
    {
    }

    protected override bool CheckCondition(Blackboard blackboard)
	{
		Entity thisEntity = blackboard.GetValue<Entity>(AiEntityProperty.THIS_BLACKBOARD_IDENTIFIER);
		bool isUsed = thisEntity.GetProperty<Usable>().IsUsed;
		return isUsed;
	}

}
