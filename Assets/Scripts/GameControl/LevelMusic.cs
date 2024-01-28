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
    }

    private void OnWallStarted()
    {
        audioSource.clip = musics.GetRandomElement();
        audioSource.Play();
    }
}
