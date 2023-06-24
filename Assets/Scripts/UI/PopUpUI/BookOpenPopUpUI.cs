using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BookOpenPopUpUI : PopUpUI, IPointerClickHandler
{
    [SerializeField] Camera renderCamera;
    [SerializeField] Transform nameText;

    Vector3 originCameraPoint;
    bool isMove = false;

    protected override void Awake()
    {
        base.Awake();

        originCameraPoint = renderCamera.transform.position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isMove = !isMove;
            Time.timeScale = 1f;
            StartCoroutine(BookMoveRoutine(isMove));
        }
    }

    IEnumerator BookMoveRoutine(bool isMove)
    {
        while (true)
        {
            if (isMove)
            {
                Vector3 targetDir = (nameText.position - renderCamera.transform.position).normalized;
                Quaternion lookDir = Quaternion.LookRotation(targetDir);
                renderCamera.transform.rotation = Quaternion.Lerp(renderCamera.transform.rotation, lookDir, 0.1f);

                renderCamera.fieldOfView = Mathf.Lerp(45f, 15f, 0.1f);

                if (renderCamera.fieldOfView == 15f)
                    yield break;
            }
            else
            {
                Vector3 targetDir = (originCameraPoint - renderCamera.transform.position).normalized;
                Quaternion lookDir = Quaternion.LookRotation(targetDir);
                renderCamera.transform.rotation = Quaternion.Lerp(renderCamera.transform.rotation, lookDir, 0.1f);

                renderCamera.fieldOfView = Mathf.Lerp(45f, 15f, 0.1f);

                if (renderCamera.fieldOfView == 15f)
                    yield break;
            }

            yield return null;
        }
    }
}
