using Redcode.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScene : BaseScene
{
    private void Awake()
    {
        GameManager.Sound.PlaySound("Audios/MainMenu/MainBGM", Audio.BGM, 0.9f);
        GameManager.Sound.PlaySound("Audios/MainMenu/RainSound");
    }

    protected override IEnumerator LoadingRoutine()
    {
        progress = 0f;
        GameManager.Sound.PlaySound("Audios/MainMenu/MainBGM", Audio.BGM, 0.9f);
        GameManager.Sound.PlaySound("Audios/MainMenu/RainSound");
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.2f;
        GameManager.UI.InitUI();
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.4f;
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.6f;
        yield return new WaitForSecondsRealtime(1f);

        progress = 1f;
        yield return new WaitForSecondsRealtime(0.1f);
    }
}
