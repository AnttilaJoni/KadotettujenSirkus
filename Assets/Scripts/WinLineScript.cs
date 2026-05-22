using System;
using TMPro;
using UnityEngine;

public class WinLineScript : MonoBehaviour
{
    [SerializeField] private Minigame2 minigame2;
    [SerializeField] private TextMeshProUGUI timerText;
    public float _time  = 5f;
    public float timeToWin = 0f;

    void Update()
    {
        if (!minigame2.minigameActive) 
        {
            timerText.gameObject.SetActive(true);
            timerText.text = _time.ToString("00");
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Shape")) {
            
            _time -= Time.deltaTime;

            if (_time <= 4.5f) {
                minigame2.minigameActive = false;
            }

            if (_time <= 0) {
                Time.timeScale = 0f;
                timerText.text = "You win!";
            }


        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Shape")) {
            _time = 5f;
        }
    }
}
