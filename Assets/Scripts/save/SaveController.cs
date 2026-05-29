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
            
            Debug.Log("Loaded game after death");
        }
        
        else if (GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().playerAlive && !GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().minigameCompleted) 
        {
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().SetPlayerHealth();
            Debug.Log("Set player health");
        }

        else if (GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().minigameCompleted) 
        {
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().TakeDamage(0);
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().LoadPlayerPosition();
            
            
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
            
            minigame1Completed = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().minigame1Completed,
            minigame2Completed = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().minigame2Completed,
            minigame3Completed = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().minigame3Completed,
            bossFightCompleted = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().bossFightCompleted
            
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
            
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().minigame1Completed = saveData.minigame1Completed;
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().minigame2Completed = saveData.minigame2Completed;
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().minigame3Completed = saveData.minigame3Completed;
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().bossFightCompleted = saveData.bossFightCompleted;
            
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
