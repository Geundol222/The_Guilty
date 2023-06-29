using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemPopUpUI : PopUpUI
{
    [SerializeField] GameObject itemPosition;

    GameObject uiObj;
    
    protected override void Awake()
    {
        base.Awake();

        buttons["Blocker"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI<ItemPopUpUI>(); });
        texts["RotateText"].text = "Rotate";
    }

    public void ShowItem(GameObject obj)
    {
        uiObj = GameManager.Resource.Instantiate(obj, obj.transform.position, obj.transform.rotation, true);
        uiObj.transform.SetParent(transform, false);
    }
}
