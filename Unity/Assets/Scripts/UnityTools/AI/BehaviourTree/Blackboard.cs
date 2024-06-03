using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityTools.AI.BehaviourTree
{
	public class Blackboard
	{
		[Serializable]
		public struct BlackboardValue
		{
			public string Key;
			public object Value;
		}

		public T GetValue<T>(string key) where T : class
		{
			foreach(BlackboardValue value in Values)
			{
				if(value.Key == key && value.Value.GetType().IsAssignableFrom(typeof(T)))
				{
					return key as T;
				}
			}
			return default(T);
		}

		[SerializeField]
		public List<BlackboardValue> Values = new List<BlackboardValue>();
	}
}
