using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownScript : MonoBehaviour
{
    //[SerializeField] private Shooter shooter;
    [SerializeField] private TextMeshProUGUI startCountDownText;
    
    
    
    public bool gameActive = false;
    void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator StartGame()
    {
        startCountDownText.gameObject.SetActive(true);
        startCountDownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        
        startCountDownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        
        startCountDownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        
        startCountDownText.text = "Go!";
        yield return new WaitForSecondsRealtime(1f);
        startCountDownText.gameObject.SetActive(false);
        gameActive = true;
        Time.timeScale = 1;
    }
}
