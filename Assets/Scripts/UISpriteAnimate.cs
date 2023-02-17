using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimate : MonoBehaviour
{

    public Image image;

    public Sprite[] spriteArray;
    public float speed = .02f;
    public float speed2 = .02f;

    private int indexSprite;
    //Coroutine m_CorotineAnim;
    //Coroutine m_CorotineFade;
    bool IsDone;

    private void Start()
    {
        //StartCoroutine(AnimateUISprite());
        //StartCoroutine(Func_FlashOut());
        Color spriteAlpha = image.color;
        spriteAlpha.a = 255.0f;
        image.color = spriteAlpha;
    }

    public void StartUIAnimation()
    {
        IsDone = false;
        StartCoroutine(AnimateUISprite());
        StartCoroutine(FlashIn());
    }

    public void Func_StopUIAnim()
    {
        IsDone = true;
        StopAllCoroutines();
        //StopCoroutine(Func_PlayAnimUI());
        //StopCoroutine(Func_FlashOut());
    }
    IEnumerator AnimateUISprite()
    {
        yield return new WaitForSeconds(speed);
        if (indexSprite >= spriteArray.Length)
        {
            indexSprite = 0;
        }
        image.sprite = spriteArray[indexSprite];
        indexSprite += 1;
        if (IsDone == false)
        {
            //m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
            StartCoroutine(AnimateUISprite());
        }
    }

    IEnumerator Func_FlashOut()
    {
        //Debug.Log("Hello");
        // yield return new WaitForSeconds(m_Speed2);
        float counter = 0;
        //Get current color
        Color spriteColor = image.color;
       // Debug.Log(counter);
        while (counter < speed2)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / speed2);
            // Debug.Log(alpha);

            //Change alpha only
            image.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
             yield return null;
        }
        StartCoroutine(FlashIn());
        StopCoroutine(Func_FlashOut());
    }

    IEnumerator FlashIn()
    {
        //Debug.Log("Hello");
        // yield return new WaitForSeconds(m_Speed2);
        float counter = 0;
        //Get current color
        Color spriteColor = image.color;
        // Debug.Log(counter);
        while (counter < speed2)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(0, 1, counter / speed2);
            //  Debug.Log(alpha);

            //Change alpha only
            image.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
        StartCoroutine(Func_FlashOut());
        StopCoroutine(FlashIn());
    }
}

