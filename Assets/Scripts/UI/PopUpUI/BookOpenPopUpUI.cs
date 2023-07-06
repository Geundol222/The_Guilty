using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BookOpenPopUpUI : PopUpUI, IPointerClickHandler
{
    [SerializeField] GameObject book;
    [SerializeField] GameObject movePoint;

    InfiltrationSceneUI sceneUI;
    DialogueData dialogue;
    Vector3 originBookPoint;
    bool isMove = false;
    bool isClicked = false;

    protected override void Awake()
    {
        base.Awake();
        originBookPoint = book.transform.position;
        sceneUI = FindObjectOfType<InfiltrationSceneUI>();
        dialogue = GameManager.Resource.Load<DialogueData>("Data/InfiltrationDialogueData");

        buttons["ExitButton"].onClick.AddListener(CloseUI);
        DialogueRender("NoteBook");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isClicked = true;
            isMove = !isMove;

            StartCoroutine(PopUpUIRoutine());
        }
    }

    protected override IEnumerator PopUpUIRoutine()
    {
        while (true)
        {
            if (isClicked)
            {
                if (isMove)
                {
                    Vector3 moveDir = (movePoint.transform.position - originBookPoint).normalized;

                    book.transform.Translate(moveDir * 15f * Time.unscaledDeltaTime, Space.World);

                    if (Vector3.Distance(book.transform.position, movePoint.transform.position) < 0.1f)
                    {
                        DialogueRender("Click");
                        book.transform.position = movePoint.transform.position;
                        isClicked = false;
                        yield break;
                    }
                }
                else
                {
                    Vector3 returnDir = (originBookPoint - book.transform.position).normalized;

                    book.transform.Translate(returnDir * 15f * Time.unscaledDeltaTime, Space.World);

                    if (Vector3.Distance(book.transform.position, originBookPoint) < 0.1f)
                    {
                        book.transform.position = originBookPoint;
                        isClicked = false;
                        yield break;
                    }
                }
            }

            yield return null;
        }
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

    public void CloseUI()
    {
        sceneUI.Clear();
        GameManager.UI.ClosePopUpUI<BookOpenPopUpUI>();
    }

    public void PageSound()
    {
        GameManager.Sound.PlaySound("Audios/RoomScene/PageFlipSound", Audio.SFX, 0.5f);
    }
}
