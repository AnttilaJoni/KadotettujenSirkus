using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PlayButton : MonoBehaviour
{
    public Button playButton;
    private bool playPressed;

    void Start()
    {
        playPressed = false;
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
        if(playPressed)
        {
            return;
        } 
        else
        {
            SceneController.instance.ChangeScene("MainScene");
            playPressed = true;
        }
        
    }
}
