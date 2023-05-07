using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsVolumeScript : MonoBehaviour
{
    // Variables
    public Slider VolumeSlider;
    public float VolumeValue;

    /**
     * Method to initialize the script
     * - Get the volume value from the player prefs
     * - Set the volume value
     */
    void Start()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("VolumeLevel", 0.5f);
        AudioListener.volume = VolumeSlider.value;
    }

    /**
     * Method to set the volume
     * - Set the volume value
     * - Set the player prefs
     * - Set the audio listener volume
     */
    public void SetVolume(float volume)
    {
        VolumeValue = volume;
        PlayerPrefs.SetFloat("VolumeLevel", VolumeValue);
        AudioListener.volume = VolumeValue;
    }
}
