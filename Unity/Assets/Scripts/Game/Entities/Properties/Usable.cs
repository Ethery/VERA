using UnityEngine;
using UnityEngine.Events;

public class Usable : EntityProperty
{

	public void Use(Entity source)
	{
		Debug.Log($"{nameof(Usable)} used by {source}");
		if (OnUse != null)
		{
			OnUse.Invoke(source, true);
		}
	}

	public void UnUse(Entity source)
	{
        Debug.Log($"{nameof(Usable)} no longer used by {source}");
        if (OnUse != null)
		{
			OnUse.Invoke(source, false);
		}
	}

	[SerializeField]
	public UnityAction<Entity, bool> OnUse;
}
