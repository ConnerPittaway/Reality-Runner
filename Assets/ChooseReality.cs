using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseReality : MonoBehaviour
{
    public void Futuristic()
    {
        GlobalDataManager.Instance.startReality = backgrounds.Worlds.FUTURISTIC;
        StartGame();
    }

    public void Space()
    {
        GlobalDataManager.Instance.startReality = backgrounds.Worlds.SPACE;
        StartGame();
    }

    public void Love()
    {
        GlobalDataManager.Instance.startReality = backgrounds.Worlds.HEART;
        StartGame();
    }

    public void Hell()
    {
        GlobalDataManager.Instance.startReality = backgrounds.Worlds.HELL;
        StartGame();
    }

    void StartGame()
    {
        AudioManager.Instance.StartGameAudio();
        SceneManager.LoadScene("RealityRunnerGame");
    }
}
