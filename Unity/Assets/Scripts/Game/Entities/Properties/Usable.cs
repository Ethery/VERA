using UnityEngine;
using UnityEngine.Events;

public class Usable : EntityProperty
{
	public void Use(Entity source)
	{
		OnUse.Invoke(source);
	}

	[SerializeField]
	public UnityAction<Entity> OnUse;
}
