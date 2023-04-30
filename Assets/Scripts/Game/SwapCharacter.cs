using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacter : MonoBehaviour
{
    public SerializableDictionary<GlobalDataManager.Characters, AnimatorOverrideController> characters;

    // Start is called before the first frame update
    void Start()
    {
        if (GlobalDataManager.Instance == null)
        {
            GetComponent<Animator>().runtimeAnimatorController = characters[0] as RuntimeAnimatorController;
        }
        else
        {
            GetComponent<Animator>().runtimeAnimatorController = characters[GlobalDataManager.Instance.currentlySelectedCharacter] as RuntimeAnimatorController;
        }
    }
}
