using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class distanceEnd : MonoBehaviour
{
    public PlayerController player;
    public TMPro.TextMeshProUGUI distanceTmp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isDead)
        {
            int distance = Mathf.RoundToInt(player.distance);
            distanceTmp.text = distance.ToString() + "m";
        }
    }

}