using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadLock : MonoBehaviour
{
    [SerializeField] GameObject paper;
    [SerializeField] GameObject unLockObject;
    [SerializeField] GameObject investigateObject;

    public void Interact()
    {
        LockInteractUI lockInteractUI = GameManager.UI.ShowPopUpUI<LockInteractUI>("UI/PopUpUI/LockInteractUI");
        lockInteractUI.SetObject(unLockObject.gameObject, investigateObject);
        paper.SetActive(true);
    }
}
