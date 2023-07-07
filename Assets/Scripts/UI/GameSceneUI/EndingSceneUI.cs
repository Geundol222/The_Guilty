using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EndingSceneUI : GameSceneUI
{
    [SerializeField] DialogueSystem dialogue;

    private DialogueData data;
    private Animator anim;

    public UnityEvent OnCameraStart;

    protected override void Awake()
    {
        base.Awake();

        texts["Name"].text = "";
        texts["Dialogue"].text = "";

        anim = GetComponent<Animator>();
        data = GameManager.Resource.Load<DialogueData>("Data/EndingDialogueData");
    }

    public void DialogueStart()
    {
        StartCoroutine(StartRoutine());
    }

    IEnumerator StartRoutine()
    {
        dialogue.Begin(data.Dialogue);
        yield return new WaitUntil(() => { return dialogue.IsEnd; });

        OnCameraStart?.Invoke();

        GameManager.Resource.Destroy(dialogue.gameObject);
        GameManager.Resource.Destroy(gameObject);

    }

    public void PlayPhoneRing()
    {
        GameManager.Sound.PlaySound("Audios/EndingScene/RingToneSound", Audio.SFX, 0.3f);
    }

    public void BeepSound()
    {
        GameObject ring = GameObject.Find("RingToneSound");
        GameManager.Resource.Destroy(ring.gameObject);
        GameManager.Sound.PlaySound("Audios/EndingScene/PhoneBeepSound", Audio.SFX, 0.3f);
    }
}
