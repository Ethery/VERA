using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

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

	[SerializeField]
	private List<EntityProperty> m_properties;
}
