using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum Audio { BGM, SFX, Size }

public class SoundManager : MonoBehaviour
{
    AudioMixer audioMixer;
    AudioSource[] audioSources;
    Dictionary<string, AudioClip> audioDic;

    private void Awake()
    {
        audioSources = new AudioSource[(int)Audio.Size];
        audioDic = new Dictionary<string, AudioClip>();

        InitAudioSource();
    }

    public void InitAudioSource()
    {
        string[] soundNames = Enum.GetNames(typeof(Audio));
        for (int i = 0; i < soundNames.Length - 1; i++)
        {
            GameObject obj = new GameObject();
            obj.name = soundNames[i];
            obj.transform.parent = transform;
            audioSources[i] = obj.AddComponent<AudioSource>();
        }
    }

    public void Clear()
    {
        StartCoroutine(ClearRoutine());

        audioDic.Clear();
    }

    IEnumerator ClearRoutine()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            while (true)
            {
                audioSource.volume = Mathf.Lerp(1f, 0f, Time.deltaTime * 10f);

                if (audioSource.volume <= 0f)
                {
                    audioSource.clip = null;
                    audioSource.Stop();
                }

                yield return null;
            }
        }
        yield break;
    }

    public void PlaySound(AudioClip audioClip, Audio type = Audio.SFX, float pitch = 1.0f, bool loop = true)
    {
        if (audioClip == null)
            return;

        if (type == Audio.BGM)
        {
            AudioSource audioSource = audioSources[(int)Audio.BGM];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.loop = loop;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = audioSources[(int)Audio.SFX];
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.loop = loop;

            if (loop)
                audioSource.Play();
            else
                audioSource.PlayOneShot(audioClip);
        }
    }

    public void PlaySound(string path, Audio type = Audio.SFX, float pitch = 1.0f, bool loop = true)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        PlaySound(audioClip, type, pitch, loop);
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
