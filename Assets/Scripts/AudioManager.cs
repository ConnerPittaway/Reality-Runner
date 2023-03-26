using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Delete Later
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSound, sfxSounds;
    public List<AudioSource> musicSources;
    public AudioSource sfxSource;
    public AudioSource mainMenuTrack;
    public backgrounds.Worlds activeTrack;

    public float bgmVolume = 1;

    private void Awake()
    {
        //Create Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        //Load Clips
        musicSources[0].clip = Array.Find(musicSound, x => x.soundName == "Futuristic City").audioClip;
        musicSources[1].clip = Array.Find(musicSound, x => x.soundName == "Industrial City").audioClip;

        //Set Saved Volume
        EventManager_OnAudioChanged(GlobalSettingsManager.Instance.audioLevel);

        //Subscribe to Slider Changes
        EventManager.AudioChanged += EventManager_OnAudioChanged;

        //Main Menu Music
        if(SceneManager.GetSceneByName("MainMenu") == SceneManager.GetActiveScene())
        {
            mainMenuTrack.Play();
        }
        else
        {
            PlayMusic(backgrounds.Worlds.FUTURISTIC);
            activeTrack = backgrounds.Worlds.FUTURISTIC;
        }
    }

    private void EventManager_OnAudioChanged(float audioValue)
    {
        Debug.Log("Audio Volume: " + audioValue);
        bgmVolume = audioValue;
        foreach (AudioSource audioSource in musicSources)
        {
            audioSource.volume = bgmVolume;
        }
        mainMenuTrack.volume = bgmVolume;
    }

    public void StartGameAudio()
    {
        mainMenuTrack.Stop();
        PlayMusic(backgrounds.Worlds.FUTURISTIC);
        activeTrack = backgrounds.Worlds.FUTURISTIC;
    }

    public void ReturnToMainMenu()
    {
        StopSongs();
        mainMenuTrack.Play();
    }

    public void StopSongs()
    {
        foreach(AudioSource audioSource in musicSources)
        {
            audioSource.Stop();
        }
    }

    public void SwapSong(backgrounds.Worlds worldToSwapTo)
    {
        StartCoroutine(FadeTrack(worldToSwapTo, musicSources[(int)worldToSwapTo], musicSources[(int)activeTrack]));
    }

    private IEnumerator FadeTrack(backgrounds.Worlds world, AudioSource musicSourceToPlay, AudioSource musicSourceToPause)
    {
        float fadeTime = 0.75f;
        float timeElapsed = 0.0f;

        musicSourceToPlay.Play();
        while (timeElapsed < fadeTime)
        {
            musicSourceToPlay.volume = Mathf.Lerp(0, bgmVolume, timeElapsed / fadeTime);
            musicSourceToPause.volume = Mathf.Lerp(bgmVolume, 0, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        musicSourceToPlay.volume = bgmVolume;
        musicSourceToPause.volume = 0;
        musicSourceToPause.Pause();

        activeTrack = world;
    }


    public void PlayMusic(backgrounds.Worlds world)
    {
        musicSources[(int)world].Play();
        activeTrack = world;
    }

    public void RestartMusic()
    {
        StopSongs();
        musicSources[0].volume = bgmVolume;
        musicSources[0].Play();
        activeTrack = backgrounds.Worlds.FUTURISTIC;
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundName == name);

        if (s == null)
        {
            Debug.Log("No Sound Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.audioClip);
        }
    }
}
