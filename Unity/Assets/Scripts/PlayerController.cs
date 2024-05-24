using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityTools.Systems.Inputs;

public class Character : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		InputManager.RegisterInput(m_moveInput, new InputManager.InputEvent(OnMoveInput_Started, InputActionPhase.Started), true);
		InputManager.RegisterInput(m_moveInput, new InputManager.InputEvent(OnMoveInput_Performed, InputActionPhase.Canceled), true);
	}

	// Update is called once per frame
	void LateUpdate()
	{
		GetComponent<Rigidbody>().velocity = m_moveDirection;
	}

	private void OnDestroy()
	{
		InputManager.RegisterInput(m_moveInput, new InputManager.InputEvent(OnMoveInput_Started, InputActionPhase.Started), false);
		InputManager.RegisterInput(m_moveInput, new InputManager.InputEvent(OnMoveInput_Performed, InputActionPhase.Canceled), false);
	}

	public void OnMoveInput_Started(InputAction inputAction)
	{
		Vector2 inputDirection = inputAction.ReadValue<Vector2>();
		m_moveDirection = m_speed * new Vector3(inputDirection.x, 0, inputDirection.y);
		Debug.Log($"Start {inputDirection}");
	}

	public void OnMoveInput_Performed(InputAction inputAction)
	{
		Debug.Log($"Performed");
		m_moveDirection = Vector3.zero;
	}

	[NonSerialized]
	private Vector3 m_moveDirection;

	[SerializeField]
	private float m_speed = 1f;

	[SerializeField]
	private InputManager.Input m_moveInput;
}
