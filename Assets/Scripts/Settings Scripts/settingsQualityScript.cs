using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class settingsQualityScript : MonoBehaviour
{
    public TMP_Dropdown QualityDropdown;
    public int QualityValue;

    // Start is called before the first frame update
    void Start()
    {
        QualityValue = PlayerPrefs.GetInt("QualityLevel", 0);
        QualityDropdown.value = QualityValue;
        SetQuality();
    }

    public void SetQuality()
    {
        QualityValue = QualityDropdown.value;
        PlayerPrefs.SetInt("QualityLevel", QualityValue);
        QualitySettings.SetQualityLevel(QualityValue);
    }
}
