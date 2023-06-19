using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuDestroy : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(LookingBack());
        Destroy(gameObject, 14f);
    }

    IEnumerator LookingBack()
    {
        yield return new WaitForSeconds(10f);
        anim.SetTrigger("LookBeHind");
    }
}
