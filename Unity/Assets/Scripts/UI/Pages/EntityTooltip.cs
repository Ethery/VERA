using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityTools.Systems.UI;

public class EntityTooltip : Tooltip
{
	public TextMeshProUGUI EntityName;

	protected Entity Entity;

	public void BindEntity(Entity entity)
	{
		Entity = entity;
	}

	public override void Update()
	{
		base.Update();
		EntityName.text = Entity.name;
	}
}
