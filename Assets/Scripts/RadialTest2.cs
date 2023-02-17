using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialTest2 : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        startedRoutines = false;
    }

    public void useItem()
    {
        usedItem = true;
        player.hasPowerup = true;
        currentAmount = 100.0f;
        itemButton.interactable = false;
        Debug.Log("Button");
        //portalImage.enabled = false;
        //UIController.Func_StopUIAnim();
        startedRoutines = false;
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
        if(player.hasPowerup)
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
                player.hasPowerup = !player.hasPowerup;
                currentAmount = 0;
            }
        }
        LoadingBarImage.fillAmount = currentAmount / 100;
    }
}
