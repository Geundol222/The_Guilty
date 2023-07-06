using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSettingUI : PopUpUI
{
    FullScreenMode screenMode;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle fullScreenButton;

    List<Resolution> resolutions = new List<Resolution>();
    int resolutionIndex;
    protected override void Awake()
    {
        base.Awake();

        InitUI();

        buttons["ConfirmButton"].onClick.AddListener(ConfirmResolution);
        buttons["ExitButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI<ResolutionSettingUI>(); });
    }

    private void InitUI()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 60)
                resolutions.Add(Screen.resolutions[i]);
        }

        resolutionDropdown.options.Clear();

        int optionIndex = 0;
        foreach(Resolution resolution in resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = resolution.width + "x" + resolution.height + " " + resolution.refreshRate + "hz";
            resolutionDropdown.options.Add(option);

            if (resolution.width == Screen.width && resolution.height == Screen.height)
                resolutionDropdown.value = optionIndex;
            optionIndex++;
        }
        resolutionDropdown.RefreshShownValue();

        fullScreenButton.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropboxOptionChange(int x)
    {
        resolutionIndex = x;
    }

    public void FullScreenButton(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void ConfirmResolution()
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, screenMode);
    }
}
