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
    public float audioLevel = 1;
    public float preMuteLevel = 1;

    private bool isMuted = false;

    private void Awake()
    {
        //Create Singleton
        if (Instance == null)
        {
            Instance = this;

            //Load Settings Data
            this.settingsDataHandler = new JsonDataHandler(Application.persistentDataPath, "SettingsData");
            SettingsData settingsData = settingsDataHandler.LoadSettingsData();

            if (settingsData == null)
            {
                settingsData = new SettingsData();
            }

            audioLevel = settingsData.audioLevel;
            preMuteLevel = settingsData.preMuteLevel;
            isMuted = settingsData.isMuted;

            EventManager.AudioChanged += EventManager_OnAudioChanged;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveSettingsData()
    {
        settingsDataHandler.SaveSettingsData();
    }

    private void EventManager_OnAudioChanged(float audioValue)
    {
        Debug.Log("Settings Volume: " + audioValue);
        audioLevel = audioValue;
        SaveSettingsData();
    }

    public float GetAudio()
    {
        return audioLevel;
    }

    public void SetMuted(bool muted)
    {
        isMuted = muted;
    }

    public bool GetMuted()
    {
        return isMuted;
    }
}
