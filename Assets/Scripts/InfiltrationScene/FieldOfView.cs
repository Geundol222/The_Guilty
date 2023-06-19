using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField, Range(0, 360)] float angle;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    List<GameObject> findList;
    float cosResult;
    bool isFind;
    Vector3 targetDir;
    public Vector3 TargetDir { get { return targetDir; } }
    public bool IsFind { get { return isFind; } }

    private void Awake()
    {
        findList = new List<GameObject>();
        cosResult = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
    }

    private void Update()
    {
        FindTarget();
    }

    public void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetMask);
        foreach (Collider collider in colliders)
        {
            targetDir = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.position, targetDir) > cosResult)
            {
                findList.Add(collider.gameObject);                
            }
            else if (Vector3.Dot(transform.forward, targetDir) < cosResult)
            {
                if (findList.Count > 0)
                    findList.Remove(collider.gameObject);
                continue;
            }

            float distToTarget = Vector3.Distance(transform.position, collider.gameObject.transform.position);
            if (Physics.Raycast(transform.position, targetDir, distToTarget, obstacleMask))
            {
                isFind = false;
                continue;
            }

            if (findList.Count > 0)
                isFind = true;
            else
                isFind = false;
                
            Debug.DrawRay(transform.position, targetDir * distToTarget, Color.red);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position, rightDir * range, Color.green);
        Debug.DrawRay(transform.position, leftDir * range, Color.green);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
