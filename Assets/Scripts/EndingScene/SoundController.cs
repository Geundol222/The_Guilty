using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private float lerpTime = 0f;
    private float duration = 2f;
    private GameObject peopleSound;
    private AudioSource peopleSource;
    private GameObject streetSound;
    private AudioSource streetSource;
    private GameObject endingBGM;
    private AudioSource endingSource;

    private void Awake()
    {
        GameManager.Sound.PlaySound("Audios/EndingScene/PeopleSound", Audio.BGM);
        GameManager.Sound.PlaySound("Audios/EndingScene/StreetNoise", Audio.BGM, 0.5f);
        GameManager.Sound.PlaySound("Audios/EndingScene/EndingSceneBGM", Audio.BGM, 0f);

        peopleSound = GameObject.Find("PeopleSound");
        peopleSource = peopleSound.GetComponent<AudioSource>();
        streetSound = GameObject.Find("StreetNoise");
        streetSource = streetSound.GetComponent<AudioSource>();
        endingBGM = GameObject.Find("EndingSceneBGM");
        endingSource = endingBGM.GetComponent<AudioSource>();
    }

    private void Start()
    {
        endingSource.Pause();
        StartCoroutine(BGMRoutine());
    }

    IEnumerator BGMRoutine()
    {
        yield return new WaitForSeconds(33f);

        endingSource.Play();
        endingSource.volume = 0.2f;

        while (lerpTime < duration)
        {
            lerpTime += Time.deltaTime * 0.01f;
            peopleSource.volume = Mathf.Lerp(peopleSource.volume, 0f, lerpTime / duration);
            streetSource.volume = Mathf.Lerp(peopleSource.volume, 0f, lerpTime / duration);
            endingSource.volume = Mathf.Lerp(endingSource.volume, 0.8f, lerpTime / duration);

            if (peopleSource.volume < 0.01f && streetSource.volume < 0.01f)
            {
                GameManager.Resource.Destroy(peopleSound);
                GameManager.Resource.Destroy(streetSound);
                break;
            }
            yield return null;
        }

        lerpTime = 0f;

        while (lerpTime < duration)
        {
            lerpTime += Time.deltaTime * 0.01f;
            endingSource.volume = Mathf.Lerp(endingSource.volume, 0.5f, lerpTime / duration);

            if (endingSource.volume >= 0.5f)
                break;

            yield return null;
        }

        yield break;
    }
}
