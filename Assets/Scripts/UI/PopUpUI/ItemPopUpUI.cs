using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPopUpUI : PopUpUI
{
    [SerializeField] GameObject itemPosition;

    protected override void Awake()
    {
        base.Awake();

        buttons["Blocker"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI<ItemPopUpUI>(); });
    }


    public void ShowItem(GameObject obj)
    {
        GameManager.Resource.Instantiate(obj, itemPosition.transform.position, itemPosition.transform.rotation, true);
    }
}
