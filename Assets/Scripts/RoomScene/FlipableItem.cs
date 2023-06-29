using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlipableItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] RectTransform flipPoint;
    [SerializeField] RectTransform StopPoint;

    Vector3 originFlipPoint;
    bool isClicked = false;
    bool isMove = false;

    private void OnEnable()
    {
        originFlipPoint = flipPoint.transform.position;
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

    IEnumerator PopUpUIRoutine()
    {
        while (true)
        {
            if (isClicked)
            {
                if (isMove)
                {
                    flipPoint.transform.Translate(Vector3.up * Time.unscaledDeltaTime * 50f, Space.World);
                    if (Vector3.Distance(flipPoint.transform.position, StopPoint.transform.position) < 0.1f)
                    {
                        flipPoint.transform.position = StopPoint.transform.position;
                        isClicked = false;
                        yield break;
                    }
                }
                else
                {
                    flipPoint.transform.Translate(Vector3.down * Time.unscaledDeltaTime * 50f, Space.World);
                    if (Vector3.Distance(flipPoint.transform.position, originFlipPoint) < 0.1f)
                    {
                        flipPoint.transform.position = originFlipPoint;
                        isClicked = false;
                        yield break;
                    }
                }
            }

            yield return null;
        }
    }
}
