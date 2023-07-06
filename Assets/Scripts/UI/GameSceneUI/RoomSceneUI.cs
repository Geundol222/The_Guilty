using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoomSceneUI : GameSceneUI
{
    QuestData quest;
    DialogueData dialogue;
    [SerializeField] Sprite PlayImage;
    [SerializeField] Sprite PauseImage;

    Animator anim;
    Image curImg;
    bool isPause = false;

    protected override void Awake()
    {
        base.Awake();

        anim = GetComponent<Animator>();
        quest = GameManager.Resource.Load<QuestData>("Data/RoomQuest");
        dialogue = GameManager.Resource.Load<DialogueData>("Data/RoomDialogueData");

        Init();
    }

    public void Init()
    {
        curImg = buttons["PauseButton"].image;

        quest.InitQuest();

        buttons["SettingButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<SettingUI>("UI/PopUpUI/SettingUI"); });
        buttons["PauseButton"].onClick.AddListener(Pause);
        buttons["ExitButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<ExitMainMenu>("UI/PopUpUI/ExitMainMenu"); });
        texts["QuestText"].text = quest.QuestList[quest.ListIndex];
        texts["DialogueText"].text = "";
    }

    public void Pause()
    {
        if (!isPause)
        {
            isPause = true;
            Time.timeScale = 0f;
            curImg.sprite = PlayImage;
        }
        else
        {
            isPause = false;
            Time.timeScale = 1f;
            curImg.sprite = PauseImage;
        }
    }

    public void QuestRender()
    {
        texts["QuestText"].text = quest.QuestList[quest.ListIndex];
        anim.Play("QuestText");
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

    public void DialogueClose()
    {
        texts["DialogueText"].text = "";
    }
}
