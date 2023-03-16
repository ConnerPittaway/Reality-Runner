using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgrounds : MonoBehaviour
{
    public List<GameObject> background;
    public List<SpriteRenderer> spriteRenderers;
    public GameObject background1;
    public GameObject background2;

    public enum Worlds
    {
        FUTURISTIC,
        INDUSTRIAL
    };

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in background)
        {
            spriteRenderers.Add(obj.GetComponent<SpriteRenderer>());
        }
    }
    public void SwitchBackgrounds(Worlds world)
    {
        /*if(backGroundNumber == 0)
        {
            background1.SetActive(false);
            background2.SetActive(true);
        }
        else
        {
            background1.SetActive(true);
            background2.SetActive(false);
        }*/
        int backgroundToChange = 0;
        switch (world)
        {
            case Worlds.FUTURISTIC:
                foreach (GameObject obj in background)
                {
                    if (backgroundToChange == 0 || backgroundToChange == 1)
                    {
                        spriteRenderers[backgroundToChange].sprite = Resources.Load<Sprite>("Sprites/Buildings/or_b_4");
                        backgroundToChange++;
                    }
                    else if (backgroundToChange == 2 || backgroundToChange == 3)
                    {
                        spriteRenderers[backgroundToChange].sprite = Resources.Load<Sprite>("Sprites/Buildings/or_b_3");
                        backgroundToChange++;
                    }
                    else if (backgroundToChange == 4 || backgroundToChange == 5)
                    {
                        spriteRenderers[backgroundToChange].sprite = Resources.Load<Sprite>("Sprites/Buildings/or_b_2");
                        backgroundToChange++;
                    }
                    else if (backgroundToChange == 6 || backgroundToChange == 7)
                    {
                        spriteRenderers[backgroundToChange].sprite = Resources.Load<Sprite>("Sprites/Buildings/or_b_1");
                        backgroundToChange++;
                    }
                }
                break;
            case Worlds.INDUSTRIAL:
                foreach (GameObject obj in background)
                {
                    if (backgroundToChange == 0 || backgroundToChange == 1)
                    {
                        spriteRenderers[backgroundToChange].sprite = Resources.Load<Sprite>("Sprites/Industrial/bg");
                        backgroundToChange++;
                    }
                    else if (backgroundToChange == 2 || backgroundToChange == 3)
                    {
                        spriteRenderers[backgroundToChange].sprite = Resources.Load<Sprite>("Sprites/Industrial/far-buildings");
                        backgroundToChange++;
                    }
                    else if (backgroundToChange == 4 || backgroundToChange == 5)
                    {
                        spriteRenderers[backgroundToChange].sprite = Resources.Load<Sprite>("Sprites/Industrial/buildings");
                        backgroundToChange++;
                    }
                    else if (backgroundToChange == 6 || backgroundToChange == 7)
                    {
                        spriteRenderers[backgroundToChange].sprite = Resources.Load<Sprite>("Sprites/Industrial/skill-foreground");
                        backgroundToChange++;
                    }
                }
                break;
            default:
                Debug.Log("Error in background");
                break;
        }
    }
}
