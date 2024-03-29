using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimate : MonoBehaviour
{

    public Image image;

    public List<Sprite> spriteSheet;
    public float animationSpeed = .02f;
    public float fadeSpeed = .02f;

    private void Start()
    {
        Color spriteAlpha = image.color;
        spriteAlpha.a = 255.0f;
        image.color = spriteAlpha;
    }

    public void StartUIAnimation()
    {
        StartCoroutine(AnimateUISprite());
        StartCoroutine(FlashIn());
    }

    public void StopUIAnimation()
    {
        StopAllCoroutines();
    }
    IEnumerator AnimateUISprite()
    {
        for(int i = 0; i < spriteSheet.Count; i++)
        {
            yield return new WaitForSeconds(animationSpeed);
            image.sprite = spriteSheet[i];
        }
        StartCoroutine(AnimateUISprite());
    }

    IEnumerator FlashOut()
    {
        float counter = 0;
        Color spriteColor = image.color;
        while (counter < fadeSpeed)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / fadeSpeed);

            //Change alpha only
            image.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);

            //Wait for a frame
             yield return null;
        }
        StartCoroutine(FlashIn());
        StopCoroutine(FlashOut());
    }

    IEnumerator FlashIn()
    {
        float counter = 0;
        Color spriteColor = image.color;
        while (counter < fadeSpeed)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(0, 1, counter / fadeSpeed);

            //Change alpha only
            image.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);

            //Wait for a frame
            yield return null;
        }
        StartCoroutine(FlashOut());
        StopCoroutine(FlashIn());
    }
}

