using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRadial : MonoBehaviour
{
    public Button itemButton;
    public bool canSpawnPortal;
    public Transform LoadingBar;
    public Transform TextProgress;
    public Image portalImage;
    public TMPro.TextMeshProUGUI textProgress;
    public Image LoadingBarImage;
    public float currentAmount;
    public UISpriteAnimate UIController;
    public bool startedRoutines;
    public PlayerController player;
    [SerializeField] private float speed;
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

        //portalImage.enabled = false;
        //UIController.Func_StopUIAnim();

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
        if(player.heldItem != PlayerController.ItemTypes.NONE)
        {
            itemButton.interactable = true;
        }
        else
        {
            itemButton.interactable = false;
        }

        if (!player.isPaused && usedItem)
        {
            if (currentAmount - (speed * Time.deltaTime) >= 0)
            {
                currentAmount -= speed * Time.deltaTime;
                //textProgress.text = ((int)currentAmount).ToString() + "%";
            }
            else
            {
                Debug.Log("Running Enable");
                // textProgress.text = "";
                   // portalImage.enabled = true;
                    //itemButton.interactable = true;
                    if (!startedRoutines)
                    {
                        // UIController.StartUIAnimation();
                        //startedRoutines = true;
                    }
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
        LoadingBarImage.fillAmount = currentAmount / 100;
    }
}
