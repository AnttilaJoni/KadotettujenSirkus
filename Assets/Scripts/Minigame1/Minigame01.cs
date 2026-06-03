using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Minigame01 : MonoBehaviour
{
    [SerializeField] private Animator comboAnim;
    [SerializeField] private TextMeshProUGUI startCountDownText;
    [SerializeField] private TextMeshProUGUI comboCounterText;
    [SerializeField] private GameObject comboCounterImage;
    [SerializeField] private GameObject bossHealthBar;
    [SerializeField] private GameObject notesSpawnPoint;
    [SerializeField] private GameObject notes;
    [SerializeField] private GameObject goal;
    public int playerHealth = 10;
    
    private GameObject _playerStats;
    
    public List<GameObject> notesList = new List<GameObject>();
    
    [Header("Notes")]
    
    public float interval = 3f;
    public float speedMultiplier = 1f;
    private float _time;
    private float _noteTime = 0.75f;
    public int damage = 1;
    
    [Header("Scoring")]
    public int comboCounter = 0;
    public int points = 1;
    public int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    public int bossHealth;
    public int bossMaxHealth;

    public bool gameActive = false;
    void Start()
    {
        StartCoroutine(StartGame());
        
        _playerStats = GameObject.FindGameObjectWithTag("PlayerStats");
        
        _time = 0f;

        score = 0;
        scoreText.text = "score: " + score.ToString();
        bossHealth = bossMaxHealth;
        
        comboCounterText.text = comboCounter.ToString();
        
        GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().ChangePlayerHealth(playerHealth);

    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            if (notesList.Count > 0) 
            {
                if (notesList[0].transform.position.y > goal.transform.position.y - _noteTime &&
                    notesList[0].transform.position.y < goal.transform.position.y + _noteTime && notesList[0].gameObject.name == "W") 
                {
                    var note = notesList[0];
                    notesList.Remove(notesList[0]);
                    Destroy(note);
                    Debug.Log("Note W hit");
                    AddScore(points);
                    AddCombo(points);
                }
            }
            
            else {
                Debug.Log("Note W not hit");
                _playerStats.GetComponent<DontDestroyOnLoad>().TakeDamage(damage);
                ResetCombo();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            if (notesList.Count > 0) 
            {
                if (notesList[0].transform.position.y > goal.transform.position.y - _noteTime &&
                    notesList[0].transform.position.y < goal.transform.position.y + _noteTime && notesList[0].gameObject.name == "A") 
                {
                    var note = notesList[0];
                    notesList.Remove(notesList[0]);
                    Destroy(note);
                    Debug.Log("Note A hit");
                    AddScore(points);
                    AddCombo(points);
                }
                
                else {
                    Debug.Log("Note A not hit");
                    _playerStats.GetComponent<DontDestroyOnLoad>().TakeDamage(damage);
                    ResetCombo();
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            if (notesList.Count > 0) 
            {
                if (notesList[0].transform.position.y > goal.transform.position.y - _noteTime &&
                    notesList[0].transform.position.y < goal.transform.position.y + _noteTime && notesList[0].gameObject.name == "S") 
                {
                    var note = notesList[0];
                    notesList.Remove(notesList[0]);
                    Destroy(note);
                    Debug.Log("Note S hit");
                    AddScore(points);
                    AddCombo(points);
                }
                
                else {
                    Debug.Log("Note S not hit");
                    _playerStats.GetComponent<DontDestroyOnLoad>().TakeDamage(damage);
                    ResetCombo();
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            if (notesList.Count > 0) 
            {
                if (notesList[0].transform.position.y > goal.transform.position.y - _noteTime &&
                    notesList[0].transform.position.y < goal.transform.position.y + _noteTime && notesList[0].gameObject.name == "D") 
                {
                    var note = notesList[0];
                    notesList.Remove(notesList[0]);
                    Destroy(note);
                    Debug.Log("Note D hit");
                    AddScore(points);
                    AddCombo(points);
                }

                else {
                    Debug.Log("Note D not hit");
                    _playerStats.GetComponent<DontDestroyOnLoad>().TakeDamage(damage);
                    ResetCombo();
                }
            }
            
            
        }


        if (gameActive) 
        {
            _time += Time.deltaTime;

            while (_time >= interval) {
                SpawnNotes();
                _time -= interval;
            }
        }
    }

    void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = " Score: " + score.ToString();
        
        BossTakeDamage(scoreToAdd * 2);
    }

    void SpawnNotes()
    {
        var note =  Instantiate(notes.gameObject, notesSpawnPoint.transform.position, Quaternion.identity);
        
        note.gameObject.SetActive(true);
        int randomInt = Random.Range(0, 4);
        
        note.transform.GetChild(randomInt).gameObject.SetActive(true);

        if (randomInt == 0) {
            note.gameObject.name = "A";
        }
        
        else if (randomInt == 1) {
            note.gameObject.name = "W";
        }
        
        else if (randomInt == 2) {
            note.gameObject.name = "S";
        }

        else if (randomInt == 3) {
            note.gameObject.name = "D";
        }
        
        notesList.Add(note);
    }

    void BossTakeDamage(int takeDamage)
    {
        bossHealth -= takeDamage;
        bossHealthBar.GetComponent<Slider>().value = bossHealth;

        if (bossHealth <= 0) {
            //Time.timeScale = 0f;
            _playerStats.GetComponent<DontDestroyOnLoad>().MinigameCompleted(1);
        }
    }

    private IEnumerator StartGame()
    {
        startCountDownText.gameObject.SetActive(true);
        startCountDownText.text = "3";
        yield return new WaitForSeconds(1f);
        
        startCountDownText.text = "2";
        yield return new WaitForSeconds(1f);
        
        startCountDownText.text = "1";
        yield return new WaitForSeconds(1f);
        
        startCountDownText.text = "Go!";
        yield return new WaitForSeconds(1f);
        startCountDownText.gameObject.SetActive(false);
        gameActive = true;
    }

    void AddCombo(int scoreToAdd)
    {
        comboCounter += scoreToAdd;
        comboCounterText.text = comboCounter.ToString();
        comboAnim.SetTrigger("Combo");
        

        if (comboCounter < 10) {
            comboCounterImage.GetComponent<RawImage>().color = Color.white;
        }
        
        else if (comboCounter >= 10 && comboCounter < 20) {
            comboCounterImage.GetComponent<RawImage>().color = Color.orange;
        }
        
        else if (comboCounter >= 20) {
            comboCounterImage.GetComponent<RawImage>().color = Color.red;
        }
    }

    void ResetCombo()
    {
        comboCounter = 0;
        comboCounterText.text = comboCounter.ToString();
        comboCounterImage.GetComponent<RawImage>().color = Color.orange;
    }
    
}
