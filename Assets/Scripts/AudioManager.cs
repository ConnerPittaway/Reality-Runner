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
    public SerializableDictionary<backgrounds.Worlds, AudioSource> musicSources;
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
        musicSources[backgrounds.Worlds.FUTURISTIC].clip = Array.Find(musicSound, x => x.soundName == "Futuristic City").audioClip;
        musicSources[backgrounds.Worlds.SPACE].clip = Array.Find(musicSound, x => x.soundName == "Space").audioClip;
        musicSources[backgrounds.Worlds.HEART].clip = Array.Find(musicSound, x => x.soundName == "Heart").audioClip;
        musicSources[backgrounds.Worlds.HELL].clip = Array.Find(musicSound, x => x.soundName == "Hell").audioClip;

        //Set Saved Volume
        EventManager_OnMusicAudioChanged(GlobalSettingsManager.Instance.audioLevelMusic);
        EventManager_OnSFXAudioChanged(GlobalSettingsManager.Instance.audioLevelSFX);

        //Subscribe to Slider Changes
        EventManager.MusicAudioChanged += EventManager_OnMusicAudioChanged;
        EventManager.SFXAudioChanged += EventManager_OnSFXAudioChanged;

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

    private void EventManager_OnMusicAudioChanged(float audioValue)
    {
        //Debug.Log("Audio Volume: " + audioValue);
        bgmVolume = audioValue;
        foreach (var audioSource in musicSources)
        {
            audioSource.Value.volume = bgmVolume;
        }
        mainMenuTrack.volume = bgmVolume;
    }

    private void EventManager_OnSFXAudioChanged(float audioValue)
    {
        //Debug.Log("SFX Audio Volume: " + audioValue);
        sfxSource.volume = audioValue;
    }

    public void StartGameAudio()
    {
        mainMenuTrack.Stop();
        RestartMusic();
    }

    public void ReturnToMainMenu()
    {
        StopSongs();
        mainMenuTrack.Play();
    }

    public void StopSongs()
    {
        foreach(var audioSource in musicSources)
        {
            audioSource.Value.Stop();
        }
    }

    public void SwapSong(backgrounds.Worlds worldToSwapTo)
    {
        StartCoroutine(FadeTrack(worldToSwapTo, musicSources[worldToSwapTo], musicSources[activeTrack]));
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
        musicSources[world].Play();
        activeTrack = world;
    }

    public void RestartMusic()
    {
        StopSongs();
        musicSources[backgrounds.Worlds.FUTURISTIC].volume = bgmVolume;
        musicSources[backgrounds.Worlds.FUTURISTIC].Play();
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
