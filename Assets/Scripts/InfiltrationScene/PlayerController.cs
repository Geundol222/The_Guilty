using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    private Collider col;
    private PlayerInput input;
    private int hp;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        col = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hp = 100;
    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("Hit");

        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetFloat("MoveSpeed", 0f);
        col.enabled = false;
        input.enabled = false;
        agent.enabled = false;
        StartCoroutine(DieRoutine());
    }

    IEnumerator DieRoutine()
    {
        anim.SetInteger("Hp", -1);

        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(4f);
        GameManager.UI.ShowPopUpUI<GameOverUI>("UI/PopUpUI/GameOverUI");
        yield break;
    }
}
