using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSound, sfxSounds;
    public AudioSource musicSource1, musicSource2, sfxSource;
    public bool isPlayingMusicSource1;

    public backgrounds.Worlds activeTrack;

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
        //musicSource1.clip = Array.Find(musicSound, x => x.soundName == "City Track").audioClip;
        PlayMusic(backgrounds.Worlds.FUTURISTIC);
        activeTrack = backgrounds.Worlds.FUTURISTIC;
        isPlayingMusicSource1 = true;
    }

    public void StopSongs()
    {
        musicSource1.Stop();
        musicSource2.Stop();
        isPlayingMusicSource1 = false;
    }

    public void SwapSong(backgrounds.Worlds world)
    {
        string songName = "";

        switch(world)
        {
            case backgrounds.Worlds.INDUSTRIAL:
                songName = "City Track";
                break;
            case backgrounds.Worlds.FUTURISTIC:
                songName = "Forest Track";
                break;
        }

        Sound s = Array.Find(musicSound, x => x.soundName == songName);
        if (s == null)
        {
            Debug.Log("No Sound Found");
        }
        else
        {
            StartCoroutine(FadeTrack(s.audioClip, world));
            isPlayingMusicSource1 = !isPlayingMusicSource1;
        }
    }

    private IEnumerator FadeTrack(AudioClip newSong, backgrounds.Worlds world)
    {
        float fadeTime = 0.75f;
        float timeElapsed = 0.0f;

        if (isPlayingMusicSource1)
        {

            musicSource2.clip = newSong;
            musicSource2.Play();

            while(timeElapsed < fadeTime)
            {
                musicSource2.volume = Mathf.Lerp(0, 1, timeElapsed / fadeTime);
                musicSource1.volume = Mathf.Lerp(1, 0, timeElapsed / fadeTime);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            musicSource1.Pause();
        }
        else
        {
            musicSource1.clip = newSong;
            musicSource1.Play();

            while (timeElapsed < fadeTime)
            {
                musicSource1.volume = Mathf.Lerp(0, 1, timeElapsed / fadeTime);
                musicSource2.volume = Mathf.Lerp(1, 0, timeElapsed / fadeTime);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
           musicSource2.Pause();
        }
        activeTrack = world;
    }


    public void PlayMusic(backgrounds.Worlds world)
    {
        string songName = "";

        switch (world)
        {
            case backgrounds.Worlds.INDUSTRIAL:
                songName = "City Track";
                break;
            case backgrounds.Worlds.FUTURISTIC:
                songName = "Forest Track";
                break;
        }

        Sound s = Array.Find(musicSound, x => x.soundName == songName);

        if(s == null)
        {
            Debug.Log("No Sound Found");
        }
        else
        {
            musicSource1.clip = s.audioClip;
            musicSource1.Play();
        }
    }

    public void RestartMusic()
    {
        musicSource1.Stop();
        musicSource2.Stop();

        if(isPlayingMusicSource1)
        {
            PlayMusic(backgrounds.Worlds.FUTURISTIC);
        }
        else
        {
            SwapSong(backgrounds.Worlds.FUTURISTIC);
        }
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
