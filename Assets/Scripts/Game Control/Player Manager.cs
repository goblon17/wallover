using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public enum PlayerColor { Red, Blue, Green, Yellow };

    public int PlayerCount => players.Count;

    private HashSet<PlayerColor> players;

    private void Awake()
    {
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
    }

    public void OnPlayerDeath()
    {

    }
}
