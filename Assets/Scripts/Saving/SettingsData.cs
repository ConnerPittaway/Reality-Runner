using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    //Data
    public float audioLevel = 1;
    public float preMuteLevel = 1;
    public bool isMuted = false;

    //Retrieve Data
    public SettingsData()
    {
        audioLevel = GlobalSettingsManager.Instance.GetAudio();
        preMuteLevel = GlobalSettingsManager.Instance.preMuteLevel;
        isMuted = GlobalSettingsManager.Instance.GetMuted();
    }
}
