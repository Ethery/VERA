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
						new WaitForPlayerInteraction
							(
								new SetBlackBoardValue("PlayerInteraction", true),
								new SetBlackBoardValue("PlayerInteraction", false)
							),
						new CheckBlackBoardValue
							("PlayerInteraction",true,
								new LogTask("checked Interaction success"),
								new LogTask("checked Interaction fail")
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
