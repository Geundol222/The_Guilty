using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockInteractUI : PopUpUI
{
    GameObject unLockObject;
    GameObject investigateObject;

    protected override void Awake()
    {
        base.Awake();

        buttons["InvestigationButton"].onClick.AddListener(OpenInvestigation);
        buttons["UnlockingLockButton"].onClick.AddListener(OpenUnLock);
        buttons["ExitButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI<LockInteractUI>(); });
    }

    public void SetObject(GameObject unlock, GameObject invest)
    {
        unLockObject = unlock;
        investigateObject = invest;
    }

    public void OpenInvestigation()
    {
        ItemPopUpUI itemPopUpUI = GameManager.UI.ShowPopUpUI<ItemPopUpUI>("UI/PopUpUI/ItemPopUpUI");
        itemPopUpUI.DialogueRender(investigateObject.name);
        itemPopUpUI.ShowItem(investigateObject);
    }

    public void OpenUnLock()
    {
        ItemPopUpUI itemPopUpUI = GameManager.UI.ShowPopUpUI<ItemPopUpUI>("UI/PopUpUI/ItemPopUpUI");        
        itemPopUpUI.ShowItem(unLockObject);
    }
}
