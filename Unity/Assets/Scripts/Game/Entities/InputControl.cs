using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityTools.Systems.Inputs;

public class InputControl : EntityProperty
{
	public static float COMPARISON_EPSILON = 0.00001f;

	// Start is called before the first frame update
	private void Start()
	{
		InputManager.RegisterInput(m_moveInput, new InputManager.InputEvent(OnMoveInput_Performed, InputActionPhase.Performed), true);
		InputManager.RegisterInput(m_moveInput, new InputManager.InputEvent(OnMoveInput_Canceled, InputActionPhase.Canceled), true);
		InputManager.RegisterInput(m_sprintInput, new InputManager.InputEvent(OnSprintInput_Performed, InputActionPhase.Performed), true);
		InputManager.RegisterInput(m_sprintInput, new InputManager.InputEvent(OnSprintInput_Canceled, InputActionPhase.Canceled), true);
		Entity.GetComponent<NavMeshAgent>().updateRotation = false;
	}

	private void LateUpdate()
	{
		if (!Entity.GetComponent<NavMeshAgent>().isStopped)
		{
			Entity.GetComponent<NavMeshAgent>().SetDestination(Entity.transform.position + m_moveDirection);
		}
		Entity.GetComponent<NavMeshAgent>().speed = m_sprinting ? m_sprintSpeed : m_speed;
	}

	private void OnDestroy()
	{
		InputManager.RegisterInput(m_moveInput, new InputManager.InputEvent(OnMoveInput_Performed, InputActionPhase.Performed), false);
		InputManager.RegisterInput(m_moveInput, new InputManager.InputEvent(OnMoveInput_Canceled, InputActionPhase.Canceled), false);
	}

	public void OnMoveInput_Performed(InputAction inputAction)
	{
		Vector2 inputDirection = inputAction.ReadValue<Vector2>().normalized;
		m_moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
		Entity.GetComponent<NavMeshAgent>().isStopped = false;
	}

	public void OnMoveInput_Canceled(InputAction inputAction)
	{
		m_moveDirection = Vector3.zero;
		Entity.GetComponent<NavMeshAgent>().isStopped = true;
	}

	public void OnSprintInput_Performed(InputAction inputAction)
	{
		m_sprinting = true;
	}

	public void OnSprintInput_Canceled(InputAction inputAction)
	{
		m_sprinting = false;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(Entity.transform.position, Entity.transform.position + m_moveDirection);
	}

	[NonSerialized]
	private Vector3 m_moveDirection;
	[NonSerialized]
	private bool m_sprinting;

	[SerializeField]
	private float m_speed = 1f;
	[SerializeField]
	private float m_sprintSpeed = 2f;

	[SerializeField]
	private InputManager.Input m_moveInput;
	[SerializeField]
	private InputManager.Input m_sprintInput;
}
