using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InfiltrationSceneUI : GameSceneUI
{
    QuestData quest;
    DialogueData dialogue;
    [SerializeField] GameObject clearTrigger;
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
        dialogue = GameManager.Resource.Load<DialogueData>("Data/InfiltrationDialogueData");

        Init();
    }

    public void Init()
    {
        curImg = buttons["PauseButton"].image;

        quest.InitQuest();

        buttons["SettingButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<SettingPopUpUI>("UI/PopUpUI/SettingUI"); });
        buttons["PauseButton"].onClick.AddListener(Pause);
        buttons["ExitButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<ExitMainMenu>("UI/PopUpUI/ExitMainMenu"); });
        texts["QuestText"].text = quest.QuestList[quest.ListIndex];
    }

    private void Start()
    {
        StartCoroutine(DialogueRoutine("Enter"));
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

    IEnumerator DialogueRoutine(string name)
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

        yield return new WaitForSeconds(5f);

        texts["DialogueText"].text = "";

        yield break;
    }

    public void Clear()
    {
        quest.ClearQuest();
        QuestRender();
        StartCoroutine(DialogueRoutine("Clear"));
        clearTrigger.SetActive(true);
    }
}
