using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum Audio { BGM, SFX, Size }

public class SoundManager : MonoBehaviour
{
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

        audioSources[(int)Audio.BGM].loop = true;
    }

    public void Clear()
    {
        foreach(AudioSource audioSource in audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        audioDic.Clear();
    }

    public void PlaySound(AudioClip audioClip, Audio type = Audio.SFX, float pitch = 1.0f)
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
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = audioSources[(int)Audio.SFX];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void PlaySound(string path, Audio type = Audio.SFX, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        PlaySound(audioClip, type, pitch);
    }

    private AudioClip GetOrAddAudioClip(string path, Audio type = Audio.SFX)
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
