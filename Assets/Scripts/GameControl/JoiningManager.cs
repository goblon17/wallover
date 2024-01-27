using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JoiningManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private RectTransform playersJoiningPanel;
    [SerializeField]
    private TextMeshProUGUI playerCount;
    [SerializeField]
    private TextMeshProUGUI countdown;
    [SerializeField]
    private float timeToJoin;

    private bool started = false;
    private float counter = 0;

    private void Awake()
    {
        gameManager.GameStateChangedEvent += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager.State newState)
    {
        if (newState != GameManager.State.Joining)
        {
            return;
        }

        StartPlayerJoining();
    }

    private void StartPlayerJoining()
    {
        Time.timeScale = 0;
        started = true;
        counter = 0;
        playersJoiningPanel.gameObject.SetActive(true);
    }

    private void EndPlayerJoining()
    {
        Time.timeScale = 1;
        started = false;
        playersJoiningPanel.gameObject.SetActive(false);
        gameManager.EndState(GameManager.State.Joining);
    }

    private void Update()
    {
        if (!started)
        {
            return;
        }

        if (playerManager.PlayerCount > 0)
        {
            counter += Time.unscaledDeltaTime;
            countdown.text = $"{timeToJoin - counter}";
        }
        else
        {
            countdown.text = "∞";
        }

        playerCount.text = $"{playerManager.PlayerCount}/4";

        if (counter >= timeToJoin)
        {
            EndPlayerJoining();
        }
    }
}
