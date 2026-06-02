using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public Button playButton;
    void Start()
    {
        Button btn = playButton.GetComponent<Button>();
		btn.onClick.AddListener(PlayStart);
    }

    public void PlayStart()
    {
        SceneController.instance.ChangeScene("MainScene");
    }
}
