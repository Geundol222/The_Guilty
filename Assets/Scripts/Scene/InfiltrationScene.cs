using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiltrationScene : BaseScene
{
    public Transform itemSpawnPoint;
    public List<Transform> enemySpawnPoints;

    GameObject clearPaper;
    List<GameObject> patrolMans;

    private void Awake()
    {
        patrolMans = new List<GameObject>();
    }

    protected override IEnumerator LoadingRoutine()
    {
        progress = 0f;
        yield return new WaitForSecondsRealtime(0.5f);

        progress = 0.2f;
        yield return new WaitForSecondsRealtime(0.5f);

        progress = 0.4f;        
        CreatePrefab();
        yield return new WaitForSecondsRealtime(0.5f);

        progress = 0.6f;
        yield return new WaitForSecondsRealtime(0.5f);

        progress = 1f;
        GameManager.Sound.PlaySound("Audios/MainMenu/MainBGM", Audio.BGM, 0.4f, 0.7f);
        yield return new WaitForSecondsRealtime(0.1f);
    }

    private void CreatePrefab()
    {
        clearPaper = GameManager.Resource.Instantiate<GameObject>("Prefabs/InfiltrationScene/ClearPaper");
        clearPaper.transform.position = itemSpawnPoint.position;
        clearPaper.transform.rotation = itemSpawnPoint.rotation;

        for (int i = 0; i < enemySpawnPoints.Count; i++)
            patrolMans.Add(GameManager.Resource.Instantiate<GameObject>("Prefabs/InfiltrationScene/PatrolMan", enemySpawnPoints[i].position, enemySpawnPoints[i].rotation, true));
    }
}
