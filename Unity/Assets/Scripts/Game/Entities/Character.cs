using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityTools.Systems.Inputs;

public class Character : Entity
{
	// Start is called before the first frame update
	private void Start()
	{
		InputManager.RegisterInput(m_moveInput, new InputManager.InputEvent(OnMoveInput_Performed, InputActionPhase.Performed), true);
		InputManager.RegisterInput(m_moveInput, new InputManager.InputEvent(OnMoveInput_Canceled, InputActionPhase.Canceled), true);
	}

	private void LateUpdate()
	{
		GetComponent<Rigidbody>().velocity = m_moveDirection;
	}

	private void OnDestroy()
	{
		InputManager.RegisterInput(m_moveInput, new InputManager.InputEvent(OnMoveInput_Performed, InputActionPhase.Performed), false);
		InputManager.RegisterInput(m_moveInput, new InputManager.InputEvent(OnMoveInput_Canceled, InputActionPhase.Canceled), false);
	}

	public void OnMoveInput_Performed(InputAction inputAction)
	{
		Vector2 inputDirection = inputAction.ReadValue<Vector2>();
		m_moveDirection = m_speed * new Vector3(inputDirection.x, 0, inputDirection.y);
	}

	public void OnMoveInput_Canceled(InputAction inputAction)
	{
		m_moveDirection = Vector3.zero;
	}

	[NonSerialized]
	private Vector3 m_moveDirection;

	[SerializeField]
	private float m_speed = 1f;

	[SerializeField]
	private InputManager.Input m_moveInput;
}
