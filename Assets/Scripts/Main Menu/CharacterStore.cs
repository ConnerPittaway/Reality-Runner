using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStore : MonoBehaviour
{

    public GlobalDataManager.Characters characterToSelect;
    public Button characterSelectButton;
    public TMP_Text selectButtonText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        EventManager.OnUIElementOpened();
    }

    public void SelectOrBuy()
    {
        if(selectButtonText.text == "Select")
        {
            //Must mean player owns character
            GlobalDataManager.Instance.currentlySelectedCharacter = characterToSelect;
            GlobalDataManager.Instance.SaveData();
            CheckCharacter();
        }
        else
        {
            //Buy Character
            GlobalDataManager.Instance.boughtCharacters[characterToSelect] = true;
            GlobalDataManager.Instance.currentlySelectedCharacter = characterToSelect;
            GlobalDataManager.Instance.SaveData();
            CheckCharacter();
        }
    }

    public void OnShroudSelected()
    {
        characterToSelect = GlobalDataManager.Characters.SHROUD;
        CheckCharacter();
    }

    public void OnShroud2Selected()
    {
        characterToSelect = GlobalDataManager.Characters.SHROUD2;
        CheckCharacter();
    }

    public void OnShroud3Selected()
    {
        characterToSelect = GlobalDataManager.Characters.SHROUD3;
        CheckCharacter();
    }

    public void CheckCharacter()
    {
        if (characterToSelect == GlobalDataManager.Instance.currentlySelectedCharacter)
        {
            selectButtonText.text = "Selected";
            characterSelectButton.interactable = false;
        }
        else
        {
            if (GlobalDataManager.Instance.GetBoughtItems()[characterToSelect] == true)
            {
                selectButtonText.text = "Select";
                characterSelectButton.interactable = true;
            }
            else
            {
                selectButtonText.text = "Buy";
                characterSelectButton.interactable = true;
            }
        }
    }
}
