using UnityTools.AI.BehaviourTree;
using UnityTools.AI.BehaviourTree.Tasks;

public class ClientBehaviourTree : BehaviourTree
{
	public override Task Root
	{
		get
		{
			Sequence rootSequence = new Sequence();

			//Find table
			rootSequence.SubTasks.Add(new SetEntityWithIdentifierInBlackBoard("Table", "Table"));
			rootSequence.SubTasks.Add(new WaitForPlayerInteraction());

			return rootSequence;
		}
	}
}
