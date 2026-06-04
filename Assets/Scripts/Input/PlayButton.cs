using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PlayButton : MonoBehaviour
{
    private string _saveLocation;
    public Button playButton;
    void Start()
    {
        _saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
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
        
        Button btn = playButton.GetComponent<Button>();
		btn.onClick.AddListener(PlayStart);
    }
    void Update()
    {
        if(playButton == Selection.activeObject)
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                PlayStart();
            }
        }
    }

    public void PlayStart()
    {
        SceneController.instance.ChangeScene("MainScene");
    }
}
