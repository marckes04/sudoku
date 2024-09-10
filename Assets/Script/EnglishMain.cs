using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnglishMain : MonoBehaviour
{
    public Button easyButton;

    public Button middleButton;

    public Button hardButton;


    public void ClickOn_Easy()
    {
        SceneManager.LoadScene("GameEnglish");
        EnglishGameSettings.EasyMiddleHard_Number = 1;
    }

    public void ClickOn_Middle()
    {
        SceneManager.LoadScene("GameEnglish");
        EnglishGameSettings.EasyMiddleHard_Number = 2;
    }

    public void ClickOn_Hard()
    {
        SceneManager.LoadScene("GameEnglish");
        EnglishGameSettings.EasyMiddleHard_Number = 3;
    }
}
