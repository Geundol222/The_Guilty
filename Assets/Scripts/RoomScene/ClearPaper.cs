using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClearPaper : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainCamera;
    [SerializeField] CinemachineVirtualCamera itemCamera; 
    [SerializeField] GameObject clearPoint;
    [SerializeField] PlayerRoomInteractor player;
    [SerializeField] RoomSceneUI roomUI;

    NavMeshAgent agent;
    QuestData quest;    
    private void Awake()
    {
        agent = player.GetComponent<NavMeshAgent>();
        quest = GameManager.Resource.Load<QuestData>("Data/RoomQuest");
    }

    public void Interact()
    {
        clearPoint.SetActive(true);

        if (player.IsWatch)
        {
            agent.isStopped = true;
            mainCamera.Priority = 1;
            itemCamera.Priority = 10;
            roomUI.DialogueRender(gameObject.name);
        }
        else
        {
            agent.isStopped = false;
            mainCamera.Priority = 10;
            itemCamera.Priority = 1;
            roomUI.DialogueClose();
        }

        quest.ClearQuest();
        roomUI.QuestRender();
    }
}
