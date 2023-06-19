using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCreate : MonoBehaviour
{
    [SerializeField] GameObject Citizen;

    Coroutine create;

    IEnumerator CreateRoutine()
    {
        yield return new WaitForSeconds(14f);
        Instantiate(Citizen, Citizen.transform.position, Citizen.transform.rotation);
    }

    private void Start()
    {
        StartCoroutine(CreateRoutine());
    }
}
