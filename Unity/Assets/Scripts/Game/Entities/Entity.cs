using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public partial class Entity : MonoBehaviour
{
	public T GetProperty<T>() where T : EntityProperty
	{
		if(TryGetComponent<T>(out T property))
			return property;
		return null;
	}

	[SerializeField]
	private List<EntityProperty> m_properties;
}
