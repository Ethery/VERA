using UnityEngine;
using UnityTools.AI.BehaviourTree;
using UnityTools.AI.BehaviourTree.Tasks;

[CreateAssetMenu(fileName = "Client behaviour tree", menuName = "AI/BehaviourTrees/Client")]
public class ClientBehaviourTree : BehaviourTree
{
	public override Task Root
	{
		get
		{
			Sequence root = new Sequence();

			root.Subtasks.Add(new LogTask());

			return root;
		}
	}
}
