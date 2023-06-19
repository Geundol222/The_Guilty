using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameExitConfirm : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();

        buttons["YesButton"].onClick.AddListener(GameQuit);
        buttons["NoButton"].onClick.AddListener(NoQuit);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void NoQuit()
    {
        this.gameObject.SetActive(false);
    }
}
