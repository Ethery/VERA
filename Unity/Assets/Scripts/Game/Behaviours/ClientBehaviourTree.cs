using UnityTools.AI.BehaviourTree;
using UnityTools.AI.BehaviourTree.Tasks;

public class ClientBehaviourTree : BehaviourTree
{
	public override Task Root
	{
		get
		{
			Sequence root = new Sequence();

			root.Subtasks.Add(new LogTask("Trouve table"));
			root.Subtasks.Add(new LogTask("mange repas"));
			root.Subtasks.Add(new LogTask("paye connar"));

			return root;
		}
	}
}

public class Client2BehaviourTree : BehaviourTree
{
	public override Task Root
	{
		get
		{
			Sequence root = new Sequence();

			root.Subtasks.Add(new LogTask("test02"));

			return root;
		}
	}
}
