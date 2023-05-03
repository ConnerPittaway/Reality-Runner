using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backgrounds : MonoBehaviour
{
    public List<GameObject> background;
    public List<SpriteRenderer> spriteRenderers;
    public GameObject background1;
    public GameObject background2;
    public Image backgroundImage;
    public enum Worlds
    {
        FUTURISTIC,
        SPACE,
        HEART
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
        switch (world)
        {
            case Worlds.FUTURISTIC:
                LoadFirstLayer("Buildings/or_b_1");
                LoadSecondLayer("Buildings/or_b_2");
                LoadThirdLayer("Buildings/or_b_3");
                LoadFourthLayer("Buildings/or_b_4");
                break;
            case Worlds.SPACE:
                LoadFirstLayer("Space/br_1");
                LoadSecondLayer("Space/br_2");
                LoadThirdLayer("Space/br_3");
                LoadFourthLayer("Space/br_4");
                break;
            case Worlds.HEART:
                LoadFirstLayer("Heart/br_1");
                LoadSecondLayer("Heart/br_2");
                LoadThirdLayer("Heart/br_3");
                LoadFourthLayer("Heart/br_4");
                break;
            default:
                Debug.Log("Error in background");
                break;
        }
    }

    void LoadFourthLayer(string path)
    {
        backgroundImage.sprite = Resources.Load<Sprite>("Sprites/" + path);
        spriteRenderers[0].sprite = Resources.Load<Sprite>("Sprites/" + path);
        spriteRenderers[1].sprite = Resources.Load<Sprite>("Sprites/" + path);
    }
    void LoadThirdLayer(string path)
    {
        spriteRenderers[2].sprite = Resources.Load<Sprite>("Sprites/" + path);
        spriteRenderers[3].sprite = Resources.Load<Sprite>("Sprites/" + path);
    }
    void LoadSecondLayer(string path)
    {
        spriteRenderers[4].sprite = Resources.Load<Sprite>("Sprites/" + path);
        spriteRenderers[5].sprite = Resources.Load<Sprite>("Sprites/" + path);
    }

    void LoadFirstLayer(string path)
    {
        spriteRenderers[6].sprite = Resources.Load<Sprite>("Sprites/" + path);
        spriteRenderers[7].sprite = Resources.Load<Sprite>("Sprites/" + path);
    }


}
