using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRadial : MonoBehaviour
{
    public Button itemButton;
    public Transform LoadingBar;
    public Image itemImage;
    public TMPro.TextMeshProUGUI textProgress;
    public Image LoadingBarImage;
    public float currentAmount;
    public UISpriteAnimate UIController;
    public bool startedRoutines;
    public PlayerController player;
    public bool usedItem;

    //Item Effects
    public GameObject forcefieldEffect;
    public float forcefieldTime;

    // Start is called before the first frame update
    void Start()
    {
        startedRoutines = false;

        //Set Item Speed
        switch (GlobalDataManager.Instance.currentShieldLevel)
        {
            case GlobalDataManager.ShieldLevel.LEVEL1:
                forcefieldTime = 1.5f;
                break;
            case GlobalDataManager.ShieldLevel.LEVEL2:
                forcefieldTime = 2f;
                break;
            case GlobalDataManager.ShieldLevel.LEVEL3:
                forcefieldTime = 2.5f;
                break;
            case GlobalDataManager.ShieldLevel.LEVEL4:
                forcefieldTime = 3f;
                break;
            case GlobalDataManager.ShieldLevel.LEVEL5:
                forcefieldTime = 3.5f;
                break;
        }
    }

    public void useItem()
    {
        usedItem = true;
        player.activeItem = player.heldItem;
        player.heldItem = PlayerController.ItemTypes.NONE;
        currentAmount = 100.0f;
        itemButton.interactable = false;
        startedRoutines = false;
        float speed = 0.0f;

        switch(player.activeItem)
        {
            case PlayerController.ItemTypes.SHIELD:
                forcefieldEffect.SetActive(true);
                speed = forcefieldTime;
                break;
        }

        LeanTween.value(gameObject, currentAmount, 0, speed).setOnUpdate((float val)=>
        {
            currentAmount = val;
        }).setOnComplete(itemFinished);
    }

    private void Awake()
    {
        LoadingBarImage = LoadingBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.heldItem != PlayerController.ItemTypes.NONE)
        {
            itemButton.interactable = true;
        }
        else
        {
            itemButton.interactable = false;
        }
        LoadingBarImage.fillAmount = currentAmount / 100;
    }

    void itemFinished()
    {
        usedItem = !usedItem;
        currentAmount = 0;
        switch (player.activeItem)
        {
            case PlayerController.ItemTypes.SHIELD:
                forcefieldEffect.SetActive(false);
                break;
        }
        player.activeItem = PlayerController.ItemTypes.NONE;
    }
}
