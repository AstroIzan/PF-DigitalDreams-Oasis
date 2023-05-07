using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class settingsMenu : MonoBehaviour
{
    private GameObject modalAlertFAQs;
    private CanvasGroup canvasGroup;

    /**
     * Method to start the script
         * - Get the objects by name´
         * - Check if the objects are founds
             * [IF isn't found] - Show a debug message
             * [ELSE] - Get the canvas group of the objects and close the objects to set the initial state
     */
    void Start() {
        modalAlertFAQs = GameObject.Find("modalAlertFAQs");
        if (modalAlertFAQs == null) {
            Debug.Log("No se ha encontrado el objeto modalAlertFAQs");
        }
        else {
            canvasGroup = modalAlertFAQs.GetComponent<CanvasGroup>();
            closeModalAlertGame();
        }
    }

    /**
     * Method to update the script
     * - Check if the scape key is pressed
         * [IF is pressed] - Check if the modal alert object is open
             * [IF is open] - Close the modal alert object
         * [ELSE] - Load the main menu scene
     */
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canvasGroup.alpha == 1)
            {
                closeModalAlertGame();
            }
            else
            {
                ReturnToMainMenu();
            }
        }
    }

    /**
     * Method to return to the main menu
     */
    public void ReturnToMainMenu()
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
     * Method to accept the modal alert game and open the FAQs
     */
    public void acceptModalAlertGame()
    {
        closeModalAlertGame();
        // Application.OpenURL("https://www.oasis.com/es/faq");
    }
}
