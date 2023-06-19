using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayerUI : InGameUI
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void ShowFindUI(Transform trans)
    {
        SetTarget(trans);
        SetOffSet(new Vector2(20f, 150f));
    }

    public void CloseFindUI()
    {
        GameManager.UI.CloseInGameUI(this);
    }
}
