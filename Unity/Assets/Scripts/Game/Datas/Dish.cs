using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dish", menuName = "GameDatas/Dish")]
public class Dish : ScriptableObject
{
	public int Price;
	public Sprite Icon;
}
