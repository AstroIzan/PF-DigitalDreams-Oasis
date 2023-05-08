using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inGameSettingsScript : MonoBehaviour
{
    // Variables
    private GameObject ModalAlertFAQs;
    private CanvasGroup ModalAlertCanvasGroup;

    private GameObject SettingsScene;
    private CanvasGroup SettingsCanvasGroup;

    private GameObject InGameHUD;
    private CanvasGroup InGameHUDCanvasGroup;

    /**
     * Method to start the script
     * - Get the objects by name
     * - Check if the objects are founds
         * [IF isn't found] - Show a debug message
         * [ELSE] - Get the canvas group of the objects and close the objects to set the initial state  
     */
    void Start()
    {
        ModalAlertFAQs = GameObject.Find("ModalAlertFAQs");
        if (ModalAlertFAQs == null)
        {
            Debug.Log("No se ha encontrado el objeto ModalAlertFAQs");
        }
        else
        {
            ModalAlertCanvasGroup = ModalAlertFAQs.GetComponent<CanvasGroup>();
            closeObject("modalAlert");
        }

        SettingsScene = GameObject.Find("SettingsScene");
        if (SettingsScene == null)
        {
            Debug.Log("No se ha encontrado el objeto SettingsScene");
        }
        else
        {
            SettingsCanvasGroup = SettingsScene.GetComponent<CanvasGroup>();
            closeObject("settings");
        }

        InGameHUD = GameObject.Find("InGameHUD");
        if (InGameHUD == null)
        {
            Debug.Log("No se ha encontrado el objeto InGameHUD");
        }
        else
        {
            InGameHUDCanvasGroup = InGameHUD.GetComponent<CanvasGroup>();
            openObject("InGameHUD");
        }
    }

    /**
     * Method to update the script
     * - Check if the scape key is pressed
         * [IF is pressed] - Check if the settings object is open
             * [IF is open] - Check if the modal alert object is open
                 * [IF is open] - Close the modal alert object
             * [ELSE] - Close the settings object and set the time scale to 1
     */
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(checkObjectState("settings") == true)
            {
                if (checkObjectState("modalAlert") == true)
                {
                    closeObject("modalAlert");
                }
                closeObject("settings");
                openObject("InGameHUD");
                Time.timeScale = 1f;
            }
            else 
            {
                openObject("settings");
                closeObject("InGameHUD");
                Time.timeScale = 0f;
            }
        }
    }

    /*
     * Method to open objects
     * @param objectName
     */
    public void openObject(string objectName)
    {
        switch (objectName)
        {
            case "modalAlert":
                ModalAlertCanvasGroup.alpha = 1;
                ModalAlertCanvasGroup.interactable = true;
                ModalAlertCanvasGroup.blocksRaycasts = true;
                break;
            case "settings":
                SettingsCanvasGroup.alpha = 1;
                SettingsCanvasGroup.interactable = true;
                SettingsCanvasGroup.blocksRaycasts = true;
                break;
            case "InGameHUD":
                InGameHUDCanvasGroup.alpha = 1;
                InGameHUDCanvasGroup.interactable = true;
                InGameHUDCanvasGroup.blocksRaycasts = true;
                break;
            default:
                Debug.Log("No se ha encontrado el objeto " + objectName);
                break;
        }
    }

    /**
     * Method to close objects
     * @param objectName
     */
    public void closeObject(string objectName)
    {
        switch (objectName)
        {
            case "modalAlert":
                ModalAlertCanvasGroup.alpha = 0;
                ModalAlertCanvasGroup.interactable = false;
                ModalAlertCanvasGroup.blocksRaycasts = false;
                break;
            case "settings":
                SettingsCanvasGroup.alpha = 0;
                SettingsCanvasGroup.interactable = false;
                SettingsCanvasGroup.blocksRaycasts = false;
                break;
            case "InGameHUD":
                InGameHUDCanvasGroup.alpha = 0;
                InGameHUDCanvasGroup.interactable = false;
                InGameHUDCanvasGroup.blocksRaycasts = false;
                break;
            default:
                Debug.Log("No se ha encontrado el objeto " + objectName);
                break;
        }
    }

    /**
     * Method to check the state of the object
     * @param objectName
     */
    public bool checkObjectState(string objectName)
    {
        switch (objectName)
        {
            case "modalAlert":
                return ModalAlertCanvasGroup.alpha == 1;
            case "settings":
                return SettingsCanvasGroup.alpha == 1;
            case "InGameHUD":
                return InGameHUDCanvasGroup.alpha == 1;
            default:
                Debug.Log("No se ha encontrado el objeto " + objectName);
                return false;
        }
    }

    /**
     * Method to accept modal alert
     */
    public void acceptModalAlertGame()
    {
        closeObject("modalAlert");
        // Application.OpenURL("https://www.oasis.com/es/faq");
    }

    /**
     * Method to return to main menu
     * - Set the time scale to 1
     * - Load the main menu scene
     */
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainMenuScene");
    } 
}