using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBeContinuedUI : GameSceneUI
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void ChangeScene()
    {
        GameManager.Scene.LoadScene(0);
    }
}
