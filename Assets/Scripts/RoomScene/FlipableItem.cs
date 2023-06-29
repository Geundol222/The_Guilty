using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlipableItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] RectTransform flipPoint;
    [SerializeField] RectTransform stopPoint;
    [SerializeField] RectTransform startPoint;

    bool isClicked = false;
    bool isMove = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isClicked = true;
            isMove = !isMove;
            StartCoroutine(PopUpUIRoutine());
        }
    }

    IEnumerator PopUpUIRoutine()
    {
        while (true)
        {
            if (isClicked)
            {
                if (isMove)
                {
                    flipPoint.transform.Translate(Vector3.up * Time.unscaledDeltaTime * 2000f, Space.World);
                    if ((stopPoint.transform.position.y - flipPoint.transform.position.y) < 0.1f)
                    {
                        flipPoint.transform.position = stopPoint.transform.position;
                        isClicked = false;
                        yield break;
                    }
                }
                else
                {
                    flipPoint.transform.Translate(Vector3.down * Time.unscaledDeltaTime * 2000f, Space.World);
                    if ((flipPoint.transform.position.y - startPoint.transform.position.y) < 0.1f)
                    {
                        flipPoint.transform.position = startPoint.transform.position;
                        isClicked = false;
                        yield break;
                    }
                }
            }

            yield return null;
        }
    }
}
