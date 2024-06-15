using System;

public class Usable : EntityProperty
{
	public void Use()
	{
		OnUse.Invoke();
	}

	public Action OnUse;
}