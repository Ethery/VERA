using System.Collections.Generic;
using UnityEngine;

public class Grabbable : EntityProperty
{
	public void Grab(Transform anchor)
	{
		Entity.transform.parent = anchor;
		Entity.transform.localPosition = Vector3.zero;
		Entity.transform.localRotation = Quaternion.identity;
		if(TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
		{
			rigidbody.isKinematic = true;
		}

		List<Collider> colliders = new List<Collider>();
		disabledColliders.Clear();
		Entity.transform.GetComponentsInChildren<Collider>(colliders);
		foreach(Collider collider in colliders)
		{
			if(!collider.isTrigger)
			{
				collider.isTrigger = true;
				disabledColliders.Add(collider);
			}
		}
	}

	public void Release()
	{
		Entity.transform.parent = null;
		if(TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
		{
			rigidbody.isKinematic = false;
		}

		foreach(Collider collider in disabledColliders)
		{
			collider.isTrigger = false;
		}
	}

	private List<Collider> disabledColliders = new List<Collider>();
}