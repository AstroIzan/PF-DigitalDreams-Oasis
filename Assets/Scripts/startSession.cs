using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startSession : MonoBehaviour
{
    private GameObject modalAlertSession;
    private CanvasGroup canvasGroup;

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

    public void continueGame()
    {
        SceneManager.LoadScene("gameScene");
    }

    public void exitGame() 
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void openModalAlertGame()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void closeModalAlertGame()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void acceptModalAlertGame()
    {
        SceneManager.LoadScene("mainMenuScene");
    }
}
