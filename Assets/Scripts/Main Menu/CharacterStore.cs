using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        EventManager.OnUIElementOpened();
    }
}