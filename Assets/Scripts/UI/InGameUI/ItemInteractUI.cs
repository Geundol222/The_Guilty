using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractUI : InGameUI
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void ItemText()
    {
        texts["ItemText"].text = "Pick";
    }

    public void WatchText()
    {
        texts["ItemText"].text = "Watch";
    }
}
