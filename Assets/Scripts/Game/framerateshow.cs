 using UnityEngine;
 using System.Collections;
 using UnityEngine.UI;
 
 public class framerateshow : MonoBehaviour
{
    public Text fpsText;
    public float deltaTime;

    private void Start()
    {
        EventManager.FrameShowChanged += EventManager_OnFrameShowChanged;
        if (!GlobalSettingsManager.Instance.GetFrameCounter())
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
    }

    private void EventManager_OnFrameShowChanged(bool frameShow)
    {
        gameObject.SetActive(frameShow);
    }

    private void OnDestroy()
    {
        EventManager.FrameShowChanged -= EventManager_OnFrameShowChanged;
    }
}
