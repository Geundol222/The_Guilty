using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();

        buttons["ContinueButton"].onClick.AddListener(() => { Time.timeScale = 1f; GameManager.Scene.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex); });
        buttons["MainMenuButton"].onClick.AddListener(() => { Time.timeScale = 1f; GameManager.Scene.LoadScene(0); });
    }
}
