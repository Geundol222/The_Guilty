using EnemyStates;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;

    [HideInInspector] public FindPlayerUI findUI;
    [HideInInspector] public EnemyWeaponHolder weaponHolder;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator anim;
    [HideInInspector] public FieldOfView fov;

    protected virtual void Awake()
    {
        findUI = GameManager.UI.ShowInGameUI<FindPlayerUI>("UI/InGameUI/FindPlayerUI");
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
}
