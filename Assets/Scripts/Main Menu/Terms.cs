using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terms : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GlobalSettingsManager.Instance.acceptedTerms)
        {
            gameObject.SetActive(false);
        }

    }

    public void Accept()
    {
        GlobalSettingsManager.Instance.acceptedTerms = true;
        GlobalSettingsManager.Instance.SaveSettingsData();
        gameObject.SetActive(false);
    }

    public void Deny()
    {
        Application.Quit();
    }
}
