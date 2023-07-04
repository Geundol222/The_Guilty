using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPaper : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainCamera;
    [SerializeField] CinemachineVirtualCamera itemCamera; 
    [SerializeField] GameObject clearPoint;
    [SerializeField] PlayerRoomInteractor player;
    [SerializeField] RoomSceneUI roomUI;

    QuestData quest;    
    private void Awake()
    {
        quest = GameManager.Resource.Load<QuestData>("Data/RoomQuest");
    }

    public void Interact()
    {
        clearPoint.SetActive(true);

        if (player.IsWatch)
        {
            mainCamera.Priority = 1;
            itemCamera.Priority = 10;
            roomUI.DialogueRender(gameObject.name);
        }
        else
        {
            mainCamera.Priority = 10;
            itemCamera.Priority = 1;
            roomUI.DialogueClose();
        }

        quest.ClearQuest();
        roomUI.QuestRender();
    }
}
