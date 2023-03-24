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
    private int audioLevel = 1;


    private void Awake()
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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
