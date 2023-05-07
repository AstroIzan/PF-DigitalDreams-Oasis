using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class settingsFullScreenScript : MonoBehaviour
{
    // Variables
    public Toggle FullScreenToggle;
    public TMP_Dropdown ResolutionDropdown;
    Resolution[] resolutions;

    /**
     * Method to initialize the script
     * - Check if the screen is full screen
     * - Check the resolution
     */
    void Start()
    {
        if (Screen.fullScreen)
        {
            FullScreenToggle.isOn = true;
        }
        else
        {
            FullScreenToggle.isOn = false;
        }

        CheckResolution();
    }

    /**
     * Method to set the full screen
     * - Set the full screen
     */
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    /**
     * Method to check the resolution
     * - Get the resolutions
     * - Clear the options
     * - Create a list of options
     * - Set the current resolution index
     * - Show the options
     */
    public void CheckResolution() {
        resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = resolutions.Length - 1; i >= 0; i--)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = resolutions.Length - 1 - i;
            }
        }
        options.Add("");

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();
    }

    /**
     * Method to set the resolution
     * - Get the resolution
     * - Set the resolution
     */
    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutions.Length - 1 - resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }   
}
