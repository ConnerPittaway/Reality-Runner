using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacter : MonoBehaviour
{
    public AnimatorOverrideController defaultPlayer;
    public AnimatorOverrideController newPlayer;

    // Start is called before the first frame update
    void Start()
    {
        LoadNewPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadDefaultPlayer()
    {
        GetComponent<Animator>().runtimeAnimatorController = defaultPlayer as RuntimeAnimatorController;
    }

    public void LoadNewPlayer()
    {
        GetComponent<Animator>().runtimeAnimatorController = newPlayer as RuntimeAnimatorController;
    }
}
