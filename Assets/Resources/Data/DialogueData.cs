using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "DialogueData", menuName = "Data/Dialogue")]
public class DialogueData : ScriptableObject
{
    [SerializeField] DialogueInfo[] dialogue;
    public DialogueInfo[] Dialogue { get { return dialogue; } }


    [Serializable]
    public class DialogueInfo
    {
        public string name;

        [TextArea(3, 5)]
        public string description;
        public bool chackRead;
    }
}
