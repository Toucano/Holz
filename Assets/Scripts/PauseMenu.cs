using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;

    public void Pause()
    {
        if (GameIsPaused == false)
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0;
            GameIsPaused = true;
        }
    }
    
    public void Resume()
    {
        if (GameIsPaused == true)
        {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1;
            GameIsPaused = false;
        }
    }
}
