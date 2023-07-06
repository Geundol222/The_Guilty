using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiltrationSceneUI : GameSceneUI
{
    QuestData quest;
    [SerializeField] Sprite PlayImage;
    [SerializeField] Sprite PauseImage;

    Animator anim;
    Image curImg;
    bool isPause = false;

    protected override void Awake()
    {
        base.Awake();

        anim = GetComponent<Animator>();
        quest = GameManager.Resource.Load<QuestData>("Data/InfiltrationQuest");

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
}
