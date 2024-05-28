using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public partial class Entity
{
	public void GatherProperties()
	{
		GetComponentsInChildren(m_properties);
		foreach(EntityProperty property in m_properties)
		{
			property.Entity = this;
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
				switch(stream.GetEventType(i))
				{
					case ObjectChangeKind.ChangeGameObjectOrComponentProperties:
						stream.GetChangeGameObjectOrComponentPropertiesEvent(i, out ChangeGameObjectOrComponentPropertiesEventArgs data);

						Entity entity = null;
						//EntityProperty changed
						EntityProperty entityProperty = EditorUtility.InstanceIDToObject(data.instanceId) as EntityProperty;
						if(entityProperty != null)
						{
							entity = entityProperty.GetComponentInParent<Entity>();
						}
						else
						{
							entity = EditorUtility.InstanceIDToObject(data.instanceId) as Entity;
						}
						GatherPropertiesOnEntity(entity);
						break;
				}
			}
		}

		public static void GatherPropertiesOnEntity(Entity entity)
		{
			if(entity != null)
			{
				entity.GatherProperties();
			}
		}
	}
}
