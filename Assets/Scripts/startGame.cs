using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    // Variables
    private GameObject modalAlertGame;
    private CanvasGroup canvasGroup;

    /**
     * Method to start the script
     * - Get the objects by name
     * - Check if the objects are founds
         * [IF isn't found] - Show a debug message
         * [ELSE] - Get the canvas group of the objects and close the objects to set the initial state  
     */
    void Start() {
        modalAlertGame = GameObject.Find("ModalAlertGame");
        if (modalAlertGame == null) {
            Debug.Log("No se ha encontrado el objeto ModalAlertGame");
        }
        else {
            canvasGroup = modalAlertGame.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    /**
     * Method to update the script
     * - Check if the scape key is pressed
         * [IF is pressed] - Check if the modal alert object is open
             * [IF is open] - Close the modal alert object
         * [ELSE] - Load the main menu scene
     */
    public void continueGame()
    {
        SceneManager.LoadScene("gameScene");
        dataGameController dataController = FindObjectOfType<dataGameController>();
        if (dataController != null) {
            dataController.getData();
        }
    }

    /**
     * Method to return to the main menu
     */
    public void returnToMainMenu()
    {
        SceneManager.LoadScene("mainMenuScene");
    }

    /**
     * Method to open the modal alert game
     */
    public void openModalAlertGame()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /**
     * Method to close the modal alert game
     */
    public void closeModalAlertGame()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /**
     * Method to accept the modal alert game
     */
    public void acceptModalAlertGame()
    {
        SceneManager.LoadScene("newGameDifficultyScene");
    }
}
