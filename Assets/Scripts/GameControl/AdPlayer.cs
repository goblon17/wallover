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

    public void PlayAd()
    {
        Time.timeScale = 0;
        videoScreen.SetActive(true);
        VideoClip ad = ads.GetRandomElement();
        player.clip = ad;
        player.loopPointReached += OnAdEnd;
        player.Play();
    }

    private void OnAdEnd(VideoPlayer source)
    {
        Time.timeScale = 1;
        videoScreen.SetActive(false);
    }
}
