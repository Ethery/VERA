using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityPlacement : EntityProperty
{
	public void PlaceEntity(Entity entity)
	{
		if(m_entitiesPlaced.Length != m_places.Length)
		{
			m_entitiesPlaced = new Entity[m_places.Length];
		}

		int firstEmptyPlaceIndex = -1;
		bool placed = false;
		for(int i= 0;i<m_entitiesPlaced.Length;i++)
		{
			Entity placedEntity = m_entitiesPlaced[i];
			if(placedEntity == null && firstEmptyPlaceIndex == -1)
			{
				firstEmptyPlaceIndex = i;
				continue;
			}
			if(placedEntity == entity)
			{
				placed = true;
				break;
			}
		}

		if(!placed)
		{
			m_entitiesPlaced[firstEmptyPlaceIndex] = entity;
			entity.transform.position = m_places[firstEmptyPlaceIndex].transform.position;

			NavMeshAgent agent = m_entitiesPlaced[firstEmptyPlaceIndex].GetComponentInChildren<NavMeshAgent>();
			if (agent != null)
			{
				agent.enabled = false;
			}

			NavMeshObstacle obstacle = m_entitiesPlaced[firstEmptyPlaceIndex].GetComponentInChildren<NavMeshObstacle>();
			if (obstacle != null)
			{
				obstacle.enabled = false;
			}
		}
	}

	public void UnplaceEntity(Entity entity)
	{
		if (m_entitiesPlaced.Length != m_places.Length)
		{
			m_entitiesPlaced = new Entity[m_places.Length];
		}

		for (int i = 0;i< m_entitiesPlaced.Length;i++)
		{
			if (m_entitiesPlaced[i] == entity)
			{
				NavMeshAgent agent = m_entitiesPlaced[i].GetComponentInChildren<NavMeshAgent>();
				if (agent != null)
				{
					agent.enabled = true;
				}

				NavMeshObstacle obstacle = m_entitiesPlaced[i].GetComponentInChildren<NavMeshObstacle>();
				if (obstacle != null)
				{
					obstacle.enabled = true;
				}
				m_entitiesPlaced[i] = null;
				break;
			}
		}
	}

	private void Start()
	{
		Entity.GetProperty<Usable>().OnUse += OnUse;
	}

	private void OnUse(Entity used, bool use)
	{
		if (use)
		{
			PlaceEntity(used);
		}
		else
		{
			UnplaceEntity(used);
		}
	}

	private void Update()
	{
		for (int i = 0; i < m_entitiesPlaced.Length; i++)
		{
			if (m_entitiesPlaced[i] != null)
			{
				if (!m_entitiesPlaced[i].gameObject.activeSelf)
				{
					UnplaceEntity(m_entitiesPlaced[i]);
				}
				else
				{
					m_entitiesPlaced[i].transform.position = m_places[i].transform.position;
					m_entitiesPlaced[i].transform.rotation = m_places[i].transform.rotation;
				}
			}
		}
	}

	private void OnDrawGizmos()
	{
		for (int i = 0; i < m_places.Length; i++)
		{
			Vector3 cubeOrigin = m_places[i].transform.position;
			cubeOrigin.y += m_gizmosCubeDimensions.y / 2;
			//Gizmos.color = Color.white;
			//Gizmos.DrawCube(cubeOrigin, m_gizmosCubeDimensions);
			Gizmos.color = Color.blue;
			Gizmos.DrawWireCube(cubeOrigin, m_gizmosCubeDimensions);
		}
	}

	public Vector3 m_gizmosCubeDimensions = new Vector3(1, 2, 1);

	public Entity[] m_entitiesPlaced;
	public GameObject[] m_places;
}
