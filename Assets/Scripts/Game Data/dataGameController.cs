using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class dataGameController : MonoBehaviour
{
    // Variables
    public GameObject mainCharacter;
    public string saveFile;
    public dataGameScript dataGameScript = new dataGameScript();

    /**
     * Method to start on the Awake
     * - Get the path of the save file
     * - Get the main character
     * - Get the data
     */
    private void Awake() {
        saveFile = Application.persistentDataPath + "/saveFile.json";
        mainCharacter = GameObject.FindGameObjectsWithTag("Player")[0];
        getData();
    }

    /**
     * Method to update the script
     * - Check if the G key is pressed
         * [IF is pressed] - Set the data
     * - Check if the C key is pressed
         * [IF is pressed] - Get the data
     */
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            setData();
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            getData();
        }
    }

    /**
     * Method to get the data
     * - Check if the file exists
     * - Get the data from the file
     * - Set the main character position
     */
    public void getData() {
        if (File.Exists(saveFile)) {
            string json = File.ReadAllText(saveFile);
            dataGameScript = JsonUtility.FromJson<dataGameScript>(json);
            mainCharacter.transform.position = dataGameScript.mainCharacterPosition;
        }
        else
        {
            Debug.Log("No se ha encontrado el archivo de guardado");
        }
    }

    /**
     * Method to set the data
     * - Get the main character position
     * - Convert the data to json  
     * - Write the data to the file
     */
    private void setData() {
        dataGameScript.mainCharacterPosition = mainCharacter.transform.position;
        string json = JsonUtility.ToJson(dataGameScript);
        File.WriteAllText(saveFile, json);
    }
}
