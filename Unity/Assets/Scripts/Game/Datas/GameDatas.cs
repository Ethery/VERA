using StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="GameDatas",menuName ="GameDatas")]
public class GameDatas : ScriptableObject
{
	public List<Dish> AvailableDishes;

}

public abstract class GameData : ScriptableObject
{ }