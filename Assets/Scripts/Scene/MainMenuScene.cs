using Redcode.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScene : BaseScene
{
    private void Awake()
    {
        GameManager.Sound.PlaySound("Audios/MainMenu/MainBGM", Audio.BGM, 0.5f, 0.9f);
        GameManager.Sound.PlaySound("Audios/MainMenu/RainSound", Audio.SFX, 0.4f, 1f, true);
    }

    protected override IEnumerator LoadingRoutine()
    {
        progress = 0f;
        yield return new WaitForSecondsRealtime(0.5f);

        progress = 0.2f;
        yield return new WaitForSecondsRealtime(0.5f);

        progress = 0.4f;
        yield return new WaitForSecondsRealtime(0.5f);

        progress = 0.6f;
        yield return new WaitForSecondsRealtime(0.5f);

        progress = 1f;        
        yield return new WaitForSecondsRealtime(0.1f);
    }
}
