using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
	private void Update()
	{
		if(GameManager.IsReady)
		{
			SceneManager.LoadScene(GameManager.Instance.StartScene);
		}
	}
}
