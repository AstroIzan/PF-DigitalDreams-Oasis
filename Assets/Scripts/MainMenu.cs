using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Its a menu, with three buttons
    // The first (ButtonPlay) is to go to the gameStartScene
    // The second (ButtonOptions) is to go to the optionsScene
    // The third (ButtonExit) is to quit the game 

    public void playGame()
    {
        SceneManager.LoadScene("gameStartScene");
    }

    public void options()
    {
        SceneManager.LoadScene("optionsScene");
    }

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
