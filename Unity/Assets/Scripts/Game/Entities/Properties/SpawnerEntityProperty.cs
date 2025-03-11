using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEntityProperty : EntityProperty
{
	[SerializeField]
	private GameObject m_objectToSpawn;
	[SerializeField]
	private Transform m_positionToSpawn;
	
	public void Spawn()
	{
		Instantiate(m_objectToSpawn,m_positionToSpawn.position,m_positionToSpawn.rotation);
	}
}
