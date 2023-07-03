using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public enum Audio { BGM, SFX, Size }

public class SoundManager : MonoBehaviour
{
    GameObject bgmObj;
    AudioSource bgmSource;
    GameObject addObj;
    AudioSource addSource;
    List<AudioSource> sfxSources;
    Dictionary<string, AudioClip> audioDic;
    bool isMuted = false;

    private void Awake()
    {
        InitSound();
    }

    public void InitSound()
    {
        sfxSources = new List<AudioSource>();
        audioDic = new Dictionary<string, AudioClip>();
    }

    public bool Clear()
    {
        StartCoroutine(ClearRoutine());

        sfxSources.Clear();
        audioDic.Clear();

        return isMuted;
    }

    IEnumerator ClearRoutine()
    {
        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / 1f);
            if (AudioListener.volume <= 0f)
            {
                GameManager.Resource.Destroy(bgmObj);
                GameManager.Resource.Destroy(addObj);
                isMuted = true;
                yield break;
            }
            yield return null;
        }
    }

    public bool FadeInAudio()
    {
        AudioListener.volume = 0f;
        StartCoroutine(FadeInRoutine());

        return isMuted;
    }

    IEnumerator FadeInRoutine()
    {
        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 1f, elapsedTime / 1f);
            if (AudioListener.volume >= 1f)
            {
                isMuted = false;
                yield break;
            }
            yield return null;
        }
    }

    public void PlaySound(AudioClip audioClip, Audio type = Audio.SFX, float volume = 1.0f, float pitch = 1.0f, bool loop = false)
    {
        StopAllCoroutines();

        if (audioClip == null)
            return;

        if (type == Audio.BGM)
        {
            bgmObj = GameManager.Resource.Instantiate<GameObject>("Prefabs/BGM");
            bgmObj.transform.parent = transform;
            bgmSource = bgmObj.GetComponent<AudioSource>();
            if (bgmSource.isPlaying)
                bgmSource.Stop();

            bgmSource.volume = volume;
            bgmSource.pitch = pitch;
            bgmSource.clip = audioClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
        else
        {
            addObj = GameManager.Resource.Instantiate<GameObject>("Prefabs/SFX", true);
            addObj.transform.parent = transform;

            addSource = addObj.GetComponent<AudioSource>();

            addSource.transform.parent = transform;
            addSource.volume = volume;
            addSource.pitch = pitch;
            addSource.clip = audioClip;
            addSource.loop = loop;
            sfxSources.Add(addSource);

            if (loop)
                addSource.Play();
            else
            {
                addSource.PlayOneShot(audioClip);

                if (!addSource.isPlaying)
                    GameManager.Resource.Destroy(addObj);
            }
        }
    }

    public void PlaySound(string path, Audio type = Audio.SFX, float volume = 1.0f, float pitch = 1.0f, bool loop = false)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        PlaySound(audioClip, type, volume, pitch, loop);
    }

    public AudioClip GetOrAddAudioClip(string path, Audio type = Audio.SFX)
    {
        if (path.Contains("Audios/") == false)
            path = $"Audios/{path}";

        AudioClip audioClip = null;

        if (type == Audio.BGM)
        {
            audioClip = GameManager.Resource.Load<AudioClip>(path);
        }
        else
        {
            if (audioDic.TryGetValue(path, out audioClip) == false)
            {
                audioClip = GameManager.Resource.Load<AudioClip>(path);
                audioDic.Add(path, audioClip);
            }
        }

        return audioClip;
    }
}
