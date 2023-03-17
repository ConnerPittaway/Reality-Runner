using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using URPGlitch.Runtime.AnalogGlitch;

public class GlitchManager : MonoBehaviour
{
    public Volume glitchVolume;
    private AnalogGlitchVolume glitch;
    // Start is called before the first frame update
    void Start()
    {
        if(glitchVolume.profile.TryGet<AnalogGlitchVolume>(out glitch))
        {
            Debug.Log("Success");
            EventManager.UIElementOpened += EventManager_UIElementOpened;
            //glitch.scanLineJitter.value = 1;
        }
    }

    private void EventManager_UIElementOpened()
    {
        StopAllCoroutines(); //Ensure one play at a time
        StartCoroutine(Glitch(glitch));
    }

    private IEnumerator Glitch(AnalogGlitchVolume glitch)
    {
        yield return StartCoroutine(GlitchIn(100f, glitch));
        yield return StartCoroutine(GlitchOut(2f, glitch));
    }

    private IEnumerator GlitchIn(float timeSpeed, AnalogGlitchVolume glitch)
    {
        while (glitch.scanLineJitter.value < 1.0f)
        {
            glitch.scanLineJitter.value += (Time.deltaTime * timeSpeed);
            glitch.colorDrift.value += (Time.deltaTime * timeSpeed);
            yield return null;
        }
    }

    private IEnumerator GlitchOut(float timeSpeed, AnalogGlitchVolume glitch)
    {
        while (glitch.scanLineJitter.value > 0.005f)
        {
            glitch.scanLineJitter.value -= (Time.deltaTime * timeSpeed);
            glitch.colorDrift.value -= (Time.deltaTime * timeSpeed);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        EventManager.UIElementOpened -= EventManager_UIElementOpened;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
