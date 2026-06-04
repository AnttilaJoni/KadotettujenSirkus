using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneInitiator : MonoBehaviour
{
    private CutsceneHandler cutsceneHandler;
    private bool playPressed;
    private string _saveLocation;

    public void Start()
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

        cutsceneHandler = GetComponent<CutsceneHandler>();
        playPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            cutsceneHandler.PlayNextElement();
    }

    public void OnStartGameClicked()
    {
        if(playPressed)
        {
            return;
        } 
        else
        {
            //if(savegameExists) -> SceneController.instance.ChangeScene("MainScene");
            //else ->
            cutsceneHandler.PlayNextElement();
            playPressed = true;
        }
    }
}
