using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EndingSceneUI : GameSceneUI
{
    [SerializeField] DialogueSystem dialogue;

    public UnityEvent OnCameraStart;

    protected override void Awake()
    {
        base.Awake();

        texts["Name"].text = "";
        texts["Dialogue"].text = "";
    }

    private void Start()
    {
        StartCoroutine(StartRoutine());
    }

    IEnumerator StartRoutine()
    {
        yield return new WaitUntil(() => { return dialogue.IsEnd; });

        OnCameraStart?.Invoke();

        GameManager.Resource.Destroy(dialogue.gameObject);
        GameManager.Resource.Destroy(gameObject);

    }
}
