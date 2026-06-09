using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneInitiator : MonoBehaviour
{
    private CutsceneHandler cutsceneHandler;
    private bool playPressed;
    private bool endingStarted;
    private string _saveLocation;

    public void Start()
    {
        cutsceneHandler = GetComponent<CutsceneHandler>();
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == $"Endings" && !endingStarted)
        {   
            StartCoroutine(OnEndingStart());
            endingStarted = true;
        }

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
        playPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            cutsceneHandler.PlayNextElement();
    }
    private IEnumerator OnEndingStart()
    {
        yield return new WaitForSeconds(2f);
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
            if(File.Exists(_saveLocation))
            {
                SceneController.instance.ChangeScene("MainScene");
                playPressed = true;
            }
            else
            {
                cutsceneHandler.PlayNextElement();
                playPressed = true;
            }
        }
    }
}
