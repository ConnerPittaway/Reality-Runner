using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgress : MonoBehaviour
{
    public Button portalButton;
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
    // Start is called before the first frame update
    void Start()
    {
        startedRoutines = false;
    }

    public void spawnPortal()
    {
        canSpawnPortal = true;
        currentAmount = 0.0f;
        portalButton.interactable = false;
        portalImage.enabled = false;
        UIController.StopUIAnimation();
        startedRoutines = false;
    }

    private void Awake()
    {
        canSpawnPortal = false;
        textProgress = TextProgress.GetComponent<TMPro.TextMeshProUGUI>();
        LoadingBarImage = LoadingBar.GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!player.isPaused)
        {
            if (currentAmount + (speed * Time.deltaTime) < 100)
            {
                currentAmount += speed * Time.deltaTime;
                textProgress.text = ((int)currentAmount).ToString() + "%";
            }
            else
            {
                textProgress.text = "";
                portalImage.enabled = true;
                portalButton.interactable = true;
                if (!startedRoutines)
                {
                    UIController.StartUIAnimation();
                    startedRoutines = true;
                }
                currentAmount = 100;
            }
        }
        LoadingBarImage.fillAmount = currentAmount / 100;
    }
}
