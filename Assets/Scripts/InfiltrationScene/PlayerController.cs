using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] List<Image> hpList;
    private Animator anim;
    private NavMeshAgent agent;
    private Collider col;
    private PlayerInput input;
    private int hp;
    private float lerpTime = 0f;
    private float fovDuration = 1f;
    private bool isDead = false;

    public bool IsDead { get { return isDead; } }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        col = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hp = 2;
    }

    private void Start()
    {
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("Hit");
        hp -= damage;

        hpList[hpList.Count - 1].color = Color.black;
        hpList.RemoveAt(hpList.Count - 1);

        if (hp <= 0)
        {
            isDead = true;
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

        while (lerpTime < fovDuration)
        {
            lerpTime += Time.deltaTime * 10f;
            playerCam.m_Lens.FieldOfView = Mathf.Lerp(55f, 20f, lerpTime / fovDuration);
        }
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(4f);
        GameManager.UI.ShowPopUpUI<GameOverUI>("UI/PopUpUI/GameOverUI");
        yield break;
    }
}
