using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : Singleton<PlayerManager>
{
    public enum PlayerColor { Red, Blue, Green, Yellow };

    [SerializeField]
    private SerializedDictionary<PlayerColor, Material> playerMaterials;

    [SerializeField]
    private SerializedDictionary<PlayerColor, Vector3> jumpersPositions;

	[SerializeField]
	private SerializedDictionary<PlayerColor, Vector3> playersSpawnPositions;

	[SerializeField]
    private GameObject jumperPrefab;

    public HashSet<PlayerColor> PlayersKilledThisRound { get; private set; } = new HashSet<PlayerColor>();
    public HashSet<PlayerColor> PlayersLeft { get; private set; } = new HashSet<PlayerColor>();

    public int PlayerCount => players.Count;

    private Dictionary<PlayerColor, PlayerData> players;

    protected override void Awake()
    {
        base.Awake();
        players = new Dictionary<PlayerColor, PlayerData>();
		WallManager.Instance.WallStartedEvent += OnWallStarted;
    }

	private void OnWallStarted()
	{
        PlayersKilledThisRound.Clear();
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
        PlayerData data = playerInput.GetComponent<PlayerData>();
		players[playerColor] = data;
		data.OnSpawn(playerColor, playerMaterials[playerColor]);
        data.Setter = playerInput.GetComponentInChildren<PlayerBodyMover>().GetComponentInChildren<PlayerBonesDataManager>();
        data.Jumper = playerInput.GetComponentInChildren<PlayerJumper>();
		playerInput.transform.position = playersSpawnPositions[playerColor];
        data.Jumper.transform.position = jumpersPositions[playerColor];
		data.Jumper.PlayerMaterial = playerMaterials[playerColor];
		data.Jumper.Color = playerColor;
        PlayersLeft.Add(playerColor);
	}

    public void OnPlayerDeath(PlayerColor color)
    {
        Debug.Log($"Player {color} died");
        PlayersLeft.Remove(color);
        PlayersKilledThisRound.Add(color);
    }
}
