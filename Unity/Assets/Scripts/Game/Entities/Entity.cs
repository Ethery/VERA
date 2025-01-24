using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public partial class Entity : MonoBehaviour
{
	public T GetProperty<T>() where T : EntityProperty
	{
		foreach(EntityProperty property in m_properties)
		{
			if(property is T propertyAsT)
				return propertyAsT;
		}
		return null;
	}

	public bool TryGetProperty<T>(out T outProperty) where T : EntityProperty
	{
		foreach(EntityProperty property in m_properties)
		{
			if(property is T propertyAsT)
			{
				outProperty = propertyAsT;
				return true;
			}
		}
		outProperty = null;
		return false;
	}

    public bool GoIdle()
    {
		bool finished = true;
        foreach (EntityProperty property in m_properties)
        {
			finished &= property.Stop();
		}
		return finished;
	}

	public bool MoveTo(Vector3 target)
	{
		if (TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
		{
			agent.SetDestination(target);
			if (agent.stoppingDistance > Vector3.Distance(agent.transform.position, target))
			{
				return true;
			}
		}
		return false;
	}


	[SerializeField]
	private List<EntityProperty> m_properties;
}
