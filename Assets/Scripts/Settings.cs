using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    [SerializeField] Button lowQualityButton;
    [SerializeField] Button highQualityButton;


    public static string Quality = "high";

    private void Start()
    {
        Debug.Log(QualitySettings.GetQualityLevel());
        if (Quality == "fast")
        {
            lowQualityButton.Select();
        }
        else if (Quality == "high")
        {
            highQualityButton.Select();
        }
    }

    public void SetQualityToFast()
    {
        if (Quality != "fast")
        {
            Quality = "fast";
            lowQualityButton.Select();
            PlayerPrefs.SetString("quality", "fast");
            QualitySettings.SetQualityLevel(0);
        }
    }

    public void SetQualityToHigh()
    {
        if (Quality != "high")
        {
            Quality = "high";
            highQualityButton.Select();
            PlayerPrefs.SetString("quality", "high");
            QualitySettings.SetQualityLevel(1);
        }
    }

}
