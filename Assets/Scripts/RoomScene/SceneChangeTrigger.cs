using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour
{
    [SerializeField] LayerMask player;

    private void OnTriggerEnter(Collider other)
    {
        if (player.IsContain(other.gameObject.layer))
        {
            GameManager.UI.ShowPopUpUI<SceneChangeConfirm>("UI/PopUpUI/SceneChangeConfirm");
        }
    }
}
