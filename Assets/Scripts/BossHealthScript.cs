using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealthScript : MonoBehaviour
{
    [SerializeField] private GameObject bossHealtBar;
    [SerializeField] private GameObject bossHealthBarFill;
    
    public float bossHealth;
    public float bossMaxHealth = 100;

    public int bossPhase = 1;
    
    void Start()
    {
        bossHealth = bossMaxHealth;
        bossHealtBar.GetComponent<Slider>().maxValue = bossMaxHealth;
        bossHealtBar.GetComponent<Slider>().value = bossHealth;
    }

    
    void Update()
    {
        UpdateBossHealth();
        
        if (bossHealtBar.GetComponent<Slider>().value < bossMaxHealth / 3) 
        {
            bossHealthBarFill.GetComponent<Image>().color = Color.yellow;
            bossPhase = 3;
        }
        
        else if (bossHealtBar.GetComponent<Slider>().value < ((bossMaxHealth / 3) * 2) && bossHealtBar.GetComponent<Slider>().value > bossMaxHealth / 3) 
        {
            bossHealthBarFill.GetComponent<Image>().color = Color.orange;
            bossPhase = 2;
        }

        else {
            bossHealthBarFill.GetComponent<Image>().color = Color.red;
            bossPhase = 1;
        }
        
    }

    void UpdateBossHealth()
    {
        bossHealth -= Time.deltaTime;
        bossHealtBar.GetComponent<Slider>().value = bossHealth;

        if (bossHealth <= 0) 
        {
            SceneManager.LoadScene("Menu");
        }
    }
    
    
}
