using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct ViewCastInfo
{
    public bool hit;
    public Vector3 point;
    public float distance;
    public float angle;

    public ViewCastInfo(bool hit, Vector3 point, float distance, float angle)
    {
        this.hit = hit;
        this.point = point;
        this.distance = distance;
        this.angle = angle;
    }
}

public struct Edge
{
    public Vector3 pointA, pointB;
    public Edge(Vector3 pointA, Vector3 pointB)
    {
        this.pointA = pointA;
        this.pointB = pointB;
    }
}

public class FieldOfView : MonoBehaviour
{
    Mesh viewMesh;
    GameObject player;
    PlayerInfiltrationInteractor interactor;
    public InfiltrationScene InfiltrationScene;

    [SerializeField] int edgeResolveIterations;
    [SerializeField] float edgeDstThreshold;
    [SerializeField] MeshFilter viewMeshFilter;
    [SerializeField] float meshResolution;
    public float range;
    [SerializeField, Range(0, 360)] float angle;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] Transform headTransform;

    List<GameObject> findList;
    float cosResult;
    bool isFind;
    Vector3 targetDir;
    public Vector3 TargetDir { get { return targetDir; } }
    public bool IsFind { get { return isFind; } }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        interactor = player.GetComponent<PlayerInfiltrationInteractor>();
        findList = new List<GameObject>();
        cosResult = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
    }

    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
    }

    private void Update()
    {
        FindTarget();
        CheckFindList();

        Vector3 lookDir = headTransform.rotation.eulerAngles;
        if (!isFind)
            transform.rotation = Quaternion.Euler(new Vector3(20, lookDir.y, lookDir.z));
        else
        {
            Vector3 targetDir = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(targetDir);
        }
    }

    private void LateUpdate()
    {
        DrawFieldOfView();
    }

    public void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetMask);
        foreach (Collider collider in colliders)
        {
            targetDir = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, targetDir) > cosResult)
                AddList(collider.gameObject);
            else if (Vector3.Dot(transform.forward, targetDir) < cosResult && findList != null)
                findList.Remove(collider.gameObject);

            float distToTarget = Vector3.Distance(transform.position, collider.gameObject.transform.position);
            if (Physics.Raycast(transform.position, targetDir, distToTarget, obstacleMask) && findList != null)
            {
                findList.Remove(collider.gameObject);
                continue;
            }

            if (Vector3.Distance(transform.position, collider.gameObject.transform.position) > range && findList != null)
                findList.Remove(collider.gameObject);

            Debug.DrawRay(transform.position, targetDir * distToTarget, Color.red);
        }
    }

    private void AddList(GameObject obj)
    {
        if (findList.Count <= 0 && !interactor.IsHide)
            findList.Add (obj);
        else
        {
            if (findList.Contains(obj))
                return;            
        }
    }

    private void CheckFindList()
    {
        if (findList.Count > 0 && !interactor.IsHide)
            isFind = true;
        else
            isFind = false;
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, range);
    // 
    //     Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
    //     Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
    //     Debug.DrawRay(transform.position, rightDir * range, Color.green);
    //     Debug.DrawRay(transform.position, leftDir * range, Color.green);
    // }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }

    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(angle * meshResolution);
        float stepAngleSize = angle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo prevViewCast = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i++)
        {
            float viewAngle = transform.eulerAngles.y - angle / 2 + stepAngleSize * i;

            ViewCastInfo newViewCast = ViewCast(viewAngle);

            if (i != 0)
            {
                bool edgeDstThresholdExceed = Mathf.Abs(prevViewCast.distance - newViewCast.distance) > edgeDstThreshold;

                if (prevViewCast.hit != newViewCast.hit || (prevViewCast.hit && newViewCast.hit && edgeDstThresholdExceed))
                {
                    Edge e = FindEdge(prevViewCast, newViewCast);

                    if (e.pointA != Vector3.zero)
                        viewPoints.Add(e.pointA);
                    
                    if (e.pointB != Vector3.zero)
                        viewPoints.Add(e.pointB);
                }
            }

            viewPoints.Add(newViewCast.point);
            prevViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;

        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();

    }

    private ViewCastInfo ViewCast(float angle)
    {
        Vector3 dir = AngleToDir(angle);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, range, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, angle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * range, range, angle);
        }
    }

    private Edge FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = minAngle + (maxAngle - minAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);
            bool edgeDstThresholdExceed = Mathf.Abs(minViewCast.distance - newViewCast.distance) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceed)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new Edge(minPoint, maxPoint);
    }
}
