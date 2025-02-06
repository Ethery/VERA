using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTriggerer : EntityProperty
{
	[SerializeField]
	private ClientWave m_wave;

	private void Start()
	{
		Entity.GetProperty<Usable>().OnUse += OnUse;
	}

	private void OnUse(Entity used, bool use)
	{
		if (use)
		{
			m_wave.AllowSpawn();
		}
		else
		{
		}
	}
}
