using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityTools.Systems.Inputs;

[RequireComponent(typeof(Collider))]
public class GrabObjectEntityProperty : EntityProperty
{
	private void Start()
	{
		InputManager.RegisterInput(m_grabObjectInput, new InputManager.InputEvent(OnGrabObject_Performed, InputActionPhase.Performed), true);
		InputManager.RegisterInput(m_useObjectInput, new InputManager.InputEvent(OnUseObject_Performed, InputActionPhase.Performed), true);
	}

	private void OnUseObject_Performed(InputAction action)
	{
		throw new NotImplementedException();
	}

	private void OnGrabObject_Performed(InputAction obj)
	{
		throw new NotImplementedException();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent(out Grabbable grabbable))
		{
			m_availableGrabbablesSqrRanges.Add(grabbable.Entity, (grabbable.Entity.transform.position - Entity.transform.position).sqrMagnitude);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.TryGetComponent(out Grabbable grabbable))
		{
			if(m_availableGrabbablesSqrRanges.ContainsKey(grabbable.Entity))
			{
				m_availableGrabbablesSqrRanges[grabbable.Entity] = (grabbable.Entity.transform.position - Entity.transform.position).sqrMagnitude;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.TryGetComponent(out Grabbable grabbable))
		{
			m_availableGrabbablesSqrRanges.Remove(grabbable.Entity);
		}
	}

	//distances of all available Grabbable entities and their squared distance (for comparison, don't need correct distance).
	private Dictionary<Entity, float> m_availableGrabbablesSqrRanges;

	private Entity m_grabbedObject;

	private InputManager.Input m_grabObjectInput;
	private InputManager.Input m_useObjectInput;
}
