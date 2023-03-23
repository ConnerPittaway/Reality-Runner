using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using URPGlitch.Runtime.AnalogGlitch;
using URPGlitch.Runtime.DigitalGlitch;

public class GameGlitchManager : MonoBehaviour
{
    public Volume glitchVolume;
    private AnalogGlitchVolume glitch;
    private DigitalGlitchVolume digitalGlitch;
    // Start is called before the first frame update
    void Start()
    {
        if(glitchVolume.profile.TryGet<AnalogGlitchVolume>(out glitch))
        {
            Debug.Log("Success");
            EventManager.PortalOpened += EventManager_OnPortal;
            //glitch.scanLineJitter.value = 1;
        }

        if (glitchVolume.profile.TryGet<DigitalGlitchVolume>(out digitalGlitch))
        {
            Debug.Log("Digi Success");
            EventManager.Death += EventManager_OnDeath;
            //glitch.scanLineJitter.value = 1;
        }
    }

    private void EventManager_OnPortal()
    {
        StopAllCoroutines(); //Ensure one play at a time
        StartCoroutine(Glitch(glitch));
    }

    private void EventManager_OnDeath()
    {
        StopAllCoroutines(); //Ensure one play at a time
        StartCoroutine(DigiGlitch(digitalGlitch));
    }

    private IEnumerator DigiGlitch(DigitalGlitchVolume glitch)
    {
        yield return StartCoroutine(DigiGlitchIn(100f, glitch));
        yield return StartCoroutine(DigiGlitchOut(2f, glitch));
    }

    private IEnumerator DigiGlitchIn(float timeSpeed, DigitalGlitchVolume glitch)
    {
        while (glitch.intensity.value < 1.0f)
        {
            glitch.intensity.value += (Time.deltaTime * timeSpeed);
            yield return null;
        }
    }

    private IEnumerator DigiGlitchOut(float timeSpeed, DigitalGlitchVolume glitch)
    {
        while (glitch.intensity.value > 0.0f)
        {
            glitch.intensity.value -= (Time.deltaTime * timeSpeed);
            yield return null;
        }
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
            //glitch.verticalJump.value += (Time.deltaTime * timeSpeed);
            glitch.horizontalShake.value += (Time.deltaTime * timeSpeed);
            glitch.scanLineJitter.value += (Time.deltaTime * timeSpeed);
            glitch.colorDrift.value += (Time.deltaTime * timeSpeed);
            yield return null;
        }
    }

    private IEnumerator GlitchOut(float timeSpeed, AnalogGlitchVolume glitch)
    {
        while (glitch.scanLineJitter.value > 0.0f)
        {
            //glitch.verticalJump.value -= (Time.deltaTime * timeSpeed);
            glitch.horizontalShake.value -= (Time.deltaTime * timeSpeed);
            glitch.scanLineJitter.value -= (Time.deltaTime * timeSpeed);
            glitch.colorDrift.value -= (Time.deltaTime * timeSpeed);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        EventManager.Death -= EventManager_OnDeath;
        EventManager.PortalOpened -= EventManager_OnPortal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
