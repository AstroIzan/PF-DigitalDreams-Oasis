using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGameScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("gameScene");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("mainMenuScene");
    }

    public void openModalAlertGame()
    {
        GameObject.Find("ModalAlertGame").SetActive(true);
    }

    public void closeModalAlertGame()
    {
        GameObject.Find("ModalAlertGame").SetActive(false);
    }

    public void acceptModalAlertGame()
    {
        GameObject.Find("ModalAlertGame").SetActive(false);
        Debug.Log("");
    }
}
