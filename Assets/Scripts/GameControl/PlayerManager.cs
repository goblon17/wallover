using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : Singleton<PlayerManager>
{
    public enum PlayerColor { Red, Blue, Green, Yellow };

    [SerializeField]
    private SerializedDictionary<PlayerColor, Material> playerMaterials;

    public int PlayerCount => players.Count;

    private HashSet<PlayerColor> players;


    protected override void Awake()
    {
        base.Awake();
        players = new HashSet<PlayerColor>();
    }

    private PlayerColor GetAvailableColor()
    {
        foreach (PlayerColor player in Enum.GetValues(typeof(PlayerColor)))
        {
            if (!players.Contains(player))
            {
                return player;
            }
        }
        return PlayerColor.Yellow;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        PlayerColor playerColor = GetAvailableColor();
        players.Add(playerColor);
        playerInput.GetComponent<PlayerData>().OnSpawn(playerColor, playerMaterials[playerColor]);
    }

    public void OnPlayerDeath()
    {

    }
}
