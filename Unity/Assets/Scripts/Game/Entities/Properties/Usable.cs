using UnityEngine;
using UnityEngine.Events;

public class Usable : EntityProperty
{
	public bool IsUsed => m_user != null;
	public Entity User => m_user;

	public void Use(Entity source)
	{
		if (m_user == null && source != null)
		{
			m_user = source;
			Debug.Log($"{nameof(Usable)} used by {source}");
			if (OnUse != null)
			{
				OnUse.Invoke(source, true);
			}
		}
	}

	public void UnUse(Entity source)
	{
		if (m_user == source)
		{
			m_user = null;
            Debug.Log($"{nameof(Usable)} no longer used by {source}");
            if (OnUse != null)
			{
				OnUse.Invoke(source, false);
			}
		}
	}

	[SerializeField]
	public Entity m_user;
	[SerializeField]
	public UnityAction<Entity, bool> OnUse;
}
