using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagdoll : PlayerMaterialSetter
{
    [HideInInspector]
    public GameObject PlayerParent;

    public void Destroy()
    {
        PlayerManager.Instance.OnPlayerDeath(Color);
        Destroy(PlayerParent);
    }
}
