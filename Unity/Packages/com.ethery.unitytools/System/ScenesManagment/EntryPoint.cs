using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
	public List<string> OverridedScenesToLoad = null;

	private void Update()
	{
		if(GameManager.Instance.IsReady)
		{
			if (OverridedScenesToLoad != null && OverridedScenesToLoad.Count>0)
			{
				for(int i = 0;i < OverridedScenesToLoad.Count;i++)
				{
					string scene = OverridedScenesToLoad[i];
					SceneManager.LoadScene(scene, i == 0 ? LoadSceneMode.Single : LoadSceneMode.Additive);
				}
			}
			else
			{
				SceneManager.LoadScene(GameManager.Instance.StartScene);
			}
		}
	}
}
