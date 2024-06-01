using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AiEntityProperty : EntityProperty
{
	private void Update()
	{
		MoveTo(m_debugTransform.position);
	}

	public void MoveTo(Vector3 targetPosition)
	{
		GetComponent<NavMeshAgent>().SetDestination(targetPosition);
	}

	[SerializeField]
	private Transform m_debugTransform;

	private Vector3 m_targetPosition;
}
