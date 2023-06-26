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
    bool isClicked = false;

    protected override void Awake()
    {
        base.Awake();

        originCameraPoint = renderCamera.transform.position;
    }

    private void LateUpdate()
    {
        if (isClicked)
        {
            if (isMove)
            {
                Vector3 targetDir = (nameText.position - renderCamera.transform.position).normalized;
                Quaternion lookDir = Quaternion.LookRotation(targetDir);
                renderCamera.transform.rotation = Quaternion.Lerp(renderCamera.transform.rotation, lookDir, 0.1f);

                renderCamera.fieldOfView = Mathf.Lerp(45f, 15f, 0.1f);
            }
            else
            {
                Vector3 targetDir = (originCameraPoint - renderCamera.transform.position).normalized;
                Quaternion lookDir = Quaternion.LookRotation(targetDir);
                renderCamera.transform.rotation = Quaternion.Lerp(renderCamera.transform.rotation, lookDir, 0.1f);

                renderCamera.fieldOfView = Mathf.Lerp(15f, 45f, 0.1f);
            }
        }
        else
            return;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isClicked = true;
            isMove = !isMove;
            Time.timeScale = 1f;
        }
    }
}
