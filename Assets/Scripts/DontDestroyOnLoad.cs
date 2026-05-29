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
    
    public bool minigame1Completed;
    public bool minigame2Completed;
    public bool minigame3Completed;
    public bool bossFightCompleted;

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
            minigame1Completed = true;
        }
        
        else if (id == 2) {
            minigame2Completed = true;
        }
        
        else if (id == 3) {
            minigame3Completed = true;
        }
        
        else if (id == 4) {
            bossFightCompleted = true;
        }
        
        minigameCompleted = true;
        SceneManager.LoadScene("MainScene");
    }
}
