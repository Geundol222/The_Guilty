using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();

        buttons["AudioButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<AudioSettingUI>("UI/PopUpUI/AudioSettingUI"); });
        buttons["ResolutionButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<ResolutionSettingUI>("UI/PopUpUI/ResolutionSettingUI"); });
        buttons["ExitButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI<SettingUI>(); });
    }
}
