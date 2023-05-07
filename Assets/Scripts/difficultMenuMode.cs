using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class difficultMenuMode : MonoBehaviour
{
    /**
     * Meethod that is called on every frame
     * - Check if the escape key is pressed
         * [IF is pressed] - Load the main menu scene
     */
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("mainMenuScene");
        }
    }

    /**
     * Method to load the easy mode
     * - Load the game scene
     */
    public void adventurerMode()
    {
        Debug.Log("Adventurer Mode");
    }

    /**
     * Method to load the medium mode
     * - Load the game scene
     */
    public void survivorMode()
    {
        Debug.Log("Survivor Mode");
    }

    /**
     * Method to load the hard mode
     * - Load the game scene
     */
    public void godlikeMode()
    {
        Debug.Log("Godlike Mode");
    }

    /**
     * Method to load the main menu scene
     * - Load the main menu scene
     */
    public void backToMainMenu()
    {
        SceneManager.LoadScene("gameStartScene");
    }
}
