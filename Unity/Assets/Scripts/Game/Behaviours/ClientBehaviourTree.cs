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
				Sequence rootSequence = new Sequence();

				//Find table
				rootSequence.SubTasks.Add(new SetEntityWithIdentifierInBlackBoard("Table", "Table"));



				rootSequence.SubTasks.Add(new WaitForPlayerInteraction(new SetBlackBoardValue("PlayerInteraction",true), new SetBlackBoardValue("PlayerInteraction", false)));
				rootSequence.SubTasks.Add(new CheckBlackBoardValue("PlayerInteraction",true, new LogTask("checked Interaction success"), new LogTask("checked Interaction fail")));

				m_root = rootSequence;
			}
			return m_root;
		}
	}

	private Task m_root = null;
}
