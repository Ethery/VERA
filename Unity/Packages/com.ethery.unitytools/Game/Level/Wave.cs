using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityTools.Systems.Collections;

namespace UnityTools.Game
{
	public abstract class Wave<T> : MonoBehaviour where T : MonoBehaviour
	{
		[Serializable]
		public struct SpawnConfig
		{
			public T Prefab;
		}

		[SerializeField]
		List<SpawnConfig> SpawnConfigs;

		private int m_currentSpawnIndex;

		protected abstract bool CanSpawn();

		public void OnEnable()
		{
			m_currentSpawnIndex = 0;
		}

		public void OnDisable()
		{
			m_currentSpawnIndex = 0;
		}

		public void Update()
		{
			if (m_currentSpawnIndex < SpawnConfigs.Count)
			{
				if (CanSpawn())
				{
					Spawn();
				}
			}
			else
			{
				gameObject.SetActive(false);
			}
		}

		private void Spawn()
		{
			SpawnConfig spawnable = SpawnConfigs[m_currentSpawnIndex];
			Debug.Log($"Spawning {spawnable.Prefab.name}");
			Instantiate(spawnable.Prefab, transform.position, transform.rotation, transform);
			m_currentSpawnIndex++;
		}

	}
}