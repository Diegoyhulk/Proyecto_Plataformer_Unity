using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private  AudioSource audioSource;
    [SerializeField] private float fadeTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ChangeMusic(AudioClip newClip)
    {
        StartCoroutine(FadeMusic(newClip));
    }

    public void EndMusicclip()
    {
        StartCoroutine(EndMusic());
    }

    public void StartMusicclip(float volumen, AudioClip newClip)
    {
        StartCoroutine(StartMusic(newClip, volumen));
    }

    private IEnumerator FadeMusic(AudioClip newClip)
    {
        float startVolume = audioSource.volume;

        // Fade out
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeTime);
            yield return null;
        }

        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, startVolume, t / fadeTime);
            yield return null;
        }

        audioSource.volume = startVolume;
    }
    private IEnumerator EndMusic()
    {
        float startVolume = audioSource.volume;

        // Fade out
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeTime);
            yield return null;
        }
    }
    private IEnumerator StartMusic(AudioClip newClip, float endvolume)
    {
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, endvolume, t / fadeTime);
            yield return null;
        }
        audioSource.volume = endvolume;
    }
}
