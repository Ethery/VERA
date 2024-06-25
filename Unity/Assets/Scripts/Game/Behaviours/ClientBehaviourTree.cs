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
				rootSequence.SubTasks.Add(new WaitForPlayerInteraction(new LogTask("Interacted true"), new LogTask("Interacted false")));
				rootSequence.SubTasks.Add(new LogTask("Interaction success"));
				m_root = rootSequence;
			}
			return m_root;
		}
	}

	private Task m_root = null;
}
