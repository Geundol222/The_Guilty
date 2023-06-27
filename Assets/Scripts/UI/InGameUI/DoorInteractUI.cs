using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractUI : InGameUI
{
    protected override void Awake()
    {
        base.Awake();

        texts["DoorText"].text = "Open";
    }

    public void Open()
    {
        texts["DoorText"].text = "Open";
    }

    public void Close()
    {
        texts["DoorText"].text = "Close";
    }
}
