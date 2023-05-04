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

    //Animation
    public Image image;
    public float animationSpeed = .02f;
    public List<spriteListClass> nestedSpriteList = new List<spriteListClass>();

    void OnEnable()
    {
        characterToSelect = GlobalDataManager.Instance.currentlySelectedCharacter;
        selectButtonText.text = "Selected";
        characterSelectButton.interactable = false;
        EventManager.OnUIElementOpened();
        StartCoroutine(AnimateUISprite());
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
        StopUIAnimation();
        StartCoroutine(AnimateUISprite());
    }

    public void OnBillySelected()
    {
        characterToSelect = GlobalDataManager.Characters.BILLY;
        CheckCharacter();
        StopUIAnimation();
        StartCoroutine(AnimateUISprite());
    }

    public void OnFionaSelected()
    {
        characterToSelect = GlobalDataManager.Characters.FIONA;
        CheckCharacter();
        StopUIAnimation();
        StartCoroutine(AnimateUISprite());
    }

    public void OnPhillySelected()
    {
        characterToSelect = GlobalDataManager.Characters.PHILLY;
        CheckCharacter();
        StopUIAnimation();
        StartCoroutine(AnimateUISprite());
    }

    public void OnSparkleSelected()
    {
        characterToSelect = GlobalDataManager.Characters.SPARKLE;
        CheckCharacter();
        StopUIAnimation();
        StartCoroutine(AnimateUISprite());
    }

    public void OnSpuderSelected()
    {
        characterToSelect = GlobalDataManager.Characters.SPUDERMAN;
        CheckCharacter();
        StopUIAnimation();
        StartCoroutine(AnimateUISprite());
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

    public void StopUIAnimation()
    {
        StopAllCoroutines();
    }
    IEnumerator AnimateUISprite()
    {
        int currentCharacter = (int)characterToSelect;
        for (int i = 0; i < nestedSpriteList[currentCharacter].spriteSheet.Count; i++)
        {
            yield return new WaitForSeconds(animationSpeed);
            image.sprite = nestedSpriteList[currentCharacter].spriteSheet[i];
        }
        StartCoroutine(AnimateUISprite());
    }

    private void OnDisable()
    {
        StopUIAnimation();
    }
}
