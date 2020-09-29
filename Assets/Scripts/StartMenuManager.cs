using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuManager : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene("GameLevel");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadOption()
    {
        SceneManager.LoadScene("Option");
    }

    public void LoadShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
        PauseMenu.GameIsPaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartStats()
    {
        PlayerPrefs.DeleteAll();
    }
}
