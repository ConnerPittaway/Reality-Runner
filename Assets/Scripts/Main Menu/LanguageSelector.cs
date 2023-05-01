using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageSelector : MonoBehaviour
{
    public TMP_Text currentLanguageText;
    public GlobalSettingsManager.Languages currentLanguage;
    // Start is called before the first frame update
    void Start()
    {
        currentLanguage = GlobalSettingsManager.Instance.currentlySelectedLanguage;
    }

    public void SelectUK()
    {
        currentLanguage = GlobalSettingsManager.Languages.ENGLISHUK;
        GlobalSettingsManager.Instance.currentlySelectedLanguage = currentLanguage;
        UpdateLanguage();
    }

    public void SelectUS()
    {
        currentLanguage = GlobalSettingsManager.Languages.ENGLISHUS;
        GlobalSettingsManager.Instance.currentlySelectedLanguage = currentLanguage;
        UpdateLanguage();
    }

    public void SelectAU()
    {
        currentLanguage = GlobalSettingsManager.Languages.ENGLISHAU;
        GlobalSettingsManager.Instance.currentlySelectedLanguage = currentLanguage;
        UpdateLanguage();
    }

    public void SelectNZ()
    {
        currentLanguage = GlobalSettingsManager.Languages.ENGLISHNZ;
        GlobalSettingsManager.Instance.currentlySelectedLanguage = currentLanguage;
        UpdateLanguage();
    }

    public void SelectCA()
    {
        currentLanguage = GlobalSettingsManager.Languages.ENGLISHCA;
        GlobalSettingsManager.Instance.currentlySelectedLanguage = currentLanguage;
        UpdateLanguage();
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }

    void UpdateLanguage()
    {
        string currentLanguageString = "Current Language: ";
        switch (currentLanguage)
        {
            case GlobalSettingsManager.Languages.ENGLISHUK:
                currentLanguageString += "English (UK)"; 
                break;
            case GlobalSettingsManager.Languages.ENGLISHUS:
                currentLanguageString += "English (US)";
                break;
            case GlobalSettingsManager.Languages.ENGLISHAU:
                currentLanguageString += "English (AU)";
                break;
            case GlobalSettingsManager.Languages.ENGLISHNZ:
                currentLanguageString += "English (NZ)";
                break;
            case GlobalSettingsManager.Languages.ENGLISHCA:
                currentLanguageString += "English (CA)";
                break;
        }
        currentLanguageText.text = currentLanguageString;
    }

    void OnEnable()
    {
        currentLanguage = GlobalSettingsManager.Instance.currentlySelectedLanguage;
        UpdateLanguage();
    }
}
