using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private RectTransform mainMenuPanel;
    [SerializeField]
    private RectTransform playerJoinPanel;

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        mainMenuPanel.gameObject.SetActive(false);
        playerJoinPanel.gameObject.SetActive(true);
    }

    public void StartButton()
    {

    }
}
