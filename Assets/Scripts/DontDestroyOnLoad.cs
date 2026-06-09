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
    [SerializeField] private GameObject heartImage;
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

    public int gameEnding = 0; // 0 = bad, 1 == neutral, 2 == good
    
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
        
        //heartImage = Resources.Load("HeartImage") as Image;
        
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainScene") {
            GameObject.FindGameObjectWithTag("SaveController")
                .GetComponent<SaveController>().SaveGame();
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainScene") {
            healthText.gameObject.SetActive(false);
            heartImage.gameObject.SetActive(false);
        }
        
        
    }

    public void SetPlayerHealth()
    {
        playerHealth = playerMaxHealth;
        healthText.text = playerHealth.ToString();
        healthText.gameObject.SetActive(true);
        heartImage.gameObject.SetActive(true);
    }
    
    public void ChangePlayerHealth(int healthChange)
    {
        playerHealth = healthChange;
        healthText.text = playerHealth.ToString();
        healthText.gameObject.SetActive(true);
        heartImage.gameObject.SetActive(true);
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
            //mapBoundary = FindFirstObjectByType<CinemachineConfiner2D>().m_BoundingShape2D.gameObject.name;

            GameObject.Find("PlayerCam").GetComponent<CinemachineConfiner2D>()
                    .m_BoundingShape2D =
                GameObject.Find(mapBoundary).GetComponent<PolygonCollider2D>();
                
                
            //FindFirstObjectByType<CinemachineConfiner2D>().m_BoundingShape2D.gameObject.name = mapBoundary;
            GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>().SetPlayerPosition(playerPosition);
            Debug.Log("Loaded player pos");
            minigameCompleted = false;
            
            DialogueState();
            
        }
    }

    public void DialogueState()
    {
        if (SceneManager.GetActiveScene().name == "MainScene") {
            if (boss1Completed) {
                GameObject.Find("DDR - Colombina").transform.GetChild(0).gameObject
                    .SetActive(false);
                GameObject.Find("DDR - Colombina").transform.GetChild(1).gameObject
                    .SetActive(true);
                LockState();
            }
            else {
                GameObject.Find("DDR - Colombina").transform.GetChild(0).gameObject
                    .SetActive(true);
            }

            if (boss2Completed) {
                GameObject.Find("Muistipeli - Moretta").transform.GetChild(0)
                    .gameObject.SetActive(false);
                GameObject.Find("Muistipeli - Moretta").transform.GetChild(1)
                    .gameObject.SetActive(true);
                LockState();
            }
            else {
                GameObject.Find("Muistipeli - Moretta").transform.GetChild(0)
                    .gameObject.SetActive(true);
            }

            if (boss3Completed) {
                GameObject.Find("Stacking - Arlecchino").transform.GetChild(0)
                    .gameObject.SetActive(false);
                GameObject.Find("Stacking - Arlecchino").transform.GetChild(1)
                    .gameObject.SetActive(false);
                LockState();
            }
            else {
                GameObject.Find("Stacking - Arlecchino").transform.GetChild(0)
                    .gameObject.SetActive(true);
            }
            
            

            if (boss1Completed && boss2Completed && boss3Completed) {
                
                // Disable boss door collider
                GameObject.Find("Lukot").gameObject.SetActive(false);
                GameObject.Find("Final Boss - Tirehtööri").transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                //GameObject.Find("NPC Final Boss").gameObject.SetActive(true);
                GameObject.Find("Final Boss - Tirehtööri").transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    void LockState()
    {
        if (key1) {
            GameObject.Find("Lukot").transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        }

        if (key2) {
            GameObject.Find("Lukot").transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        }

        if (key3) {
            GameObject.Find("Lukot").transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        }


    }

    public void SetEndingState(int ending)
    {
        gameEnding = ending;
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        healthText.text = playerHealth.ToString();
        
        if (playerHealth <= 0) 
        {
            playerAlive = false;
            AudioManager.musicSource.Stop();
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
            
            minigameCompleted = true;
            SceneManager.LoadScene("MainScene");
        }
        
        else if (id == 2) {
            boss2Completed = true;
            key2 = true;
            
            minigameCompleted = true;
            Debug.Log("Memory game completed");
            SceneController.instance.ChangeSceneByIndex(7);
        }
        
        else if (id == 3) {
            boss3Completed = true;
            
            minigameCompleted = true;
            SceneManager.LoadScene("MainScene");
            
            key3 = true;
        }
        
        else if (id == 4) {
            boss4Completed = true;
            
            minigameCompleted = true;
            SceneManager.LoadScene("Endings");

        }
        
        
    }
}
