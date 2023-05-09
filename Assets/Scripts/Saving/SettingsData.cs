using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    //Data
    public float audioLevelMusic = 1;
    public float preMuteLevelMusic = 1;
    public float audioLevelSFX = 1;
    public float preMuteLevelSFX = 1;
    public bool isMuted = false;
    public GlobalSettingsManager.Languages currentlySelectedLanguage;
    public bool frameCounterOn = true;
    public bool acceptedTerms = false;

    //Retrieve Data
    public SettingsData()
    {
        audioLevelMusic = GlobalSettingsManager.Instance.GetMusicAudio();
        preMuteLevelMusic = GlobalSettingsManager.Instance.preMuteLevelMusic;
        audioLevelSFX = GlobalSettingsManager.Instance.GetSFXAudio();
        preMuteLevelSFX = GlobalSettingsManager.Instance.preMuteLevelSFX;
        isMuted = GlobalSettingsManager.Instance.GetMuted();
        currentlySelectedLanguage = GlobalSettingsManager.Instance.currentlySelectedLanguage;
        frameCounterOn = GlobalSettingsManager.Instance.GetFrameCounter();
        acceptedTerms = GlobalSettingsManager.Instance.acceptedTerms;
    }
}
