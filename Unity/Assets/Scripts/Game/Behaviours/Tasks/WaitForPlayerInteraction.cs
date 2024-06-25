using UnityTools.AI.BehaviourTree;
using UnityTools.AI.BehaviourTree.Tasks;

public class WaitForPlayerInteraction : IfElseCondition
{
	public WaitForPlayerInteraction(Task trueTask, Task falseTask) : base(trueTask, falseTask)
	{
		ConditionFunction = IsUsed;
	}

	private bool IsUsed(Blackboard blackboard)
	{
		Entity thisEntity = blackboard.GetValue<Entity>(AiEntityProperty.THIS_BLACKBOARD_IDENTIFIER);
		return thisEntity.GetProperty<Usable>().IsUsed;
	}

}
