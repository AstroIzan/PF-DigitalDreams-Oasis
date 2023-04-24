using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class difficultMenuMode : MonoBehaviour
{
    public void adventurerMode()
    {
        Debug.Log("Adventurer Mode");
    }

    public void survivorMode()
    {
        Debug.Log("Survivor Mode");
    }

    public void godlikeMode()
    {
        Debug.Log("Godlike Mode");
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("gameStartScene");
    }
}
