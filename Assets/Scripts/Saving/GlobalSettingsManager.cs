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
            EventManager.AudioChanged += EventManager_OnAudioChanged;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void EventManager_OnAudioChanged(float audioValue)
    {
        Debug.Log("Settings Volume: " + audioValue);
        audioLevel = audioValue;
    }
}
