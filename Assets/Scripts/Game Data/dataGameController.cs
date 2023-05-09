using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class dataGameController : MonoBehaviour
{
    public dataGameScript dataGameScript = new dataGameScript(); // Instance of the dataGameScript
    public string saveFile; // String to store the path of the save file

    private GameObject mainCharacter; // GameObject to store the main character
    private GameObject healthBar; // GameObject to store the health bar
    private GameObject staminaBar; // GameObject to store the stamina bar
    private GameObject manaBar; // GameObject to store the mana bar
    private GameObject moneyHUD; // GameObject to store the money HUD

    private TextMeshProUGUI moneyText;

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
     * Method to start on the Start
     * - Get the money text
     * - Get the bars 
     */
    void Start()
    {
        healthBar = GameObject.Find("HealthHUD");
        healthBar = healthBar.transform.GetChild(0).gameObject;
        staminaBar = GameObject.Find("StaminaHUD");
        staminaBar = staminaBar.transform.GetChild(0).gameObject;
        manaBar = GameObject.Find("ManaHUD");
        manaBar = manaBar.transform.GetChild(0).gameObject;
        
        moneyHUD = GameObject.Find("MoneyHUD");
        moneyText = moneyHUD.GetComponent<TextMeshProUGUI>();
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
        setStamina();
        setHealth();
        setMana();
        setMoney();

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
         * [IF exists] - Get the data from local file and set it to the main character
         * [ELSE] - Show a message
     */
    public void getData() {
        if (File.Exists(saveFile)) {
            string json = File.ReadAllText(saveFile);
            dataGameScript = JsonUtility.FromJson<dataGameScript>(json);
            mainCharacter.transform.position = dataGameScript.mainCharacterPosition;
            mainCharacter.GetComponent<mainCharacterScript>().currentHealth = dataGameScript.currentHealth;
            mainCharacter.GetComponent<mainCharacterScript>().currentStamina = dataGameScript.currentStamina;
            mainCharacter.GetComponent<mainCharacterScript>().currentMana = dataGameScript.currentMana;
            mainCharacter.GetComponent<mainCharacterScript>().currentMoney = dataGameScript.currentMoney;
        }
        else
        {
            Debug.Log("No se ha encontrado el archivo de guardado");
        }
    }

    /**
     * Method to save the data in the file
     * - Get the data of the main character
     * - Convert the data to json
     * - Write the json in the file
     */
    private void setData() {
        dataGameScript.mainCharacterPosition = mainCharacter.transform.position;
        dataGameScript.currentHealth = mainCharacter.GetComponent<mainCharacterScript>().currentHealth;
        dataGameScript.currentStamina = mainCharacter.GetComponent<mainCharacterScript>().currentStamina;
        dataGameScript.currentMana = mainCharacter.GetComponent<mainCharacterScript>().currentMana;
        dataGameScript.currentMoney = mainCharacter.GetComponent<mainCharacterScript>().currentMoney;
        string json = JsonUtility.ToJson(dataGameScript);
        File.WriteAllText(saveFile, json);  
    }

    /**
     * Method to set the position of the main character
     * - Get the position of the main character
     * - Set the position of the main character
     */
    private void setPosition() {
        dataGameScript.mainCharacterPosition = mainCharacter.transform.position;
        mainCharacter.transform.position = dataGameScript.mainCharacterPosition;
    }

    /**
     * Method to set the health
     * - Get the health of the main character
     * - Set the health bar
     */
    private void setHealth() {
        dataGameScript.currentHealth = mainCharacter.GetComponent<mainCharacterScript>().currentHealth;
        healthBar.GetComponent<Image>().fillAmount = dataGameScript.currentHealth / 10f;
    }

    /**
     * Method to set the stamina
     * - Get the stamina of the main character
     * - Set the stamina bar
     */
    private void setStamina() {
        dataGameScript.currentStamina = mainCharacter.GetComponent<mainCharacterScript>().currentStamina;
        staminaBar.GetComponent<Image>().fillAmount = dataGameScript.currentStamina / 10f;
    }

    /**
     * Method to set the mana
     * - Get the mana of the main character
     * - Set the mana bar
     */
    private void setMana() {
        dataGameScript.currentMana = mainCharacter.GetComponent<mainCharacterScript>().currentMana;
        manaBar.GetComponent<Image>().fillAmount = dataGameScript.currentMana / 10f;
    }

    /**
     * Method to set the money
     * - Get the money of the main character
     * - Set the money text
     */
    private void setMoney() {
        dataGameScript.currentMoney = mainCharacter.GetComponent<mainCharacterScript>().currentMoney;
        moneyText.text = dataGameScript.currentMoney.ToString("0000");
    }
}
