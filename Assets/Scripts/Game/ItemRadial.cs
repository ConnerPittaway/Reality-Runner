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

    // Start is called before the first frame update
    void Start()
    {
        startedRoutines = false;
    }

    public void useItem()
    {
        usedItem = true;
        player.activeItem = player.heldItem;
        player.heldItem = PlayerController.ItemTypes.NONE;
        currentAmount = 100.0f;
        itemButton.interactable = false;
        startedRoutines = false;

        switch(player.activeItem)
        {
            case PlayerController.ItemTypes.SHIELD:
                forcefieldEffect.SetActive(true);
                break;
        }

        LeanTween.value(gameObject, currentAmount, 0, 2.5f).setOnUpdate((float val)=>
        {
            currentAmount = val;
        }).setOnComplete(itemFinished);
    }

    private void Awake()
    {
        canSpawnPortal = false;
       // textProgress = TextProgress.GetComponent<TMPro.TextMeshProUGUI>();
        LoadingBarImage = LoadingBar.GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentAmount);
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
