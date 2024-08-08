using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityTools.AI.BehaviourTree;

public class SetEntityWithIdentifierInBlackBoard : Task
{
	public SetEntityWithIdentifierInBlackBoard(string identifier, string nameInBlackboard)
	{
		m_identifier = identifier;
		m_nameInBlackboard = nameInBlackboard;
	}

	public override ETaskStatus Tick(Blackboard blackboard)
	{
		Entity entity = IdentifierEntityProperty.FindEntityWithIdentifier(m_identifier);
		if(entity != null)
		{
			if(!blackboard.Values.ContainsKey(m_nameInBlackboard))
			{
				blackboard.Values.Add(m_nameInBlackboard, entity);
			}
			return ETaskStatus.Success;
		}
		else
		{
			return ETaskStatus.Failed;
		}
	}

	private string m_identifier;
	private string m_nameInBlackboard;
}
