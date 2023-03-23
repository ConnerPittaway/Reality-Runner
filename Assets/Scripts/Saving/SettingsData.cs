using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    //Data
    public int audioLevel = 0;

    //Retrieve Data
    public SettingsData()
    {
        audioLevel = GlobalDataManager.Instance.GetAudio();
    }
}
