using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Systems.UI;

public class TooltipProperty : EntityProperty
{
	[SerializeField]
	private EPage m_tooltip;
	[SerializeField]
	private Vector3 m_offset;

	private int m_tooltipId;

	private Tooltip Tooltip => UIManager.Instance.Page<Tooltip>(m_tooltipId);

	private void OnEnable()
	{
		m_tooltipId = UIManager.Instance.CreatePage(m_tooltip);
		Tooltip.BindToTransform(Entity.transform);
		Tooltip.ApplyOffset(m_offset);

		if(Tooltip is EntityTooltip entityTooltip)
		{
			entityTooltip.BindEntity(Entity);
		}
	}

	private void OnDisable()
	{
		UIManager.Instance.DestroyPage(m_tooltipId);
	}

}
