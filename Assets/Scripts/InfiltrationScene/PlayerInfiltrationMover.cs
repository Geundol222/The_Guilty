using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerInfiltrationMover : MonoBehaviour
{
    [SerializeField] bool debug;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] float walkStepRange;
    [SerializeField] float runStepRange;

    private PlayerInfiltrationInteractor interactor;
    private FootStepRender wave;
    private bool isWalk;
    private Animator anim;
    private Camera mainCam;
    private NavMeshAgent agent;
    private RaycastHit hit;
    private bool isDiscovered = false;
    private bool isCrouching = false;
    private float originWalkStepRange;
    private float originRunStepRange;


    private void Awake()
    {
        interactor = GetComponent<PlayerInfiltrationInteractor>();
        wave = GetComponentInChildren<FootStepRender>();
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        originWalkStepRange = walkStepRange;
        originRunStepRange = runStepRange;
    }

    private void Update()
    {
        Move();
        Crouch();
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, hit.point) < 0.5f)
        {
            anim.SetFloat("MoveSpeed", 0f);
            wave.anim.SetFloat("MoveSpeed", 0f);
        }
    }

    private void OnMove(InputValue value)
    {
        if (Time.timeScale > 0f)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask) && !interactor.IsHide)
            {
                wave.RenderMode(isWalk);

                if (groundMask.IsContain(hit.collider.gameObject.layer))
                {
                    if (isWalk && !interactor.IsHide)
                        anim.SetFloat("MoveSpeed", 1f);
                    else if (!isWalk && !interactor.IsHide)
                        anim.SetFloat("MoveSpeed", 3f);
                }

                agent.destination = hit.point;
            }
            Move();

            StartCoroutine(StepSoundRoutine());
        }
        else
            return;
    }

    IEnumerator StepSoundRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            if (isDiscovered)
            {
                yield return new WaitForSeconds(10f);
                walkStepRange = originWalkStepRange;
                runStepRange = originRunStepRange;
                isDiscovered = false;
            }
            else
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, isWalk ? walkStepRange : runStepRange, enemyMask);

                foreach (Collider collider in colliders)
                {
                    isDiscovered = true;
                    IListenable listenable = collider.GetComponent<IListenable>();
                    listenable?.Listen(transform.position);
                    walkStepRange *= 0.5f;
                    runStepRange *= 0.5f;
                }
            }

            yield return null;
        }
    }

    private void Crouch()
    {
        if (isCrouching)
        {
            isWalk = true;
            anim.SetBool("IsCrouch", true);
            agent.speed = 5f;
        }
        else
        {
            isWalk = false;
            anim.SetBool("IsCrouch", false);
            agent.speed = 15f;
        }
    }

    private void OnCrouch(InputValue value)
    {
        if (Time.timeScale > 0f)
            isCrouching = !isCrouching;
        else
            return;
    }

    public void FirstStepSound()
    {
        if (isCrouching)
            GameManager.Sound.PlaySound("Audios/InfiltrationScene/CrouchWalk1", Audio.SFX, 0.5f);
        else
            GameManager.Sound.PlaySound("Audios/InfiltrationScene/RunSound1", Audio.SFX, 0.5f);
    }

    public void SecondStepSound()
    {
        if (isCrouching)
            GameManager.Sound.PlaySound("Audios/InfiltrationScene/CrouchWalk2", Audio.SFX, 0.5f);
        else
            GameManager.Sound.PlaySound("Audios/InfiltrationScene/RunSound2", Audio.SFX, 0.5f);
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.cyan;
    //     Gizmos.DrawWireSphere(transform.position, walkStepRange);
    //     Gizmos.DrawWireSphere(transform.position, runStepRange);
    // }
}
