using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject pauseMenuButton;
    private GameObject[] cards;
    

    void Start()
    {
        menuCanvas.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!menuCanvas.activeSelf && PauseController.IsGamePaused)
            {
                return;
            }
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            PauseController.SetPause(menuCanvas.activeSelf);
            EventSystem.current.SetSelectedGameObject(pauseMenuButton);
        }
    }

    public void CloseMenu()
    {
        if(!menuCanvas.activeSelf && PauseController.IsGamePaused)
            {
                return;
            }
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            PauseController.SetPause(menuCanvas.activeSelf);
            
    }
    public void Quit()
    {
        if(!menuCanvas.activeSelf && PauseController.IsGamePaused)
        {
            return;
        }
        menuCanvas.SetActive(!menuCanvas.activeSelf);
        PauseController.SetPause(menuCanvas.activeSelf);
        SceneController.instance.ChangeSceneByIndex(0);
    }
}
