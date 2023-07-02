using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator anim;
    public bool IsOpen { get; private set; } = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        anim.SetBool("IsOpen", true);
        IsOpen = true;
    }

    public void OpenSound()
    {
        GameManager.Sound.PlaySound("Audios/RoomScene/OpenDoorSound", Audio.SFX, 0.4f);
    }
}
