using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettingsManager : MonoBehaviour
{
    //Instance
    public static GlobalSettingsManager Instance;

    //Data Handler
    private JsonDataHandler settingsDataHandler;

    //Settings Data
    //Audio
    public float audioLevelMusic = 1;
    public float preMuteLevelMusic = 1;
    public float audioLevelSFX = 1;
    public float preMuteLevelSFX = 1;
    private bool isMuted = false;

    //Frame Counter
    private bool frameCountOn = true;

    //Language
    //Enum Languages
    public enum Languages
    {
        ENGLISHUK,
        ENGLISHUS,
        ENGLISHAU,
        ENGLISHNZ,
        ENGLISHCA
    }
    public Languages currentlySelectedLanguage;

    //Terms
    public bool acceptedTerms = false;

    private void Awake()
    {
        //Create Singleton
        if (Instance == null)
        {
            Instance = this;

            //Load Settings Data
            this.settingsDataHandler = new JsonDataHandler(Application.persistentDataPath, "SettingsData");
            SettingsData settingsData = settingsDataHandler.LoadData<SettingsData>();

            if (settingsData == null)
            {
                settingsData = new SettingsData();
            }

            audioLevelMusic = settingsData.audioLevelMusic;
            preMuteLevelMusic = settingsData.preMuteLevelMusic;
            audioLevelSFX = settingsData.audioLevelSFX;
            preMuteLevelSFX = settingsData.preMuteLevelSFX;
            isMuted = settingsData.isMuted;
            currentlySelectedLanguage = settingsData.currentlySelectedLanguage;
            frameCountOn = settingsData.frameCounterOn;
            acceptedTerms = settingsData.acceptedTerms;

            EventManager.MusicAudioChanged += EventManager_OnMusicAudioChanged;
            EventManager.SFXAudioChanged += EventManager_OnSFXAudioChanged;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveSettingsData()
    {
        settingsDataHandler.SaveData<SettingsData>();
    }

    private void EventManager_OnMusicAudioChanged(float audioValue)
    {
        //Debug.Log("Settings Volume: " + audioValue);
        audioLevelMusic = audioValue;
        SaveSettingsData();
    }

    private void EventManager_OnSFXAudioChanged(float audioValue)
    {
        //Debug.Log("SFX Settings Volume: " + audioValue);
        audioLevelSFX = audioValue;
        SaveSettingsData();
    }

    //Audio Volume
    public float GetMusicAudio()
    {
        return audioLevelMusic;
    }

    public float GetSFXAudio()
    {
        return audioLevelSFX;
    }

    //Audio Muted
    public void SetMuted(bool muted)
    {
        isMuted = muted;
    }

    public bool GetMuted()
    {
        return isMuted;
    }

    //Frame Counter
    public void SetFrameCounter(bool frameCounter)
    {
        frameCountOn = frameCounter;
    }

    public bool GetFrameCounter()
    {
        return frameCountOn;
    }
}
