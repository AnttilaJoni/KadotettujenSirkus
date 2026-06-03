using System.IO;
using Cinemachine;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string _saveLocation;


    void Awake()
    {
        _saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        
        if (!GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().playerAlive) 
        {
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().SetPlayerHealth();
            LoadGame();
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().playerAlive = true;
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().TakeDamage(0);
            PauseController.SetPause(false);
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().DialogueState();
            
            Debug.Log("Loaded game after death");
        }
        
        else if (GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().playerAlive && !GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().minigameCompleted) 
        {
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().SetPlayerHealth();
            PauseController.SetPause(false);
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().DialogueState();
            Debug.Log("Set player health");
            
        }

        else if (GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().minigameCompleted) 
        {
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().TakeDamage(0);
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().LoadPlayerPosition();
            PauseController.SetPause(false);
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().DialogueState();
            
            Debug.Log("Minigame completed");
        }
        
    }

    void Start()
    {
        
        
        //SaveGame();
    }


    public void SaveGame()
    {
        SaveData saveData = new SaveData()
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            playerHealth = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().playerHealth,
            mapBoundary = FindFirstObjectByType<CinemachineConfiner2D>().m_BoundingShape2D.gameObject.name,
            
            boss1dialogue = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss1dialogue,
            boss2dialogue = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss2dialogue,
            boss3dialogue = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss3dialogue,
            boss4dialogue = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss4dialogue,
            
            boss1Completed = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss1Completed,
            boss2Completed = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss2Completed,
            boss3Completed = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss3Completed,
            boss4Completed = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss4Completed,
            
            key1 = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().key1,
            key2 = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().key1,
            key3 = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().key1
            
        };
        
        File.WriteAllText(_saveLocation, JsonUtility.ToJson(saveData));
        
        Debug.Log("Game saved");
    }

    public void LoadGame()
    {
        if (File.Exists(_saveLocation)) 
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_saveLocation));
            
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().playerHealth = saveData.playerHealth;
            FindFirstObjectByType<CinemachineConfiner2D>().m_BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
            
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss1dialogue = saveData.boss1dialogue;
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss2dialogue = saveData.boss2dialogue;
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss3dialogue = saveData.boss3dialogue;
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss4dialogue = saveData.boss4dialogue;
            
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss1Completed = saveData.boss1Completed;
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss2Completed = saveData.boss2Completed;
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss3Completed = saveData.boss3Completed;
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss4Completed = saveData.boss4Completed;
            
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().key1 = saveData.key1;
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().key2 = saveData.key2;
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().key3 = saveData.key3;
            
            Debug.Log("Game loaded");
        }

        else 
        {
            SaveGame();
        }
        
        
    }

    public void DeleteSave()
    {
        try {
            if (File.Exists(_saveLocation)) {
                File.Delete(_saveLocation);
                Debug.Log("Save file deleted");
            }

            else {
                Debug.Log("No save file found");
            }
        }

        catch (DirectoryNotFoundException) {
            Debug.Log("File not found");
        }
    }
}
