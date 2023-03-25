using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStatsData : MonoBehaviour
{
    //Instance
    public static GlobalStatsData Instance;

    //Data Handler
    private JsonDataHandler statsDataHandler;

    private void Awake()
    {
        //Create Singleton
        if (Instance == null)
        {
            Instance = this;

            //Load Settings Data
            this.statsDataHandler = new JsonDataHandler(Application.persistentDataPath, "SettingsData");
            //SettingsData settingsData = statsDataHandler.LoadData<SettingsData>();

            //if (settingsData == null)
            //{
            //    settingsData = new SettingsData();
           // }

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
