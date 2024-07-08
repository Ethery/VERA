using UnityEngine.InputSystem.Haptics;
using UnityTools.AI.BehaviourTree;
using UnityTools.AI.BehaviourTree.Tasks;

public class ClientBehaviourTree : BehaviourTree
{
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
                        new SetEntityWithIdentifierInBlackBoard("Table", "Table"),
                        new SetEntityWithIdentifierInBlackBoard(IdentifierEntityProperty.Identifiers.PLAYER_BLACKBOARD_IDENTIFIER, IdentifierEntityProperty.Identifiers.PLAYER_BLACKBOARD_IDENTIFIER),
						new WaitForPlayerInteraction
							(
								true
								, new SetBlackBoardValue("PlayerInteraction", true)
								, new SetBlackBoardValue("PlayerInteraction", false)
							),
						new CheckBlackBoardValue
							("PlayerInteraction",true
								, new MoveToTask(IdentifierEntityProperty.Identifiers.PLAYER_BLACKBOARD_IDENTIFIER)
								, new IdleEntityTask(IdentifierEntityProperty.Identifiers.PLAYER_BLACKBOARD_IDENTIFIER)
							)
                    } 
				};

				m_root = rootSequence;
			}
			return m_root;
		}
	}

	private Task m_root = null;
}
