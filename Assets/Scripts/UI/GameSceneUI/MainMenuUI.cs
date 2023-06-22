using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : GameSceneUI
{
    protected override void Awake()
    {
        base.Awake();

        buttons["ExitButton"].onClick.AddListener(ClickExit);
        buttons["SettingButton"].onClick.AddListener(ClickSetting);
    }

    public void ClickExit()
    {
        GameManager.UI.ShowPopUpUI<GameExitConfirm>("UI/PopUpUI/ExitConfirm");
    }

    public void ClickSetting()
    {
        GameManager.UI.ShowPopUpUI<SettingPopUpUI>("UI/PopUpUI/SettingUI");
    }
}
