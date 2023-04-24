using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    private GameObject modalAlertGame;
    private CanvasGroup canvasGroup;

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

    public void continueGame()
    {
        SceneManager.LoadScene("gameScene");
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene("mainMenuScene");
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
        SceneManager.LoadScene("newGameDifficultyScene");
    }
}
