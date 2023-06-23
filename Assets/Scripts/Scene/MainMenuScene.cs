using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScene : BaseScene
{
    public void StartButton()
    {
        GameManager.Scene.LoadScene("InfiltrationScene");
    }

    protected override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
}
