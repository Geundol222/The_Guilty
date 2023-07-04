using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPopUpUI : PopUpUI
{
    [SerializeField] GameObject itemPosition;
    GameObject uiObj;
    DialogueData dialogue;
    RoomSceneUI roomUI;


    protected override void Awake()
    {
        base.Awake();
        dialogue = GameManager.Resource.Load<DialogueData>("Data/RoomDialogueData");

        buttons["ExitButton"].onClick.AddListener(RemoveItem);
        texts["DialogueText"].text = "";
    }

    public void ShowItem(GameObject obj)
    {
        uiObj = GameManager.Resource.Instantiate(obj, obj.transform.position, obj.transform.rotation, true);
        uiObj.transform.SetParent(transform, false);
    }

    public void RemoveItem()
    {
        GameManager.Resource.Destroy(uiObj);
        texts["DialogueText"].text = "";
        GameManager.UI.ClosePopUpUI<ItemPopUpUI>();
    }

    public void DialogueRender(string name)
    {
        for (int i = 0; i < dialogue.Dialogue.Length; i++)
        {
            if (name.Contains(dialogue.Dialogue[i].name))
            {
                texts["DialogueText"].text = dialogue.Dialogue[i].description;
            }
            else
                continue;
        }
    }
}
