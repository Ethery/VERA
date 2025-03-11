using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
	public struct Order
	{
		public int DishId;
		public float TimeLeftForPreparation;
	}

	[SerializeField]
	private List<Order> Orders = new List<Order>();
	[SerializeField]
	private Transform SpawnLocation;

	float timeSinceLaunch;

	public void PlaceOrder(int selectedDishId)
	{
		Orders.Add(new Order
		{
			DishId = selectedDishId,
			TimeLeftForPreparation = (VeraGameManager.Instance.GameRules as GameRules).AvailableDishes[selectedDishId].TimeForPreparation
		});
	}

	private void Update()
	{
		for (int i =0; i< Orders.Count; i++)
		{
			Order order = Orders[i];
			order.TimeLeftForPreparation -= Time.deltaTime;
			Orders[i] = order;
			if(order.TimeLeftForPreparation <0)
			{
				Dish dish = (VeraGameManager.Instance.GameRules as GameRules).AvailableDishes[order.DishId];
				if (dish.Prefab != null)
				{
					Instantiate(dish.Prefab, SpawnLocation.position, SpawnLocation.rotation);
				}
				Orders.RemoveAt(i);
				i--;
			}
		}
	}
}
