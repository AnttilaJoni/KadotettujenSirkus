using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Minigame01 : MonoBehaviour
{
    [SerializeField] private GameObject notesSpawnPoint;
    [SerializeField] private GameObject notes;
    [SerializeField] private GameObject goal;
    private GameObject _playerStats;
    
    public List<GameObject> notesList = new List<GameObject>();
    
    [Header("Notes")]
    
    public float interval = 3f;
    public float speedMultiplier = 1f;
    private float _time;
    private float _noteTime = 0.5f;
    public int damage = 1;
    
    [Header("Scoring")]
    public int points = 1;
    public int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    void Start()
    {
        _playerStats = GameObject.FindGameObjectWithTag("PlayerStats");
        
        _time = 0f;

        score = 0;
        scoreText.text = score.ToString();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
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
                }
            }
            
            else {
                Debug.Log("Note W not hit");
                _playerStats.GetComponent<DontDestroyOnLoad>().TakeDamage(damage);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.A)) 
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
                }
                
                else {
                    Debug.Log("Note A not hit");
                    _playerStats.GetComponent<DontDestroyOnLoad>().TakeDamage(damage);
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.S)) 
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
                }
                
                else {
                    Debug.Log("Note S not hit");
                    _playerStats.GetComponent<DontDestroyOnLoad>().TakeDamage(damage);
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.D)) 
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
                }

                else {
                    Debug.Log("Note D not hit");
                    _playerStats.GetComponent<DontDestroyOnLoad>().TakeDamage(damage);
                }
            }
            
            
        }
        
        _time += Time.deltaTime;
        
        while (_time >= interval) {
            SpawnNotes();
            _time -= interval;
        }
    }

    void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = " Score: " + score.ToString();
    }

    void SpawnNotes()
    {
        var note =  Instantiate(notes.gameObject, notesSpawnPoint.transform.position, Quaternion.identity);
        
        note.gameObject.SetActive(true);
        int randomInt = Random.Range(0, 4);
        
        note.transform.GetChild(randomInt).gameObject.SetActive(true);

        if (randomInt == 0) {
            note.gameObject.name = "W";
        }
        
        else if (randomInt == 1) {
            note.gameObject.name = "A";
        }
        
        else if (randomInt == 2) {
            note.gameObject.name = "S";
        }

        else if (randomInt == 3) {
            note.gameObject.name = "D";
        }
        
        notesList.Add(note);
    }    
    
    
}
