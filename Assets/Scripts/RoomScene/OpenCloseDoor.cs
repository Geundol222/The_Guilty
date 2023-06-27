using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour
{
    Animator anim;
    bool isOpen = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckOpen();
    }

    public void CheckOpen()
    {
        if (isOpen)
        {
            anim.SetBool("IsOpen", true);
        }
        else
        {
            anim.SetBool("IsOpen", false);
        }
    }

    public void Interact()
    {
        isOpen = !isOpen;
    }
}
