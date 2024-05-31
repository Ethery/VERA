#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public partial class Entity
{
	public void GatherProperties()
	{
		if(m_properties == null)
			m_properties = new List<EntityProperty>();
		GetComponentsInChildren(m_properties);
		foreach(EntityProperty property in m_properties)
		{
			property.Entity = this;
		}
	}

	public void ResetProperties()
	{
		if(m_properties != null)
		{
			foreach(EntityProperty property in m_properties)
			{
				if(property != null)
				{
					property.Entity = null;
				}
			}
		}
	}

	[InitializeOnLoad]
	protected static class EntityEditorEventsReader
	{
		static EntityEditorEventsReader()
		{
			ObjectChangeEvents.changesPublished += ObjectChangeEventsHandler;
		}

		public static void ObjectChangeEventsHandler(ref ObjectChangeEventStream stream)
		{
			for(int i = 0; i < stream.length; i++)
			{
				Entity entity = null;
				switch(stream.GetEventType(i))
				{
					case ObjectChangeKind.ChangeGameObjectStructure:
					{
						stream.GetChangeGameObjectStructureEvent(i, out ChangeGameObjectStructureEventArgs data);

						GameObject entityObjectChanged = EditorUtility.InstanceIDToObject(data.instanceId) as GameObject;

						entity = entityObjectChanged.GetComponentInParent<Entity>();

						break;
					}
					case ObjectChangeKind.ChangeGameObjectParent:
					{
						stream.GetChangeGameObjectParentEvent(i, out ChangeGameObjectParentEventArgs data);

						GameObject entityObjectChanged = EditorUtility.InstanceIDToObject(data.instanceId) as GameObject;

						entity = entityObjectChanged.GetComponentInParent<Entity>();

						break;
					}
					case ObjectChangeKind.ChangeGameObjectStructureHierarchy:
					{
						stream.GetChangeGameObjectStructureHierarchyEvent(i, out ChangeGameObjectStructureHierarchyEventArgs data);

						GameObject entityObjectChanged = EditorUtility.InstanceIDToObject(data.instanceId) as GameObject;

						entity = entityObjectChanged.GetComponentInParent<Entity>();

						break;
					}
				}
				GatherPropertiesOnEntity(entity);
			}
		}

		public static void GatherPropertiesOnEntity(Entity entity)
		{
			if(entity != null)
			{
				entity.ResetProperties();
				entity.GatherProperties();
			}
		}
	}
}
#endif