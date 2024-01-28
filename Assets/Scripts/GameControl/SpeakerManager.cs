using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SpeakerManager : Singleton<SpeakerManager>
{
    [SerializeField]
    private SoundModule soundModule;
    [SerializeField]
    private WallManager wallManager;
    [SerializeField]
    private PlayerManager playerManager;
    [SerializeField]
    private float movieAdProbability;

    private void Start()
    {
        wallManager.WallStartedEvent += OnWallStarted;
        wallManager.WallEndedEvent += OnWallEnded;
    }

    private void OnWallEnded()
    {
        if (playerManager.PlayersKilledThisRound.Count <= 0)
        {
            if (playerManager.PlayersLeft.Count == 1)
            {
                string key = playerManager.PlayersLeft.Single() switch
                {
                    PlayerManager.PlayerColor.Red => "RedSuccess",
                    PlayerManager.PlayerColor.Blue => "BluSuccess",
                    PlayerManager.PlayerColor.Green => "GrnSuccess",
                    _ => "YlwSuccess",
                };
                soundModule.PlaySound(key);
            }
            else
            {
                if (Random.value <= 0.5)
                {
                    PlayerManager.PlayerColor color = playerManager.PlayersLeft.GetRandomElement();
                    string key = color switch
                    {
                        PlayerManager.PlayerColor.Red => "RedSuccess",
                        PlayerManager.PlayerColor.Blue => "BluSuccess",
                        PlayerManager.PlayerColor.Green => "GrnSuccess",
                        _ => "YlwSuccess",
                    };
                    soundModule.PlaySound(key);
                }
                else
                {
                    soundModule.PlaySound("AllSuccess");
                }
            }
        }
        else if (playerManager.PlayersLeft.Count <= 0)
        {
            if (playerManager.PlayersKilledThisRound.Count == 1)
            {
                string key = playerManager.PlayersLeft.Single() switch
                {
                    PlayerManager.PlayerColor.Red => "RedFail",
                    PlayerManager.PlayerColor.Blue => "BluFail",
                    PlayerManager.PlayerColor.Green => "GrnFail",
                    _ => "YlwFail",
                };
                soundModule.PlaySound(key);
            }
            else
            {
                soundModule.PlaySound("AllFail");
            }
        }
        else
        {
            IEnumerable<PlayerManager.PlayerColor> players = playerManager.PlayersLeft.Union(playerManager.PlayersKilledThisRound);
            PlayerManager.PlayerColor color = players.GetRandomElement();
            bool isAlive = playerManager.PlayersLeft.Contains(color);
            string key = color switch
            { 
                PlayerManager.PlayerColor.Red => "Red" + (isAlive ? "Success" : "Fail"),
                PlayerManager.PlayerColor.Blue => "Blu" + (isAlive ? "Success" : "Fail"),
                PlayerManager.PlayerColor.Green => "Grn" + (isAlive ? "Success" : "Fail"),
                _ => "Ylw" + (isAlive ? "Success" : "Fail"),
            };
            soundModule.PlaySound(key);
        }
    }

    private void OnWallStarted()
    {
        bool customMesh = wallManager.MeshMeta != WallList.MeshMeta.Normal;
        bool customMaterial = wallManager.MaterialMeta != WallList.MaterialMeta.Normal;
        if (customMesh && customMaterial)
        {
            float rand = Random.value;
            if (rand <= 0.5f)
            {
                PlayWallMesh(wallManager.MeshMeta);
            }
            else
            {
                PlayWallMaterial(wallManager.MaterialMeta);
            }
        }
        else if (!customMesh && !customMaterial)
        {
            PlayWallNormal();
        }
        else if (customMesh)
        {
            PlayWallMesh(wallManager.MeshMeta);
        }
        else
        {
            PlayWallMaterial(wallManager.MaterialMeta);
        }
    }

    private void PlayWallMesh(WallList.MeshMeta meta)
    {
        string key = meta switch
        {
            WallList.MeshMeta.Tricross => "Tricross",
            _ => "WallNormal",
        };
        soundModule.PlaySound(key);
    }

    private void PlayWallMaterial(WallList.MaterialMeta meta)
    {
        string key = meta switch
        {
            WallList.MaterialMeta.Jez => "Jezyk",
            WallList.MaterialMeta.Bullech => "Bullech",
            WallList.MaterialMeta.MechPosnan => "Mech",
            WallList.MaterialMeta.Frosch => "Frosch",
            WallList.MaterialMeta.Koziolki => "Koziolki",
            WallList.MaterialMeta.Draakus => "Draakus",
            _ => "WallNormal",
        };
        soundModule.PlaySound(key);
    }

    private void PlayWallNormal()
    {
        if (RollAndPlayMovieAd())
        {
            return;
        }
        soundModule.PlaySound("WallNormal");
    }

    public void PlayIntro()
    {
        soundModule.PlaySound("Intro");
    }

    private bool RollAndPlayMovieAd()
    {
        if (Random.value <= movieAdProbability)
        {
            AdPlayer.Instance.PlayAd();
            return true;
        }
        return false;
    }
}
