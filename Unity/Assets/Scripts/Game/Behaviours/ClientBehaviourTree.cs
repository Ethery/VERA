using System.Linq;
using UnityTools.AI.BehaviourTree;
using UnityTools.AI.BehaviourTree.Tasks;

public class ClientBehaviourTree : BehaviourTree
{
	public const string TABLE_BLACKBOARD_IDENTIFIER = "Table";
	public override Task Root
	{
		get
		{
			if (m_root == null)
			{
				Sequence rootSequence = new Sequence()
				{
					SubTasks = new System.Collections.Generic.List<Task>()
					{
						//Find table
                        SetTableInBlackboard,
						SetPlayerInBlackboard,
						WaitForPlayerInteractionAndSetupBlackboardValue,
						//HandlePlayerInteraction,
						//new ScriptableTask(CheckIfTableIsNearAndSitToIt),
                    }
				};

				m_root = rootSequence;
			}
			return m_root;
		}
	}

	/// <summary>
	/// All the tasks can be created static since the blackboard is supposed to store all the datas.
	/// </summary>
	private static SetEntityWithIdentifierInBlackBoard SetTableInBlackboard = new SetEntityWithIdentifierInBlackBoard("Table", TABLE_BLACKBOARD_IDENTIFIER);
	private static SetEntityWithIdentifierInBlackBoard SetPlayerInBlackboard = new SetEntityWithIdentifierInBlackBoard(IdentifierEntityProperty.Identifiers.PLAYER_BLACKBOARD_IDENTIFIER, IdentifierEntityProperty.Identifiers.PLAYER_BLACKBOARD_IDENTIFIER);
	private static WaitForPlayerInteraction WaitForPlayerInteractionAndSetupBlackboardValue = new WaitForPlayerInteraction
							(
								isToggle: true,
								trueTask: CheckNearTable,
								falseTask: new IdleEntityTask(IdentifierEntityProperty.Identifiers.PLAYER_BLACKBOARD_IDENTIFIER)
							);

	private static DelegableIfElseCondition CheckNearTable = new DelegableIfElseCondition(
		ifElseDelegate: CheckIfTableIsNear,
		trueTask: SitOnTable,
		falseTask: FollowPlayer);


	private static Sequence FollowPlayer = new Sequence()
	{
		SubTasks = new System.Collections.Generic.List<Task>()
		{
			new MoveToTask(IdentifierEntityProperty.Identifiers.PLAYER_BLACKBOARD_IDENTIFIER)
		}
	};

	private static TaskFromDelegate SitOnTable = new TaskFromDelegate(SitOnTable_delegate);
	private static ETaskStatus SitOnTable_delegate(Blackboard blackboard)
	{
		Entity thisEntity = blackboard.GetValue<Entity>(AIEntityProperty.THIS_BLACKBOARD_IDENTIFIER);
		Entity table = blackboard.GetValue<Entity>(TABLE_BLACKBOARD_IDENTIFIER);
		EntityPlacement placement = table.GetProperty<EntityPlacement>();
		placement.PlaceEntity(thisEntity);
		if (placement.m_entitiesPlaced.Contains(thisEntity))
		{
			return ETaskStatus.Success;
		}
		return ETaskStatus.Failed;
	}

	private static bool CheckIfTableIsNear(Blackboard blackboard)
	{
		Entity thisEntity = blackboard.GetValue<Entity>(AIEntityProperty.THIS_BLACKBOARD_IDENTIFIER);
		Entity table = blackboard.GetValue<Entity>(TABLE_BLACKBOARD_IDENTIFIER);

		EntityPlacement placement = table.GetProperty<EntityPlacement>();
		return thisEntity.GetProperty<UseObjectEntityProperty>().CanUse(table, out Usable _);
	}

	private Task m_root = null;
}
