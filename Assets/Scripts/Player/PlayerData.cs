using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private Renderer renderer;

    [HideInInspector]
    public PlayerRagdoll Ragdoll;
    [HideInInspector]
    public PlayerBonesDataManager Setter;
    [HideInInspector]
    public PlayerJumper Jumper;

    public PlayerManager.PlayerColor Color { get; private set; }

    public void OnSpawn(PlayerManager.PlayerColor playerColor, Material material)
    {
        Color = playerColor;
        renderer.sharedMaterial = material;
    }
}
