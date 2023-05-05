using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{
    public Slider musicAudioSilder, sfxAudioSlider;
    public GameObject muteButton, unmuteButton, frameButtonOn, frameButtonOff, languageScreen, audioScreen, usernameScreen;
    public bool bootedGameFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        musicAudioSilder.value = GlobalSettingsManager.Instance.audioLevelMusic;
        sfxAudioSlider.value = GlobalSettingsManager.Instance.audioLevelSFX;
        if (GlobalSettingsManager.Instance.GetMuted())
        {
            muteButton.SetActive(false);
            unmuteButton.SetActive(true);
        }

        if(!GlobalSettingsManager.Instance.GetFrameCounter())
        {
            frameButtonOn.SetActive(false);
            frameButtonOff.SetActive(true);
        }

        bootedGameFlag = false;
    }

    //Audio
    public void OpenAudio()
    {
        audioScreen.SetActive(true);
    }

    public void CloseAudio()
    {
        audioScreen.SetActive(false);
    }

    //Languages
    public void OpenLanguages()
    {
        languageScreen.SetActive(true);
    }

    //Privacy Statement
    public void OpenPrivacyStatement()
    {
        Application.OpenURL("http://unity3d.com/");
    }

    //Support
    public void ContactSupport()
    {
        Application.OpenURL("http://unity3d.com/");
    }

    //Audio Sliders
    public void MusicSliderVolumeChanged()
    {
        if(!bootedGameFlag)
        {
            muteButton.SetActive(true);
            unmuteButton.SetActive(false);
            GlobalSettingsManager.Instance.SetMuted(false);
            EventManager.OnMusicAudioChanged(musicAudioSilder.value);
        }
    }

    public void SFXSliderVolumeChanged()
    {
        if (!bootedGameFlag)
        {
            muteButton.SetActive(true);
            unmuteButton.SetActive(false);
            GlobalSettingsManager.Instance.SetMuted(false);
            EventManager.OnSFXAudioChanged(sfxAudioSlider.value);
        }
    }

    //Framerate Counter
    public void TurnFrameCounterOn()
    {
        //Save Frame Setting
        GlobalSettingsManager.Instance.SetFrameCounter(true);
        frameButtonOff.SetActive(false);
        frameButtonOn.SetActive(true);
        GlobalSettingsManager.Instance.SaveSettingsData();

        //Push Event (for game)
        EventManager.OnFrameShowChanged(true);
    }

    public void TurnFrameCounterOff()
    {
        //Save Frame Setting
        GlobalSettingsManager.Instance.SetFrameCounter(false);
        frameButtonOn.SetActive(false);
        frameButtonOff.SetActive(true);
        GlobalSettingsManager.Instance.SaveSettingsData();

        //Push Event (for game)
        EventManager.OnFrameShowChanged(false);
    }

    //Mute and UnMute Buttons
    public void MuteAll()
    {
        GlobalSettingsManager.Instance.preMuteLevelMusic = musicAudioSilder.value;
        GlobalSettingsManager.Instance.preMuteLevelSFX = sfxAudioSlider.value;
        musicAudioSilder.value = 0;
        sfxAudioSlider.value = 0;
        muteButton.SetActive(false);
        unmuteButton.SetActive(true);
        GlobalSettingsManager.Instance.SetMuted(true);
        EventManager.OnMusicAudioChanged(0);
        EventManager.OnSFXAudioChanged(0);
    }

    public void UnMuteAll()
    {
        GlobalSettingsManager.Instance.audioLevelMusic = GlobalSettingsManager.Instance.preMuteLevelMusic;
        GlobalSettingsManager.Instance.audioLevelSFX = GlobalSettingsManager.Instance.preMuteLevelSFX;
        musicAudioSilder.value = GlobalSettingsManager.Instance.preMuteLevelMusic;
        sfxAudioSlider.value = GlobalSettingsManager.Instance.preMuteLevelSFX;
        muteButton.SetActive(true);
        unmuteButton.SetActive(false);
        GlobalSettingsManager.Instance.SetMuted(false);
        EventManager.OnMusicAudioChanged(GlobalSettingsManager.Instance.preMuteLevelMusic);
        EventManager.OnSFXAudioChanged(GlobalSettingsManager.Instance.preMuteLevelSFX);
    }

    //Cloud Data
    public void LoadCloudData()
    {
        GlobalDataManager.Instance.LoadCloudData();
        GlobalStatsData.Instance.LoadCloudData();
    }

    //Leaderboard Name
    public void OpenUsername()
    {
        usernameScreen.SetActive(true);
    }

    void OnEnable()
    {
        EventManager.OnUIElementOpened();
    }

    void OnDisable()
    {
        usernameScreen.SetActive(false);
        languageScreen.SetActive(false);
        audioScreen.SetActive(false);
    }
}
