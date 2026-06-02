using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PlayButton : MonoBehaviour
{
    public Button playButton;
    void Start()
    {
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
