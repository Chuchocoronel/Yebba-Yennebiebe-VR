using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }
    public void Quitgame()
    {
        Application.Quit();
    }
}
