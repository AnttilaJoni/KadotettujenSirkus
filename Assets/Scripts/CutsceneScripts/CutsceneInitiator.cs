using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneInitiator : MonoBehaviour
{
    private CutsceneHandler cutsceneHandler;
    private bool playPressed;

    public void Start()
    {
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
