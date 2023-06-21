using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepRender : MonoBehaviour
{
    [HideInInspector] public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void RenderMode(bool isWalk)
    {
        if (isWalk)
            anim.SetFloat("MoveSpeed", 1f);
        else
            anim.SetFloat("MoveSpeed", 3f);
    }
}
