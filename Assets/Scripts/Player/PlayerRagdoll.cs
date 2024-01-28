using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagdoll : PlayerMaterialSetter
{
    [HideInInspector]
    public GameObject PlayerParent;

	private void Update()
	{
		if(transform.position.x > 10 ||
			transform.position.x < -10 ||
			transform.position.y < -1 ||
			transform.position.z < -8 ||
			transform.position.z > 0)
		{
			Destroy();
		}
	}

	public void Destroy()
    {
        PlayerManager.Instance.OnPlayerDeath(Color);
        Destroy(PlayerParent);
    }
}
