using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiltrationScene : BaseScene
{
    public Transform itemSpawnPoint;

    GameObject hangar;
    GameObject clearPaper;

    private void Awake()
    {

    }

    protected override IEnumerator LoadingRoutine()
    {
        progress = 0f;
        GameManager.Sound.PlaySound("Audios/MainMenu/MainBGM", Audio.BGM, 1f, 0.9f);
        GameManager.Sound.PlaySound("Audios/MainMenu/RainSound", Audio.SFX);
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.2f;
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
        hangar = GameManager.Resource.Instantiate<GameObject>("Prefabs/InfiltrationScene/Hangar");
        hangar.transform.position = new Vector3(0, 0, 0);

        clearPaper = GameManager.Resource.Instantiate<GameObject>("Prefabs/InfiltrationScene/ClearPaper");
        clearPaper.transform.position = itemSpawnPoint.position;
        clearPaper.transform.rotation = itemSpawnPoint.rotation;
    }
}
