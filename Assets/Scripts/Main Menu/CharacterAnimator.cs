using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAnimator : MonoBehaviour
{

    public Image image;
    public Sprite[] spriteArray;
    public float animationSpeed = .02f;

    private void Start()
    {
        
    }

    public void StopUIAnimation()
    {
        StopAllCoroutines();
    }
    IEnumerator AnimateUISprite()
    {
        for(int i = 0; i < spriteArray.Length; i++)
        {
            yield return new WaitForSeconds(animationSpeed);
            image.sprite = spriteArray[i];
        }
        StartCoroutine(AnimateUISprite());
    }

    private void OnEnable()
    {
        StartCoroutine(AnimateUISprite());
    }

    private void OnDisable()
    {
        StopUIAnimation();
    }
}

