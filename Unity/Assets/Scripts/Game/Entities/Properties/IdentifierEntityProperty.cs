using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.SceneManagement;
using UnityEngine;

public class IdentifierEntityProperty : EntityProperty
{
	public static Entity FindEntityWithIdentifier(string identifier)
	{
		foreach(Entity entity in GameObject.FindObjectsByType<Entity>(FindObjectsSortMode.None))
		{
			if(entity.TryGetProperty<IdentifierEntityProperty>(out IdentifierEntityProperty identifierProperty))
			{
				if(identifierProperty.m_identifier == identifier)
					return entity;
			}
		}
		return null;
	}

	public static List<Entity> FindEntitiesWithIdentifier(string identifier)
	{
		List<Entity> entities = new List<Entity>();
		foreach(Entity entity in GameObject.FindObjectsByType<Entity>(FindObjectsSortMode.None))
		{
			if(entity.TryGetProperty<IdentifierEntityProperty>(out IdentifierEntityProperty identifierProperty))
			{
				if(identifierProperty.m_identifier == identifier)
					entities.Add(entity);
			}
		}
		return entities;
	}

	[SerializeField]
	private string m_identifier;
}

