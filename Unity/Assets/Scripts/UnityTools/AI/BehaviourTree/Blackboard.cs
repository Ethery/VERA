using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityTools.AI.BehaviourTree
{
	[Serializable]
	public class Blackboard
	{
		public Dictionary<string, object> Datas = new Dictionary<string, object>();
	}
}
