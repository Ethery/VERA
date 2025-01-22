using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameLevelData : BaseLevelDatas
{
	public override void Reset()
	{
		throw new System.NotImplementedException();
	}

	public override void Save()
	{
		throw new System.NotImplementedException();
	}
}

public class BaseGameLevel : BaseLevel<BaseGameLevelData>
{
}
