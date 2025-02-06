using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Game;

public class ClientWave : Wave<Entity>
{
	bool canSpawn = false;
	
	public void AllowSpawn()
	{
		canSpawn = true;
	}

	protected override bool CanSpawn()
	{
		if(canSpawn)
		{
			canSpawn = false;
			return true;
		}
		return false;
	}
}
