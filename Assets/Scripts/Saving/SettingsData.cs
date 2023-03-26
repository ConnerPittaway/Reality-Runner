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
    public GlobalSettingsManager.Languages currentlySelectedLanguage;
    public bool frameCounterOn = true;

    //Retrieve Data
    public SettingsData()
    {
        audioLevel = GlobalSettingsManager.Instance.GetAudio();
        preMuteLevel = GlobalSettingsManager.Instance.preMuteLevel;
        isMuted = GlobalSettingsManager.Instance.GetMuted();
        currentlySelectedLanguage = GlobalSettingsManager.Instance.currentlySelectedLanguage;
        frameCounterOn = GlobalSettingsManager.Instance.GetFrameCounter();
    }
}
