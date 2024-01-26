using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class RigidbodyDisabler : MonoBehaviour
{
	[SerializeField]
	private bool disableGravity;

	private void Start()
	{
		if (disableGravity)
		{
			SetGravity(false);
		}
	}

	public void SetGravity(bool value)
	{
		var comp = GetComponentsInChildren<Rigidbody>();
		comp.Select(r => r.useGravity = value);
	}
}
