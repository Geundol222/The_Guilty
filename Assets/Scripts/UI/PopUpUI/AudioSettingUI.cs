using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingUI : PopUpUI
{
    public AudioMixer audioMixer;

    public TMP_Text masterVolumeText;
    public TMP_Text bgmVolumeText;
    public TMP_Text sfxVolumeText;
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    public Sprite empty;
    public Sprite full;
    public Image masterVolumeImage;
    public Image bgmVolumeImage;
    public Image sfxVolumeImage;

    protected override void Awake()
    {
        base.Awake();

        texts["MasterVolumeText"].text = "100";
        texts["BGMVolumeText"].text = "100";
        texts["SFXVolumeText"].text = "100";
        buttons["ExitButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI<AudioSettingUI>(); });
    }

    public void SetMasterVolume()
    {
        float masterVolume = Mathf.Log10(masterSlider.value) * 20;

        audioMixer.SetFloat("Master", masterVolume);

        if (masterVolume <= -80)
        {
            masterVolumeImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 25);
            masterVolumeImage.sprite = empty;
        }
        else
        {
            masterVolumeImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 40);
            masterVolumeImage.sprite = full;
        }
    }

    public void SetBGMVolume()
    {
        float bgmVolume = Mathf.Log10(bgmSlider.value) * 20;

        audioMixer.SetFloat("BGM", bgmVolume);

        if (bgmVolume <= -80)
        {
            bgmVolumeImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 25);
            bgmVolumeImage.sprite = empty;
        }
        else
        {
            bgmVolumeImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 40);
            bgmVolumeImage.sprite = full;
        }
           
    }

    public void SetSFXVolume()
    {
        float sfxVolume = Mathf.Log10(sfxSlider.value) * 20;

        audioMixer.SetFloat("SFX", sfxVolume);

        if (sfxVolume <= -80)
        {
            sfxVolumeImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 25);
            sfxVolumeImage.sprite = empty;
        }
        else
        {
            sfxVolumeImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 40);
            sfxVolumeImage.sprite = full;
        }
    }
}
