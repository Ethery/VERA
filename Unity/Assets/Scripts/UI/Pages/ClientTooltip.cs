using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityTools.Systems.Collections;
using UnityTools.Systems.UI;

public class ClientTooltip : EntityTooltip, ISerializationCallbackReceiver
{
	public Image StatusIcon;

	public SerializableDictionary<ClientBehaviourEntityProperty.ClientState, Sprite> m_statusesIcons;

	public override void Update()
	{
		base.Update();
		StatusIcon.sprite = m_statusesIcons[Entity.GetProperty<ClientBehaviourEntityProperty>().CurrentState];
	}
	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		foreach (ClientBehaviourEntityProperty.ClientState type in Enum.GetValues(typeof(ClientBehaviourEntityProperty.ClientState)))
		{
			if (!m_statusesIcons.ContainsKey(type))
			{
				m_statusesIcons.Add(type, null);
			}
		}
	}
}
