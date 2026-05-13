using UnityEngine;
using UnityEngine.UI;

public class BossHealthScript : MonoBehaviour
{
    [SerializeField] private GameObject bossHealtBar;
    [SerializeField] private GameObject bossHealthBarFill;
    
    public float bossHealth;
    public float bossMaxHealth = 100;
    
    void Start()
    {
        bossHealth = bossMaxHealth;
        bossHealtBar.GetComponent<Slider>().maxValue = bossMaxHealth;
        bossHealtBar.GetComponent<Slider>().value = bossHealth;
    }

    
    void Update()
    {
        if (bossHealtBar.GetComponent<Slider>().value < bossMaxHealth / 3) 
        {
            bossHealthBarFill.GetComponent<Image>().color = Color.yellow;
        }
        
        else if (bossHealtBar.GetComponent<Slider>().value < ((bossMaxHealth / 3) * 2) && bossHealtBar.GetComponent<Slider>().value > bossMaxHealth / 3) 
        {
            bossHealthBarFill.GetComponent<Image>().color = Color.orange;
        }

        else {
            bossHealthBarFill.GetComponent<Image>().color = Color.red;
        }
        
        
    }
    
    
}
