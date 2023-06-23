using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiltrationSceneUI : GameSceneUI
{
    [SerializeField] Sprite PlayImage;
    [SerializeField] Sprite PauseImage;

    Image curImg;
    bool isPause = false;

    protected override void Awake()
    {
        base.Awake();

        curImg = buttons["PauseButton"].image
            ;

        buttons["SettingButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<SettingPopUpUI>("UI/PopUpUI/SettingUI"); });
        buttons["PauseButton"].onClick.AddListener(Pause);
    }

    public void Pause()
    {
        if (!isPause)
        {
            isPause = true;
            Time.timeScale = 0f;
            curImg.sprite = PlayImage;
        }
        else
        {
            isPause = false;
            Time.timeScale = 1f;
            curImg.sprite = PauseImage;
        }
    }
}
