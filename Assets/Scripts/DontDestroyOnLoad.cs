using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{

    public int playerHealth = 0;
    public int playerMaxHealth = 5;

    [SerializeField] private TextMeshProUGUI healthText;
    
    static bool _onceCall;
    
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

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        healthText.text = "Health: " + playerHealth.ToString();

        if (playerHealth <= 0) {
            GameOver();
        }
    }

    void GameOver()
    {
        if (SceneManager.GetActiveScene().name == "Teemu") 
        {
            SceneManager.LoadScene("Teemu");
            playerHealth = playerMaxHealth;
            healthText.text = "Health: " + playerHealth.ToString();
        }
        
        else if (SceneManager.GetActiveScene().name == "Teemu2") 
        {
            SceneManager.LoadScene("Teemu2");
            playerHealth = playerMaxHealth;
            healthText.text = "Health: " + playerHealth.ToString();
        }
        
        else if (SceneManager.GetActiveScene().name == "Teemu3") 
        {
            SceneManager.LoadScene("Teemu3");
            playerHealth = playerMaxHealth;
            healthText.text = "Health: " + playerHealth.ToString();
        }
        
        
        
    }
}
