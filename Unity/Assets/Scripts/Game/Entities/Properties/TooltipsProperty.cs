using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Systems.UI;

public class TooltipsProperty : EntityProperty
{
	private static Entity CurrentPlayerTarget;

	[SerializeField]
	private EPage m_tooltip;
	[SerializeField]
	private Vector3 m_offset;
	[SerializeField]
	private EPage m_isTargetTooltip;

	private Guid m_tooltipId;
	private Guid m_isTargetTooltipId;

	private Tooltip Tooltip => UIManager.Instance.Page<Tooltip>(m_tooltipId);
	private Tooltip IsTargetTooltip => UIManager.Instance.Page<Tooltip>(m_isTargetTooltipId);

	private void OnEnable()
	{
		m_tooltipId = UIManager.Instance.CreatePage(m_tooltip);
		m_isTargetTooltipId = UIManager.Instance.CreatePage(m_isTargetTooltip);
		IsTargetTooltip.BindToTransform(Entity.transform);
		IsTargetTooltip.gameObject.SetActive(false);
		Tooltip.BindToTransform(Entity.transform);
		Tooltip.ApplyOffset(m_offset);

		if (Tooltip is EntityTooltip entityTooltip)
		{
			entityTooltip.BindEntity(Entity);
		}
	}

	private void OnDisable()
	{
		UIManager.Instance.DestroyPage(m_tooltipId);
	}

	public void SetAsTarget()
	{
		if (CurrentPlayerTarget != null && CurrentPlayerTarget.TryGetProperty(out TooltipsProperty currentTargetTooltip))
		{
			currentTargetTooltip.IsTargetTooltip.gameObject.SetActive(false);
		}

		if (IsTargetTooltip != null)
		{
			IsTargetTooltip.gameObject.SetActive(true);
		}

		CurrentPlayerTarget = Entity;
	}

	public void UnsetAsTarget()
	{
		if (CurrentPlayerTarget == Entity && CurrentPlayerTarget.TryGetProperty(out TooltipsProperty currentTargetTooltip))
		{
			currentTargetTooltip.IsTargetTooltip.gameObject.SetActive(false);
		}
	}
}
