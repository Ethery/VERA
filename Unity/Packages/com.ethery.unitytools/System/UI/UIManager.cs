using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Systems.Collections;

namespace UnityTools.Systems.UI
{
	public class UIManager<ETYPE> : Singleton<UIManager<ETYPE>>, ISerializationCallbackReceiver
	{
		public T Page<T>(int pageId) where T : Page
		{
			return m_loaded[pageId] as T;
		}

		[SerializeField]
		private SerializableDictionary<ETYPE, Page> m_prefabs;

		[SerializeField]
		private Transform UICanvas;

		#region Datas
		
		private List<Page> m_loaded = new List<Page>();

		#endregion

		public int CreatePage(ETYPE page)
		{
			m_loaded.Add(Instantiate(m_prefabs[page], UICanvas));
			return m_loaded.Count - 1;
		}

		public void DestroyPage(int pageId)
		{
			Page page = Page<Page>(pageId);
			m_loaded.RemoveAt(pageId);
			GameObject.Destroy(page.gameObject);
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			foreach(ETYPE type in Enum.GetValues(typeof(ETYPE)))
			{
				if(!m_prefabs.ContainsKey(type))
				{
					m_prefabs.Add(type, null);
				}
			}
		}

	}
}