using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInteractUI : InGameUI
{
    protected override void Awake()
    {
        base.Awake();

        texts["HideText"].text = "Hide";
    }

    public void Hide()
    {
        texts["HideText"].text = "Hide";
    }

    public void Escape()
    {
        texts["HideText"].text = "Escape";
    }
}
