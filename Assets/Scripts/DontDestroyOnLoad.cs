using System;
using System.IO;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField] private GameObject playerHealthBar;
    [SerializeField] private Sprite fullHeartImage;
    [SerializeField] private Sprite halfHeartImage;
    [SerializeField] private Sprite emptyHeartImage;
    public Vector3 playerPosition;
    
    public int playerHealth = 0;
    public int playerMaxHealth = 5;

    public TextMeshProUGUI healthText;
    
    static bool _onceCall;

    public GameObject player;
    
    public string mapBoundary;

    public bool boss1dialogue = false;
    public bool boss2dialogue = false;
    public bool boss3dialogue = false;
    public bool boss4dialogue = false;
    
    public bool boss1Completed = false;
    public bool boss2Completed = false;
    public bool boss3Completed = false;
    public bool boss4Completed = false;
    
    public bool key1 = false;
    public bool key2 = false;
    public bool key3 = false;
    
    public bool playerAlive = true;

    public bool minigameCompleted = false;
    
    private void Awake()
    {
        SetPlayerHealth();

        if (!_onceCall) {
            DontDestroyOnLoad(this);
            _onceCall = true;
            
            playerHealth = playerMaxHealth;
            healthText.text = playerHealth.ToString();
            
        }
        else {
            Destroy(gameObject);
        }
        
    }

    void Start()
    {
        
        GameObject.FindGameObjectWithTag("SaveController").GetComponent<SaveController>().SaveGame();
    }

    public void SetPlayerHealth()
    {
        playerHealth = playerMaxHealth;
        healthText.text = playerHealth.ToString();
    }
    
    public void SavePlayerPosition()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        mapBoundary = FindFirstObjectByType<CinemachineConfiner2D>().m_BoundingShape2D.gameObject.name;
    }

    public void LoadPlayerPosition()
    {
        if (SceneManager.GetActiveScene().name == "MainScene") 
        {
            //playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            mapBoundary = FindFirstObjectByType<CinemachineConfiner2D>().m_BoundingShape2D.gameObject.name;
            minigameCompleted = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>().SetPlayerPosition(playerPosition);
            Debug.Log("Loaded player pos");

            if (boss1Completed) {
                GameObject.Find("NPC (1)").gameObject.SetActive(false);    
            }
            else {
                GameObject.Find("NPC (1)").gameObject.SetActive(true);
            }
            
            if (boss2Completed) {
                GameObject.Find("NPC (2)").gameObject.SetActive(false);    
            }
            else {
                GameObject.Find("NPC (2)").gameObject.SetActive(true);
            }
            
            if (boss2Completed) {
                GameObject.Find("NPC (3)").gameObject.SetActive(false);    
            }
            else {
                GameObject.Find("NPC (3)").gameObject.SetActive(true);
            }
            
            if (boss4Completed) {
                GameObject.Find("NPC (4)").gameObject.SetActive(false);    
            }
            else {
                GameObject.Find("NPC (4)").gameObject.SetActive(true);
            }
            
            
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        healthText.text = playerHealth.ToString();
        
        if (playerHealth <= 0) 
        {
            playerAlive = false;
            SceneManager.LoadScene("MainScene");
        }
    }

    public void GameOver()
    {
        if (SceneManager.GetActiveScene().name == "Teemu") 
        {
            SceneManager.LoadScene("TestSceneJoni");
            playerHealth = playerMaxHealth;
            healthText.text = "Health: " + playerHealth.ToString();
        }
        
        else if (SceneManager.GetActiveScene().name == "Teemu2") 
        {
            SceneManager.LoadScene("TestSceneJoni");
            playerHealth = playerMaxHealth;
            healthText.text = "Health: " + playerHealth.ToString();
        }
        
        else if (SceneManager.GetActiveScene().name == "Teemu3") 
        {
            SceneManager.LoadScene("TestSceneJoni");
            playerHealth = playerMaxHealth;
            healthText.text = "Health: " + playerHealth.ToString();
        }
        
        else if (SceneManager.GetActiveScene().name == "Teemu4") 
        {
            SceneManager.LoadScene("MainScene");
            playerHealth = playerMaxHealth;
            healthText.text = "Health: " + playerHealth.ToString();
        }
        
    }

    public void MinigameCompleted(int id)
    {
        if (id == 1) {
            boss1Completed = true;
            key1 = true;
        }
        
        else if (id == 2) {
            boss2Completed = true;
            key2 = true;
        }
        
        else if (id == 3) {
            boss3Completed = true;
            key3 = true;
        }
        
        else if (id == 4) {
            boss4Completed = true;
        }
        
        minigameCompleted = true;
        SceneManager.LoadScene("MainScene");
    }
}
