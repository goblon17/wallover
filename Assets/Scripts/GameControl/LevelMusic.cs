using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> musics;

    private bool isPlayingAd = false;

    private void Start()
    {
        WallManager.Instance.WallStartedEvent += OnWallStarted;
        AdPlayer.Instance.AdStartedEvent += () =>
        {
            audioSource.Stop();
            isPlayingAd = true;
        };
        AdPlayer.Instance.AdEndedEvent += () =>
        {
            audioSource.Play();
            isPlayingAd = false;
        };
    }

    private void OnWallStarted()
    {
        audioSource.clip = musics.GetRandomElement();
        if (!isPlayingAd)
        {
            audioSource.Play();
        }
    }
}
