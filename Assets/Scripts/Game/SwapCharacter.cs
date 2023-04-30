using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacter : MonoBehaviour
{
    public AnimatorOverrideController shroud;
    public AnimatorOverrideController boxy;
    public AnimatorOverrideController fiona;

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
                case GlobalDataManager.Characters.BOXY:
                    LoadBoxy();
                    break;
                case GlobalDataManager.Characters.FIONA:
                    LoadFiona();
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

    public void LoadBoxy()
    {
        GetComponent<Animator>().runtimeAnimatorController = boxy as RuntimeAnimatorController;
    }

    public void LoadFiona()
    {
        GetComponent<Animator>().runtimeAnimatorController = fiona as RuntimeAnimatorController;
    }
}
