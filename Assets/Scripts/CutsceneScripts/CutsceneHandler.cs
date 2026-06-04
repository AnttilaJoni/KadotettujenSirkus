using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneHandler : MonoBehaviour
{
    private CutsceneElementBase[] cutsceneElements;
    private int index = -1;
    private bool gameStarted;

    public void Start()
    {
        cutsceneElements = GetComponents<CutsceneElementBase>();
        gameStarted = false;
    }
    public void Update()
    {
        if (index >= 0)
        {
            if(index < cutsceneElements.Length)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E)) 
                {
                    PlayNextElement();
                }
            }
            
        }
    }
    private void ExecuteCurrentElement()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(index >= 0 && index < cutsceneElements.Length)
        {
            cutsceneElements[index].Execute();
        } 
        else if(gameStarted == false && scene.name == $"Menu")
        {   
            SceneController.instance.ChangeScene("MainScene");
            gameStarted = true;
            //DataPersistenceManager.instance.NewGame();
            //SceneManager.LoadSceneAsync(1);
            
            //Cursor.lockState = CursorLockMode.Locked;
            //Time.timeScale = 1; //pausecontroller not paused?
            //AudioListener.pause = false;
        }
    }

    public void PlayNextElement()
    {
        index++;
        ExecuteCurrentElement();
    }
}
