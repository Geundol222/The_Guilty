using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScene : BaseScene
{
    public List<Transform> itemSpawnPoints;
    [SerializeField] RoomSceneUI roomUI;

    private void Awake()
    {

    }

    protected override IEnumerator LoadingRoutine()
    {
        progress = 0f;
        roomUI.Init();
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.2f;
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.4f;
        CreatePrefab();
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.6f;
        yield return new WaitForSecondsRealtime(1f);

        progress = 1f;
        GameManager.Sound.PlaySound("Audios/RoomScene/RoomBGM", Audio.BGM, 0.3f);
        yield return new WaitForSecondsRealtime(0.1f);
    }

    private void CreatePrefab()
    {

    }
}