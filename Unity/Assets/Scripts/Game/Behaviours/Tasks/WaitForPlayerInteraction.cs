using UnityEngine;
using UnityTools.AI.BehaviourTree;
using UnityTools.AI.BehaviourTree.Tasks;

public class WaitForPlayerInteraction : IfElseCondition
{
    public WaitForPlayerInteraction(bool isToggle, Task trueTask, Task falseTask) : base(trueTask, falseTask)
    {
		m_isToggle = isToggle;
    }

    protected override bool CheckCondition(Blackboard blackboard)
	{
		Entity thisEntity = blackboard.GetValue<Entity>(AIEntityProperty.THIS_BLACKBOARD_IDENTIFIER);
		bool isUsed = thisEntity.GetProperty<Usable>().IsUsed;
		if(m_isToggle)
		{
			if (isUsed)
				m_currentToggleValue = !m_currentToggleValue;
			isUsed = m_currentToggleValue;
        }
		Debug.Log($"{nameof(WaitForPlayerInteraction)} is used = {isUsed}");
		return isUsed;
	}

	private bool m_isToggle;
	private bool m_currentToggleValue;
}
