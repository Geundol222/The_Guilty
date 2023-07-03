using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "QuestData", menuName = "Data/Quest")]
public class QuestData : ScriptableObject
{
    [SerializeField] List<string> questList;

    public List<string> QuestList { get { return questList; } }

    int listIndex = 0;
    public int ListIndex { get { return listIndex; } }

    public void InitQuest()
    {
        listIndex = 0;
    }

    public void ClearQuest()
    {
        if ((questList.Count - 1) > listIndex)
            listIndex++;
        else
            return;
    }
}
