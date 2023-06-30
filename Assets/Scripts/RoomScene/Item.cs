using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] GameObject UIObject;

    public void Interact()
    {
        ItemPopUpUI itemPopUpUI = GameManager.UI.ShowPopUpUI<ItemPopUpUI>("UI/PopUpUI/ItemPopUpUI");
        itemPopUpUI.ShowItem(UIObject);
    }
}
