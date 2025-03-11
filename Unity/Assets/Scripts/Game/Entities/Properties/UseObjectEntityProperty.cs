using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityTools.Systems.Inputs;

[RequireComponent(typeof(Collider))]
public class UseObjectEntityProperty : EntityProperty
{
	public bool CanUse(Entity entity, out Usable usable)
	{
		if (entity.TryGetProperty(out usable))
		{
			if (m_closestUsable != null)
				return m_closestUsable.Entity == entity;
		}
		return false;
	}

	public bool Use(Entity entity)
	{
		if (CanUse(entity, out Usable usable))
		{
			Debug.Log($"Using {entity} from {Entity}");
			usable.Use(Entity);
			m_usedObject = usable;
			return true;
		}
		return false;
	}

	private void Start()
	{
		m_availableUsablesSqrRanges = new Dictionary<Usable, float>();
	}

	private void Awake()
	{
		InputManager.RegisterInput(m_useObjectInput, new InputManager.InputEvent(OnUseObject_Performed, InputActionPhase.Performed), false);
		InputManager.RegisterInput(m_useObjectInput, new InputManager.InputEvent(OnUseObject_Canceled, InputActionPhase.Canceled), false);
	}

	private void OnEnable()
	{
		InputManager.RegisterInput(m_useObjectInput, new InputManager.InputEvent(OnUseObject_Performed, InputActionPhase.Performed), true);
		InputManager.RegisterInput(m_useObjectInput, new InputManager.InputEvent(OnUseObject_Canceled, InputActionPhase.Canceled), true);
	}

	private void OnDisable()
	{
		InputManager.RegisterInput(m_useObjectInput, new InputManager.InputEvent(OnUseObject_Performed, InputActionPhase.Performed), false);
		InputManager.RegisterInput(m_useObjectInput, new InputManager.InputEvent(OnUseObject_Canceled, InputActionPhase.Canceled), false);
	}

	private void OnDestroy()
	{
		InputManager.RegisterInput(m_useObjectInput, new InputManager.InputEvent(OnUseObject_Performed, InputActionPhase.Performed), false);
		InputManager.RegisterInput(m_useObjectInput, new InputManager.InputEvent(OnUseObject_Canceled, InputActionPhase.Canceled), false);
	}

	private void OnUseObject_Performed(InputAction obj)
	{
		if (m_closestUsable != null)
		{
			Use(m_closestUsable.Entity);
		}
	}

	private void OnUseObject_Canceled(InputAction obj)
	{
		if (m_usedObject != null)
		{
			Debug.Log($"Unusing {m_usedObject.Entity} from {Entity}");
			m_usedObject.UnUse(Entity);
			m_usedObject = null;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out Usable Usable) && !m_availableUsablesSqrRanges.ContainsKey(Usable))
		{
			m_availableUsablesSqrRanges.Add(Usable, (Usable.Entity.transform.position - Entity.transform.position).sqrMagnitude);
			OnAvailableUsableChanged();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.TryGetComponent(out Usable Usable) && m_availableUsablesSqrRanges.ContainsKey(Usable))
		{
			m_availableUsablesSqrRanges[Usable] = (Usable.Entity.transform.position - Entity.transform.position).sqrMagnitude;
			OnAvailableUsableChanged();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent(out Usable Usable) && m_availableUsablesSqrRanges.ContainsKey(Usable))
		{
			m_availableUsablesSqrRanges.Remove(Usable);
			OnAvailableUsableChanged();
		}
	}

	private void OnAvailableUsableChanged()
	{
		Usable oldUsable = m_closestUsable;
		if (FindClosestUsable())
		{
			if (m_closestUsable.Entity.TryGetProperty(out TooltipsProperty closestUsableTooltip))
			{
				closestUsableTooltip.SetAsTarget();
			}
		}
		else
		{
			if (oldUsable != null)
			{
				if (oldUsable.Entity.TryGetProperty(out TooltipsProperty closestUsableTooltip))
				{
					closestUsableTooltip.UnsetAsTarget();
				}
			}
		}
	}

	private bool FindClosestUsable()
	{
		m_closestUsable = null;
		float minDistance = float.MaxValue;
		foreach (KeyValuePair<Usable, float> kvp in m_availableUsablesSqrRanges)
		{
			if (kvp.Value < minDistance)
			{
				m_closestUsable = kvp.Key;
				minDistance = kvp.Value;
			}
		}
		return m_closestUsable != null;
	}

	private void OnDrawGizmosSelected()
	{
		foreach (KeyValuePair<Usable, float> kvp in m_availableUsablesSqrRanges)
		{
			Gizmos.color = FindClosestUsable() == kvp.Key ? Color.green : Color.red;
			Gizmos.DrawCube(kvp.Key.Entity.transform.position, Vector3.one);
			Gizmos.DrawLine(kvp.Key.Entity.transform.position, Entity.transform.position);
		}
	}

	//distances of all available Usebable entities and their squared distance (for comparison, don't need correct distance).
	private Dictionary<Usable, float> m_availableUsablesSqrRanges = new Dictionary<Usable, float>();

	[NonSerialized]
	private Usable m_closestUsable = null;

	[SerializeField]
	private Usable m_usedObject;
	[SerializeField]
	private InputManager.Input m_useObjectInput;
}
