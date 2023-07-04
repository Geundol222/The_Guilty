using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BookOpenPopUpUI : PopUpUI, IPointerClickHandler
{
    [SerializeField] GameObject book;
    [SerializeField] GameObject movePoint;

    Vector3 originBookPoint;
    bool isMove = false;
    bool isClicked = false;

    protected override void Awake()
    {
        base.Awake();
        originBookPoint = book.transform.position;

        buttons["ExitButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI<BookOpenPopUpUI>(); });
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
}
