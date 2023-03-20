using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStore : MonoBehaviour
{

    public string characterToSelect;
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

    public void OnShroudSelected()
    {
        characterToSelect = "Shroud";
        CheckCharacter();
    }

    public void OnShroud2Selected()
    {
        characterToSelect = "Shroud2";
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
