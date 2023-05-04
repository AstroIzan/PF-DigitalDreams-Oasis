using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsFullScreenScript : MonoBehaviour
{
    public Toggle FullScreenToggle;
    // Start is called before the first frame update
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
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
