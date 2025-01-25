using StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="GameDatas",menuName ="GameDatas")]
public class GameRules : UnityTools.Game.BaseGameRules
{
	public List<Dish> AvailableDishes;

}
