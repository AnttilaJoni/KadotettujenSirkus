using System.Collections.Generic;
using UnityEngine;

public class Minigame01 : MonoBehaviour
{
    [SerializeField] private GameObject notesSpawnPoint;
    [SerializeField] private GameObject notes;
    [SerializeField] private GameObject goal;
    
    public List<GameObject> notesList = new List<GameObject>();
    
    public float interval = 3f;

    private float _time;

    private float _noteTime = 0.5f;
    
    void Start()
    {
        _time = 0f;
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
                }
            }
            
            else {
                Debug.Log("Note W not hit");
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
                }
                
                else {
                    Debug.Log("Note A not hit");
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
                }
                
                else {
                    Debug.Log("Note S not hit");
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
                }

                else {
                    Debug.Log("Note D not hit");
                }
            }
            
            
        }
        
        _time += Time.deltaTime;
        
        while (_time >= interval) {
            SpawnNotes();
            _time -= interval;
        }
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
