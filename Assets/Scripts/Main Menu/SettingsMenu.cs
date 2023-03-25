using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider audioSilder;
    public GameObject muteButton, unmuteButton;

    // Start is called before the first frame update
    void Start()
    {
        audioSilder.value = GlobalSettingsManager.Instance.audioLevel;
        if(GlobalSettingsManager.Instance.GetMuted())
        {
            muteButton.SetActive(false);
            unmuteButton.SetActive(true);
        }
    }

    //Push Slider Value To Global and Audio Manager
    public void SliderVolumeChanged()
    {
        muteButton.SetActive(true);
        unmuteButton.SetActive(false);
        GlobalSettingsManager.Instance.SetMuted(false);
        EventManager.OnAudioChanged(audioSilder.value);
    }

    public void MuteAll()
    {
        GlobalSettingsManager.Instance.preMuteLevel = audioSilder.value;
        audioSilder.value = 0;
        muteButton.SetActive(false);
        unmuteButton.SetActive(true);
        GlobalSettingsManager.Instance.SetMuted(true);
        EventManager.OnAudioChanged(0);
    }

    public void UnMuteAll()
    {
        GlobalSettingsManager.Instance.audioLevel = GlobalSettingsManager.Instance.preMuteLevel;
        audioSilder.value = GlobalSettingsManager.Instance.preMuteLevel;
        muteButton.SetActive(true);
        unmuteButton.SetActive(false);
        GlobalSettingsManager.Instance.SetMuted(false);
        EventManager.OnAudioChanged(GlobalSettingsManager.Instance.preMuteLevel);
    }

    void OnEnable()
    {
        EventManager.OnUIElementOpened();
    }
}
