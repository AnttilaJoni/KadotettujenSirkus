using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{

    public int playerHealth = 0;
    public int playerMaxHealth = 3;

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
        SceneManager.LoadScene("Teemu2");
        playerHealth = playerMaxHealth;
        healthText.text = "Health: " + playerHealth.ToString();
    }
}
