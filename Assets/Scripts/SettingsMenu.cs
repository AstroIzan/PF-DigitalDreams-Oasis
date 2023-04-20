using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    // First button return to main menu
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("mainMenuScene");
    }
}
