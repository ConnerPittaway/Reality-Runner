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
            switch(GlobalDataManager.Instance.currentlySelectedCharacter)
            {
                case GlobalDataManager.Characters.SHROUD:
                    LoadShroud();
                    break;
                case GlobalDataManager.Characters.SHROUD2:
                    LoadShroud2();
                    break;
                case GlobalDataManager.Characters.SHROUD3:
                    LoadShroud3();
                    break;
                default:
                    Debug.LogError("No Character Assigned");
                    break;
            }
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

    public void LoadShroud2()
    {
        GetComponent<Animator>().runtimeAnimatorController = shroud as RuntimeAnimatorController;
    }

    public void LoadShroud3()
    {
        GetComponent<Animator>().runtimeAnimatorController = shroud as RuntimeAnimatorController;
    }

    public void LoadNewPlayer()
    {
        GetComponent<Animator>().runtimeAnimatorController = newPlayer as RuntimeAnimatorController;
    }
}
