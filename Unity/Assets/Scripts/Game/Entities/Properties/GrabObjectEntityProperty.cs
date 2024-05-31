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
		m_availableGrabbablesSqrRanges = new Dictionary<Grabbable, float>();
		InputManager.RegisterInput(m_grabObjectInput, new InputManager.InputEvent(OnGrabObject_Performed, InputActionPhase.Performed), true);
		InputManager.RegisterInput(m_useObjectInput, new InputManager.InputEvent(OnUseObject_Performed, InputActionPhase.Performed), true);
	}

	private void OnDestroy()
	{
		InputManager.RegisterInput(m_grabObjectInput, new InputManager.InputEvent(OnGrabObject_Performed, InputActionPhase.Performed), false);
		InputManager.RegisterInput(m_useObjectInput, new InputManager.InputEvent(OnUseObject_Performed, InputActionPhase.Performed), false);
	}

	private void OnUseObject_Performed(InputAction action)
	{
		if(m_grabbedObject != null)
		{
			if(m_grabbedObject.Entity.TryGetProperty(out Usable usable))
			{
				usable.Use();
			}
		}
	}

	private void OnGrabObject_Performed(InputAction obj)
	{
		if(m_grabbedObject == null)
		{
			Grabbable closestGrabbable = null;
			float minDistance = float.MaxValue;
			foreach(KeyValuePair<Grabbable, float> kvp in m_availableGrabbablesSqrRanges)
			{
				if(kvp.Value < minDistance)
				{
					closestGrabbable = kvp.Key;
					minDistance = kvp.Value;
				}
			}
			if(closestGrabbable != null)
			{
				m_grabbedObject = closestGrabbable;
				m_grabbedObject.Grab(m_grabbedObjectAnchor);
			}
		}
		else
		{
			m_grabbedObject.Release();
			m_grabbedObject = null;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent(out Grabbable grabbable))
		{
			m_availableGrabbablesSqrRanges.Add(grabbable, (grabbable.Entity.transform.position - Entity.transform.position).sqrMagnitude);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.TryGetComponent(out Grabbable grabbable))
		{
			if(m_availableGrabbablesSqrRanges.ContainsKey(grabbable))
			{
				m_availableGrabbablesSqrRanges[grabbable] = (grabbable.Entity.transform.position - Entity.transform.position).sqrMagnitude;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.TryGetComponent(out Grabbable grabbable))
		{
			m_availableGrabbablesSqrRanges.Remove(grabbable);
		}
	}

	//distances of all available Grabbable entities and their squared distance (for comparison, don't need correct distance).
	private Dictionary<Grabbable, float> m_availableGrabbablesSqrRanges = new Dictionary<Grabbable, float>();

	private Grabbable m_grabbedObject;

	[SerializeField]
	private Transform m_grabbedObjectAnchor;
	[SerializeField]
	private InputManager.Input m_grabObjectInput;
	[SerializeField]
	private InputManager.Input m_useObjectInput;
}
