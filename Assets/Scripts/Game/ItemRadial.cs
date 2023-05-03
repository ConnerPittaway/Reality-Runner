using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRadial : MonoBehaviour
{
    public Button itemButton;
    public Transform LoadingBar;
    public TMPro.TextMeshProUGUI textProgress;
    public Image LoadingBarImage;
    public float currentAmount;
    public bool startedRoutines;
    public PlayerController player;

    //Item Effects
    public GameObject forcefieldEffect;
    public float forcefieldTime;
    public Image forcefieldImage;
    public UISpriteAnimate forcefieldUIController;

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
        if(player.activeItem != PlayerController.ItemTypes.NONE)
        {
            LeanTween.cancel(gameObject);
            itemFinished();
        }

        player.activeItem = player.heldItem;
        player.heldItem = PlayerController.ItemTypes.NONE;
        currentAmount = 100.0f;
        itemButton.interactable = false;
        startedRoutines = false;
        textProgress.enabled = true;
        float speed = 0.0f;

        switch(player.activeItem)
        {
            case PlayerController.ItemTypes.SHIELD:
                forcefieldEffect.SetActive(true);
                forcefieldImage.enabled = false;
                forcefieldUIController.StopUIAnimation();
                speed = forcefieldTime;
                break;
        }

        LeanTween.value(gameObject, currentAmount, 0, speed).setOnUpdate((float val)=>
        {
            currentAmount = val;
            textProgress.text = ((int)currentAmount).ToString() + "%";
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
            if (!startedRoutines)
            {
                textProgress.enabled = false;
                switch (player.heldItem)
                {
                    case PlayerController.ItemTypes.SHIELD:
                        forcefieldImage.enabled = true;
                        forcefieldUIController.StartUIAnimation();
                        startedRoutines = true;
                        break;
                }
            }
        }
        LoadingBarImage.fillAmount = currentAmount / 100;
    }

    void itemFinished()
    {
        textProgress.text = "";
        currentAmount = 0;
        switch (player.activeItem)
        {
            case PlayerController.ItemTypes.SHIELD:
                forcefieldEffect.SetActive(false);
                break;
        }
        player.activeItem = PlayerController.ItemTypes.NONE;
    }

    private void OnDisable()
    {
        startedRoutines = false;
    }
}
