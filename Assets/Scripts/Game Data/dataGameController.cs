using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class dataGameController : MonoBehaviour
{
    // Variables
    public GameObject mainCharacter;
    public string saveFile;
    public dataGameScript dataGameScript = new dataGameScript();

    /*
     * Health, stamina and mana bar
     */
    public GameObject healthBar;
    public GameObject staminaBar;
    public GameObject manaBar;
    public GameObject moneyHUD;

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
     */
    public void getData() {
        if (File.Exists(saveFile)) {
            string json = File.ReadAllText(saveFile);
            dataGameScript = JsonUtility.FromJson<dataGameScript>(json);
            mainCharacter.transform.position = dataGameScript.mainCharacterPosition;
            mainCharacter.GetComponent<mainCharacterScript>().mana = dataGameScript.mana;
            mainCharacter.GetComponent<mainCharacterScript>().health = dataGameScript.health;
            mainCharacter.GetComponent<mainCharacterScript>().stamina = dataGameScript.stamina;
            mainCharacter.GetComponent<mainCharacterScript>().money = dataGameScript.money;
        }
        else
        {
            Debug.Log("No se ha encontrado el archivo de guardado");
        }
    }

    /**
     * Method to set the data
        * - Set the data
     */
    private void setData() {
        dataGameScript.mainCharacterPosition = mainCharacter.transform.position;
        dataGameScript.mana = mainCharacter.GetComponent<mainCharacterScript>().mana;
        dataGameScript.health = mainCharacter.GetComponent<mainCharacterScript>().health;
        dataGameScript.stamina = mainCharacter.GetComponent<mainCharacterScript>().stamina;
        dataGameScript.money = mainCharacter.GetComponent<mainCharacterScript>().money;
        string json = JsonUtility.ToJson(dataGameScript);
        File.WriteAllText(saveFile, json);  
    }

    private void setStamina() {
        dataGameScript.stamina = mainCharacter.GetComponent<mainCharacterScript>().stamina;
        staminaBar.GetComponent<Image>().fillAmount = dataGameScript.stamina / 10f;
    }

    private void setHealth() {
        dataGameScript.health = mainCharacter.GetComponent<mainCharacterScript>().health;
        healthBar.GetComponent<Image>().fillAmount = dataGameScript.health / 10f;
    }

    private void setMana() {
        dataGameScript.mana = mainCharacter.GetComponent<mainCharacterScript>().mana;
        manaBar.GetComponent<Image>().fillAmount = dataGameScript.mana / 10f;
    }

    private void setMoney() {
        dataGameScript.money = mainCharacter.GetComponent<mainCharacterScript>().money;
        moneyText.text = dataGameScript.money.ToString("0000");
    }
}
