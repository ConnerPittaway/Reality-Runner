using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class spriteListClass
{
    public List<Sprite> spriteSheet;
}


public class CharacterAnimator : MonoBehaviour
{

    public Image image;
    public float animationSpeed = .02f;
    public List<spriteListClass> nestedSpriteList = new List<spriteListClass>();
    private int currentCharacter = 0;
    private void Start()
    {
        
    }

    public void StopUIAnimation()
    {
        StopAllCoroutines();
    }
    IEnumerator AnimateUISprite()
    {
        
        for (int i = 0; i < nestedSpriteList[currentCharacter].spriteSheet.Count; i++)
        {
            yield return new WaitForSeconds(animationSpeed);
            image.sprite = nestedSpriteList[currentCharacter].spriteSheet[i];
        }
        StartCoroutine(AnimateUISprite());
    }

    private void OnEnable()
    {
        currentCharacter = (int)GlobalDataManager.Instance.currentlySelectedCharacter;
        StartCoroutine(AnimateUISprite());
    }

    private void OnDisable()
    {
        StopUIAnimation();
    }
}

