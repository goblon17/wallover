using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AdPlayer : Singleton<AdPlayer>
{
    [SerializeField]
    private List<VideoClip> ads;
    [SerializeField]
    private VideoPlayer player;
    [SerializeField]
    private GameObject videoScreen;

    public event System.Action AdStartedEvent;
    public event System.Action AdEndedEvent;

    public void PlayAd()
    {
        Time.timeScale = 0;
        videoScreen.SetActive(true);
        VideoClip ad = ads.GetRandomElement();
        player.clip = ad;
        player.loopPointReached += OnAdEnd;
        player.Play();
        AdStartedEvent?.Invoke();
    }

    private void OnAdEnd(VideoPlayer source)
    {
        Time.timeScale = 1;
        videoScreen.SetActive(false);
        AdEndedEvent?.Invoke();
    }
}
