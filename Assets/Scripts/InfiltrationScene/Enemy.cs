using EnemyStates;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemyWeaponHolder weaponHolder;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator anim;
    [HideInInspector] public FieldOfView fov;
    [HideInInspector] public GameObject player;
    [HideInInspector] public bool isFind;
    [HideInInspector] public PlayerController playerController;

    FindPlayerUI findUI;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        weaponHolder = GetComponentInChildren<EnemyWeaponHolder>();
        fov = GetComponentInChildren<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    public IEnumerator LookRoutine(Transform trans)
    {
        while (true)
        {
            if (fov.IsFind)
            {
                Vector3 lookDir = (trans.position - transform.position).normalized;
                Quaternion lookRot = Quaternion.LookRotation(new Vector3(lookDir.x, 0, lookDir.z));
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, 0.1f);
                yield return null;
            }
            else
                yield break;
        }
    }

    public void FindUIRender(bool find)
    {
        if (find)
        {
            if (findUI == null || !findUI.gameObject.activeSelf)
            {
                findUI = GameManager.UI.ShowInGameUI<FindPlayerUI>("UI/InGameUI/FindPlayerUI");
                findUI.ShowFindUI(transform);
            }
            else
                return;
        }
        else
        {
            if (findUI != null && findUI.gameObject.activeSelf)
                findUI.CloseFindUI();
            else
                return;
        }
    }
}
