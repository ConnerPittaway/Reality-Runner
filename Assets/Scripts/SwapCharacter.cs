using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacter : MonoBehaviour
{
    public AnimatorOverrideController shroud;
    public AnimatorOverrideController newPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if(GlobalDataManager.Instance == null)
        {
            LoadShroud();
        }
        else
        {
            //switch 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadShroud()
    {
        GetComponent<Animator>().runtimeAnimatorController = shroud as RuntimeAnimatorController;
    }

    public void LoadNewPlayer()
    {
        GetComponent<Animator>().runtimeAnimatorController = newPlayer as RuntimeAnimatorController;
    }
}
