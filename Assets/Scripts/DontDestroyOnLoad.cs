using System;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    public Vector3 playerPosition;
    
    public int playerHealth = 0;
    public int playerMaxHealth = 5;

    [SerializeField] private TextMeshProUGUI healthText;
    
    static bool _onceCall;

    public GameObject player;
    
    public string mapBoundary;
    
    public bool minigame1Completed;
    public bool minigame2Completed;
    public bool minigame3Completed;
    public bool bossFightCompleted;

    public bool playerAlive = true;
    
    private void Awake()
    {
        
        
        if (!_onceCall) 
        {
            DontDestroyOnLoad (this);
            _onceCall = true;
        } 
        else 
        {
            Destroy (gameObject);
        }
        
        if (SceneManager.GetActiveScene().name == "TestSceneJoni") 
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            mapBoundary = FindFirstObjectByType<CinemachineConfiner2D>().m_BoundingShape2D.gameObject.name;
        }
    }
    
    
    void Start()
    {
        playerHealth = playerMaxHealth;
        healthText.text = "Health: " + playerHealth.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePlayerPosition()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        mapBoundary = FindFirstObjectByType<CinemachineConfiner2D>().m_BoundingShape2D.gameObject.name;
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        healthText.text = "Health: " + playerHealth.ToString();
        
        if (playerHealth <= 0) {
            //GameOver();
            playerAlive = false;
            //SceneManager.LoadScene("Teemu");
            SceneManager.LoadScene("Teemu4");
            playerHealth = playerMaxHealth;
            healthText.text = "Health: " + playerHealth.ToString();
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
            SceneManager.LoadScene("Teemu4");
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
        
        SceneManager.LoadScene("Teemu");
        //SceneManager.LoadScene("TestSceneJoni");

        //player.transform.position = playerPosition;
    }
}
