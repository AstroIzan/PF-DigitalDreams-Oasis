using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startSession : MonoBehaviour
{
    // Variables
    private GameObject modalAlertSession;
    private CanvasGroup canvasGroup;

    /**
     * Method to start the script
     * - Get the objects by name
     * - Check if the objects are founds
         * [IF isn't found] - Show a debug message
         * [ELSE] - Get the canvas group of the objects and close the objects to set the initial state  
     */
    void Start() {
        modalAlertSession = GameObject.Find("ModalAlertSession");
        if (modalAlertSession == null) {
            Debug.Log("No se ha encontrado el objeto ModalAlertSession");
        }
        else {
            canvasGroup = modalAlertSession.GetComponent<CanvasGroup>();
            closeModalAlertGame();
        }
    }

    /**
     * Method to load the game scene
     */
    public void continueGame()
    {
        SceneManager.LoadScene("gameScene");
    }

    /**
     * Method to return to the main menu
     */
    public void exitGame() 
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
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
        SceneManager.LoadScene("mainMenuScene");
    }
}
