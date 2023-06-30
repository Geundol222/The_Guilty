using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UnLockToLock : MonoBehaviour
{
    GameObject sceneObj;
    [SerializeField] List<GameObject> rullers;
    [SerializeField] int[] answerArr;
    [SerializeField] Image selectArrow;
    [SerializeField] Image anwerArrow;

    Animator anim;
    Vector3 originSelectArrow;
    Vector3 arrowPosition;
    int scrollAngle = 36;
    int changeIndex = 0;
    int[] numberArr = { 0, 0, 0, 0 };
    int numberRuller = 0;

    float horizontal;
    float vertical;
    Vector3 awakeRot;

    private void Awake()
    {
        originSelectArrow = selectArrow.rectTransform.anchoredPosition;
        arrowPosition = selectArrow.rectTransform.anchoredPosition;

        sceneObj = GameObject.FindGameObjectWithTag("Lock");
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        awakeRot = new Vector3(144, 0, 0);
        selectArrow.rectTransform.anchoredPosition = originSelectArrow;

        foreach (GameObject obj in rullers)
        {
            obj.transform.Rotate(awakeRot, Space.World);
        }
    }

    private void Update()
    {
        ChangeRuller();
        RotateRuller();
        CheckAnswer();
    }

    private void ChangeRuller()
    {
        if (horizontal > 0)
        {
            changeIndex++;
            numberRuller += 1;
            selectArrow.rectTransform.anchoredPosition = new Vector3(selectArrow.rectTransform.anchoredPosition.x + 100f, arrowPosition.y, arrowPosition.z);

            if (numberRuller > rullers.Count - 1)
                numberRuller = 0;

            if (numberRuller == 0)
                selectArrow.rectTransform.anchoredPosition = originSelectArrow;
        }
        else if (horizontal < 0)
        {
            changeIndex--;
            numberRuller -= 1;
            selectArrow.rectTransform.anchoredPosition = new Vector3(selectArrow.rectTransform.anchoredPosition.x - 100f, arrowPosition.y, arrowPosition.z);

            if (numberRuller < 0)
                numberRuller = 3;

            if (numberRuller == 3)
                selectArrow.rectTransform.anchoredPosition = new Vector3(selectArrow.rectTransform.anchoredPosition.x + 400f, arrowPosition.y, arrowPosition.z);
        }
        changeIndex = (changeIndex + rullers.Count) % rullers.Count;
        horizontal = 0;
    }

    private void OnChangeRuller(InputValue value)
    {
        horizontal = value.Get<float>();
    }

    private void RotateRuller()
    {
        if (vertical > 0)
        {
            rullers[changeIndex].transform.Rotate(-scrollAngle, 0, 0, Space.Self);

            numberArr[changeIndex] += 1;

            if (numberArr[changeIndex] > 9)
                numberArr[changeIndex] = 0;
        }
        else if (vertical < 0)
        {
            rullers[changeIndex].transform.Rotate(scrollAngle, 0, 0, Space.Self);

            numberArr[changeIndex] -= 1;

            if (numberArr[changeIndex] < 0)
                numberArr[changeIndex] = 9;
        }
        vertical = 0;
    }

    private void OnRotateRuller(InputValue value)
    {
        vertical = value.Get<float>();
    }

    private void CheckAnswer()
    {
        int count = 0;
        for (int i = 0; i < answerArr.Length; i++)
        {
            if (numberArr[i] == answerArr[i])
                count++;
            else
                return;
        }

        if (count >= 4)
        {
            anim.SetBool("IsOpen", true);
        }
    }

    public void CloseUI()
    {
        GameManager.Resource.Destroy(sceneObj);
        GameManager.UI.ClosePopUpUI<ItemPopUpUI>();
        GameManager.UI.ClosePopUpUI<LockInteractUI>();        
    }

    private void OnDisable()
    {
        selectArrow.rectTransform.anchoredPosition = originSelectArrow;
    }
}
