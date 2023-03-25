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

    // Start is called before the first frame update
    void Start()
    {

    }

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

    public void OnShroud2Selected()
    {
        characterToSelect = GlobalDataManager.Characters.SHROUD2;
        CheckCharacter();
        StopUIAnimation();
        StartCoroutine(AnimateUISprite());
    }

    public void OnShroud3Selected()
    {
        characterToSelect = GlobalDataManager.Characters.SHROUD3;
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
