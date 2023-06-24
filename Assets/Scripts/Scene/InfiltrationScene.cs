using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiltrationScene : BaseScene
{
    public Transform playerStartPoint;
    public Transform itemSpawnPoint;
    public List<Transform> patrolMenSpawnPoints;
    public Transform RifleManSpawnPoint;
    public CinemachineVirtualCamera virtualCamera;

    GameObject hangar;
    GameObject clearPaper;
    GameObject player;
    List<GameObject> patrolMan;
    GameObject rifleMan;

    private void Awake()
    {
        patrolMan = new List<GameObject>();
    }

    protected override IEnumerator LoadingRoutine()
    {
        progress = 0f;
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.2f;
        GameManager.UI.InitUI();
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.4f;        
        CreatePrefab();
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.6f;
        yield return new WaitForSecondsRealtime(1f);

        progress = 1f;
        yield return new WaitForSecondsRealtime(0.1f);
    }

    private void CreatePrefab()
    {
        player = GameManager.Resource.Instantiate<GameObject>("Prefabs/InfiltrationScene/Player");
        player.transform.position = playerStartPoint.position;
        player.transform.rotation = playerStartPoint.rotation;

        hangar = GameManager.Resource.Instantiate<GameObject>("Prefabs/InfiltrationScene/Hangar");
        hangar.transform.position = new Vector3(0, 0, 0);

        virtualCamera.Follow = player.transform;
        virtualCamera.LookAt = player.transform;

        clearPaper = GameManager.Resource.Instantiate<GameObject>("Prefabs/InfiltrationScene/ClearPaper");
        clearPaper.transform.position = itemSpawnPoint.position;
        clearPaper.transform.rotation = itemSpawnPoint.rotation;

        for (int i = 0; i < patrolMenSpawnPoints.Count; i++)
        {
            patrolMan.Add(GameManager.Resource.Instantiate<GameObject>("Prefabs/InfiltrationScene/PatrolMan"));
            patrolMan[i].transform.position = patrolMenSpawnPoints[i].position;
            patrolMan[i].transform.rotation = patrolMenSpawnPoints[i].rotation;
        }

        rifleMan = GameManager.Resource.Instantiate<GameObject>("Prefabs/InfiltrationScene/RifleMan");
        rifleMan.transform.position = RifleManSpawnPoint.position;
        rifleMan.transform.rotation = RifleManSpawnPoint.rotation;
    }
}
