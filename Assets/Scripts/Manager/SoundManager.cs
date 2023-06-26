using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum Audio { BGM, SFX, Size }

public class SoundManager : MonoBehaviour
{
    AudioSource bgmSource;
    List<AudioSource> sfxSources;
    Dictionary<string, AudioClip> audioDic;
    private float delay = 1f;
    bool isMuted = false;

    private void Awake()
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

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / delay);
            if (AudioListener.volume <= 0f)
            {
                isMuted = true;
                yield break;
            }                
            yield return null;
        }
    }

    public void FadeInAudio()
    {
        AudioListener.volume = 0f;
        StartCoroutine(FadeInRoutine());
    }

    IEnumerator FadeInRoutine()
    {
        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 1, elapsedTime / delay);
            if (AudioListener.volume <= 1f)
            {
                isMuted = false;
                yield break;
            }
            yield return null;
        }
    }

    public void PlaySound(AudioClip audioClip, Audio type = Audio.SFX, float volume = 1.0f, float pitch = 1.0f, bool loop = true)
    {
        StopAllCoroutines();
        isMuted = false;

        if (audioClip == null)
            return;

        if (type == Audio.BGM)
        {
            bgmSource = GameManager.Resource.Instantiate<AudioSource>("Prefabs/BGM");
            if (bgmSource.isPlaying)
                bgmSource.Stop();

            bgmSource.transform.parent = transform;
            bgmSource.volume = volume;
            bgmSource.pitch = pitch;
            bgmSource.clip = audioClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
        else
        {
            AudioSource audioSource = GameManager.Resource.Instantiate<AudioSource>("Prefabs/SFX", true);
            audioSource.transform.parent = transform;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.loop = loop;
            sfxSources.Add(audioSource);

            if (loop)
                audioSource.Play();
            else
                audioSource.PlayOneShot(audioClip);
        }
    }

    public void PlaySound(string path, Audio type = Audio.SFX, float volume = 1.0f, float pitch = 1.0f, bool loop = true)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        PlaySound(audioClip, type, volume, pitch, loop);
    }

    public AudioClip GetOrAddAudioClip(string path, Audio type = Audio.SFX)
    {
        if (path.Contains("Audios/") == false)
            path = $"AUdios/{path}";

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
