using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class EndingSelector : MonoBehaviour
{
    public GameObject neutral;
    public GameObject good;
    private string _saveLocation;
    
    void Start()
    {
        _saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        if(GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().gameEnding == 2) //<-- Good ending saveState
        {
            good.SetActive(true);
            DeleteSaveData();

        }
        else
        {
            neutral.SetActive(true);
            DeleteSaveData();
        }
    }

    void DeleteSaveData()
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
