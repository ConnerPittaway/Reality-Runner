using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTweenReset : MonoBehaviour
{
    private void Awake()
    {
        LeanTween.reset();
    }
}
