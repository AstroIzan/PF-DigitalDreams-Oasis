using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class startSession : MonoBehaviour
{
        // Variables
    private GameObject modalAlertSession;
    private CanvasGroup canvasGroup;

    public string loginUrl = "http://localhost/PF-DigitalDreams-WebDigitalDreams/html/login.php?location=2"; // URL de inicio de sesión de tu página web


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
        SceneManager.LoadScene("gameScene");
    }

    /**
     * Method to loggin session into dddevelops web.
     */
    public IEnumerator CheckLoginStatus()
    {
        Debug.Log("Comprobando el estado de inicio de sesión...");
        // Envía la solicitud GET al servidor
        UnityWebRequest www = UnityWebRequest.Get(loginUrl);

        // Espera la respuesta del servidor
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error al obtener el estado de inicio de sesión: " + www.error);
        }
        else
        {
            // Obtiene la respuesta del servidor
            string response = www.downloadHandler.text;

            // Analiza la respuesta JSON
            LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(response);

            // Realiza el procesamiento de la respuesta aquí según tus necesidades
            if (loginResponse.status == "success")
            {
                Debug.Log("El usuario " + loginResponse.username + " ha iniciado sesión");
                // Aquí puedes realizar acciones adicionales cuando el usuario ha iniciado sesión en la página web.
            }
            else if (loginResponse.status == "error")
            {
                Debug.Log("Error en el inicio de sesión: " + loginResponse.message);
                // Aquí puedes realizar acciones adicionales cuando se produce un error en el inicio de sesión.
            }
            else
            {
                Debug.Log("Respuesta desconocida del servidor: " + response);
                // Aquí puedes manejar otros casos de respuesta desconocida según tus necesidades.
            }
        }
    }

    /**
     * Simple method to get the json response from the server.
     */
    [System.Serializable]
    public class LoginResponse
    {
        public string status;
        public string message;
        public string username;
    }
}
