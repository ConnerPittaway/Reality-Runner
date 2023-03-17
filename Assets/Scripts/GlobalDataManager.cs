using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour
{
    //Instance
    public static GlobalDataManager Instance;

    //Data
    private int totalCoins = 0;
    private int highScore = 0;

    private void Awake()
    {
        //Create Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //LoadData()
    }

    //Coins
    //Add or Subtract Coins
    public void AlterCoins(int value)
    {
        totalCoins += value;
    }

    public int GetCoins()
    {
        return totalCoins;
    }

    //High Score
    public void UpdateHighScore(int score)
    {
        if(score > highScore)
        {
            highScore = score;
        }
    }

    public int GetHighScore()
    {
        return highScore;
    }

    //Unlocks

}
