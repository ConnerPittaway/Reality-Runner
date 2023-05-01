using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalRadial : MonoBehaviour
{
    public Button portalButton;
    public bool isSpawningPortal;
    public Transform LoadingBar;
    public Image portalImage;
    public TMPro.TextMeshProUGUI textProgress;
    public Image LoadingBarImage;
    public float currentAmount;
    public UISpriteAnimate UIController;
    public bool startedRoutines;
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        startedRoutines = false;
        LeanTween.value(gameObject, currentAmount, 100, 2.5f).setOnUpdate((float val) =>
        {
            currentAmount = val;
            textProgress.text = ((int)currentAmount).ToString() + "%";
        }).setOnComplete(PortalFinished);
    }

    public void spawnPortal()
    {
        isSpawningPortal = true;
        currentAmount = 0.0f;
        portalButton.interactable = false;
        portalImage.enabled = false;
        UIController.StopUIAnimation();
        startedRoutines = false;

        LeanTween.value(gameObject, currentAmount, 100, 2.5f).setOnUpdate((float val) =>
        {
            currentAmount = val;
            textProgress.text = ((int)currentAmount).ToString() + "%";
        }).setOnComplete(PortalFinished);
    }

    private void Awake()
    {
        isSpawningPortal = false;
        LoadingBarImage = LoadingBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isPaused)
        {
            if (currentAmount == 100)
            {
                if (!startedRoutines)
                {
                    UIController.StartUIAnimation();
                    startedRoutines = true;
                }
            }
        }
        LoadingBarImage.fillAmount = currentAmount / 100;
    }

    private void PortalFinished()
    {
        textProgress.text = "";
        portalImage.enabled = true;
        portalButton.interactable = true;
        if (!startedRoutines)
        {
            UIController.StartUIAnimation();
            startedRoutines = true;
        }
    }

    private void OnDisable()
    {
        startedRoutines = false;
    }
}
