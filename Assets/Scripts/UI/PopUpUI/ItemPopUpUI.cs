using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPopUpUI : PopUpUI
{
    [SerializeField] GameObject itemPosition;
    GameObject uiObj;


    protected override void Awake()
    {
        base.Awake();
        buttons["ExitButton"].onClick.AddListener(RemoveItem);
    }

    public void ShowItem(GameObject obj)
    {
        uiObj = GameManager.Resource.Instantiate(obj, obj.transform.position, obj.transform.rotation, true);
        uiObj.transform.SetParent(transform, false);
    }

    public void RemoveItem()
    {
        GameManager.Resource.Destroy(uiObj);
        GameManager.UI.ClosePopUpUI<ItemPopUpUI>();
    }
}
