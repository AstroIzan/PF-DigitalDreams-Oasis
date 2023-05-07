using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class settingsQualityScript : MonoBehaviour
{
    // Variables
    public TMP_Dropdown QualityDropdown;
    public int QualityValue;

    /**
     * Method to initialize the script
     * - Get the quality value from the player prefs
     * - Set the quality value
     */
    void Start()
    {
        QualityValue = PlayerPrefs.GetInt("QualityLevel", 0);
        QualityDropdown.value = QualityValue;
        SetQuality();
    }

    /**
     * Method to set the quality
     * - Set the quality value
     * - Set the player prefs
     * - Set the quality settings
     */
    public void SetQuality()
    {
        QualityValue = QualityDropdown.value;
        PlayerPrefs.SetInt("QualityLevel", QualityValue);
        QualitySettings.SetQualityLevel(QualityValue);
    }
}
