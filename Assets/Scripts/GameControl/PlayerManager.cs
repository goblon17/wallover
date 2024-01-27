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

    [SerializeField]
    private SerializedDictionary<PlayerColor, PlayerJumper> playerJumpers; 

    public int PlayerCount => players.Count;

    private Dictionary<PlayerColor, PlayerBonesDataManager> players;


    protected override void Awake()
    {
        base.Awake();
        players = new Dictionary<PlayerColor, PlayerBonesDataManager>();
        foreach (PlayerJumper jumper in playerJumpers.Values)
        {
            jumper.gameObject.SetActive(false);
        }
    }

    private PlayerColor GetAvailableColor()
    {
        foreach (PlayerColor player in Enum.GetValues(typeof(PlayerColor)))
        {
            if (!players.ContainsKey(player))
            {
                return player;
            }
        }
        return PlayerColor.Yellow;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        PlayerColor playerColor = GetAvailableColor();
        players[playerColor] = playerInput.GetComponentInChildren<PlayerBonesDataManager>();
        playerInput.GetComponent<PlayerData>().OnSpawn(playerColor, playerMaterials[playerColor]);
    }

    public void SpawnJumpers()
    {
		foreach (PlayerColor player in Enum.GetValues(typeof(PlayerColor)))
        {
			if (players.ContainsKey(player))
            {
				playerJumpers[player].gameObject.SetActive(true);
                playerJumpers[player].Jump(players[player].GetBonesData());
			}
		}
	}

    public void OnPlayerDeath()
    {

    }
}
