using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 61;   
    }
}
