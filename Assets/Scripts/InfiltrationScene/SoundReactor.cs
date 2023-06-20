using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReactor : MonoBehaviour, IListenable
{
    private Transform target;
    public Transform Target { get {  return target; } }

    bool isListen;
    public bool IsListen { get { return  isListen; } }

    public void Listen(Transform trans)
    {
        target = trans;
        isListen = true;
        StartCoroutine(ListenRoutine(target));
    }

    IEnumerator ListenRoutine(Transform trans)
    {
        Vector3 originDir = (trans.position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(originDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.5f);
        yield return new WaitForSeconds(1f);
    }
}
