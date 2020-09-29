using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitialisation : MonoBehaviour
{
    //[SerializeField] float qualityReduce = .7f;


    Settings settings;

    void Start()
    {
        if (PlayerPrefs.GetString("quality") == "fast")
        {
            Settings.Quality = "fast";
        }
        else if (PlayerPrefs.GetString("quality") == "high")
        {
            Settings.Quality = "high";
        }
        SetQuality();
    }

    private void SetQuality()
    {
        //var initialWidth = Screen.currentResolution.width;
        //var initialHeight = Screen.currentResolution.height;
        if (Settings.Quality == "fast")
        {
            QualitySettings.SetQualityLevel(0);
        }
        if (Settings.Quality == "high")
        {
            QualitySettings.SetQualityLevel(4);
        }
    }
}
