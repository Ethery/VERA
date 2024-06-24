using UnityEngine;
using UnityTools.AI.BehaviourTree;

public class WaitForPlayerInteraction : Task
{
	private enum TaskState
	{
		Begin,
		WaitingForPlayer,
		Finished,
	}

	public override ETaskStatus Tick(Blackboard blackboard)
	{
		switch (m_currentState)
		{
			case TaskState.Begin:
				{
					if (m_thisEntity == null)
					{
						m_thisEntity = blackboard.GetValue<Entity>(AiEntityProperty.THIS_BLACKBOARD_IDENTIFIER);
					}

					if (m_thisEntity != null)
					{
						m_thisEntity.GetProperty<Usable>().OnUse += OnPlayerInteracted;
						m_currentState = TaskState.WaitingForPlayer;
					}
					return ETaskStatus.Running;
				}
			case TaskState.WaitingForPlayer:
				return ETaskStatus.Running;
			case TaskState.Finished:
				return ETaskStatus.Success;
		}
		return ETaskStatus.Failed;
	}

	private void OnPlayerInteracted(Entity user)
	{
		Debug.Log($"{m_thisEntity} has been used by {user}");
		m_currentState = TaskState.Finished;
	}

	private TaskState m_currentState;
	private Entity m_thisEntity;
}
