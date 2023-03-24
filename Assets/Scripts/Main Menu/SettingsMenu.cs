using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider audioSilder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAudioVolumeChanged()
    {
        EventManager.OnAudioChanged(audioSilder.value);
    }

    void OnEnable()
    {
        EventManager.OnUIElementOpened();
    }
}
