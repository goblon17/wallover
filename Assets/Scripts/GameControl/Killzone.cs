using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out PlayerRagdoll ragdoll))
		{
			ragdoll.Destroy();
		}
	}
}
