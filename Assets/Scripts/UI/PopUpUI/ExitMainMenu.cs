using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMainMenu : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();

        buttons["YesButton"].onClick.AddListener(() => { Time.timeScale = 1f; GameManager.Scene.LoadScene("MainMenuScene"); });
        buttons["NoButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI<ExitMainMenu>(); });
    }
}
