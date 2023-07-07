using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static DialogueData;

public class DialogueSystem : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text textSentence;
    public bool IsEnd { get; private set; } = false;

    DialogueData data;
    Queue<string> nameQueue = new Queue<string>();
    Queue<string> sentences = new Queue<string>();

    private void Awake()
    {
        data = GameManager.Resource.Load<DialogueData>("Data/EndingDialogueData");
    }

    public void Begin(DialogueInfo[] info)
    {
        sentences.Clear();

        for (int i = 0; i < info.Length; i++)
        {
            nameQueue.Enqueue(info[i].name);
            sentences.Enqueue(info[i].description);
        }

        Next();
    }

    public void Next()
    {
        if (sentences.Count == 0)
        {
            End();
            return;
        }

        nameText.text = string.Empty;
        textSentence.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences.Dequeue(), nameQueue.Dequeue()));
    }

    IEnumerator TypeSentence(string sentence, string name)
    {
        nameText.text = name;

        foreach(char letter in sentence)
        {
            textSentence.text += letter;
            yield return new WaitForSeconds(0.08f);
        }

        yield return new WaitForSeconds(2f);
        Next();
    }

    private void End()
    {
        if (sentences != null)
        {
            IsEnd = true;
        }
    }
}
