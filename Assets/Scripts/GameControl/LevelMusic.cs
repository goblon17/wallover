using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> musics;

    private void Start()
    {
        WallManager.Instance.WallStartedEvent += OnWallStarted;
        AdPlayer.Instance.AdStartedEvent += () => audioSource.Stop();
        AdPlayer.Instance.AdEndedEvent += () => audioSource.Play();
    }

    private void OnWallStarted()
    {
        audioSource.clip = musics.GetRandomElement();
        audioSource.Play();
    }
}
