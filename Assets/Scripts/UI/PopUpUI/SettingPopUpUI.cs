using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingPopUpUI : PopUpUI
{
    public AudioMixer audioMixer;

    public TMP_Text masterVolumeText;
    public TMP_Text bgmVolumeText;
    public TMP_Text sfxVolumeText;
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    protected override void Awake()
    {
        base.Awake();

        texts["MasterVolumeText"].text = "100";
        texts["BGMVolumeText"].text = "100";
        texts["SFXVolumeText"].text = "100";
    }

    public void SetMasterVolume()
    {
        float masterVolume = Mathf.Log10(masterSlider.value) * 20;

        audioMixer.SetFloat("Master", masterVolume);
    }

    public void SetBGMVolume()
    {
        float bgmVolume = Mathf.Log10(bgmSlider.value) * 20;

        audioMixer.SetFloat("BGM", bgmVolume);
    }

    public void SetSFXVolume()
    {
        float sfxVolume = Mathf.Log10(sfxSlider.value) * 20;

        audioMixer.SetFloat("SFX", sfxVolume);
    }
}
