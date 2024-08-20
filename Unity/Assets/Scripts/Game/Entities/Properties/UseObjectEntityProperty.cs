using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityTools.Systems.Inputs;

[RequireComponent(typeof(Collider))]
public class UseObjectEntityProperty : EntityProperty
{
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
		Usable closestUsable = null;
		float minDistance = float.MaxValue;
		foreach (KeyValuePair<Usable, float> kvp in m_availableUsablesSqrRanges)
		{
			if (kvp.Value < minDistance)
			{
				closestUsable = kvp.Key;
				minDistance = kvp.Value;
			}
		}
		if (closestUsable != null)
		{
			Debug.Log($"Using {closestUsable.Entity} from {Entity}");
			closestUsable.Use(Entity);
			m_usedObject = closestUsable;
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
		if (other.TryGetComponent(out Usable Usable))
		{
			m_availableUsablesSqrRanges.Add(Usable, (Usable.Entity.transform.position - Entity.transform.position).sqrMagnitude);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.TryGetComponent(out Usable Usable))
		{
			if (m_availableUsablesSqrRanges.ContainsKey(Usable))
			{
				m_availableUsablesSqrRanges[Usable] = (Usable.Entity.transform.position - Entity.transform.position).sqrMagnitude;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent(out Usable Usable))
		{
			m_availableUsablesSqrRanges.Remove(Usable);
		}
	}

	//distances of all available Usebable entities and their squared distance (for comparison, don't need correct distance).
	private Dictionary<Usable, float> m_availableUsablesSqrRanges = new Dictionary<Usable, float>();

	[SerializeField]
	private Usable m_usedObject;
	[SerializeField]
	private InputManager.Input m_useObjectInput;
}
