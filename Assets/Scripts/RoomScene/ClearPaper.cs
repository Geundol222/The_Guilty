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
    
    public void Interact()
    {
        clearPoint.SetActive(true);

        if (player.IsWatch)
        {
            mainCamera.Priority = 1;
            itemCamera.Priority = 10;
        }
        else
        {
            mainCamera.Priority = 10;
            itemCamera.Priority = 1;
        }
    }
}
