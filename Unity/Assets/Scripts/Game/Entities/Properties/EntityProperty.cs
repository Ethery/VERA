using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityProperty : MonoBehaviour
{
	public Entity Entity
	{
		get => m_entity;
		set
		{
			if(m_entity != null && value != null)
			{
				Debug.LogError($"{this} already have a different Entity from {value} ({m_entity})");
			}
			else
			{
				m_entity = value;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns>true if it's stopped, false if it need running in later frames.</returns>
	public virtual bool Stop() { return true; }

	[SerializeField]
	private Entity m_entity;

}
