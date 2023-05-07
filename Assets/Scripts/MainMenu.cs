using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    /**
     * Meethod that is called on every frame
     * - Check if the escape key is pressed
         * [IF is pressed] - Load the main menu scene
     */
    public void playGame()
    {
        SceneManager.LoadScene("gameStartScene");
    }

    /**
     * Method to load the options scene
     * - Load the options scene
     */
    public void options()
    {
        SceneManager.LoadScene("optionsScene");
    }

    /**
     * Method to load the credits scene
     * - Load the credits scene
     */
    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
