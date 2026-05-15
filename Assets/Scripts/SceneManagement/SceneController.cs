using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;
    private bool loading;

    private void Awake()
    {
        loading = false;
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2 && !loading)
        {
            if (Input.GetKey("escape"))
            {
                ChangeSceneByIndex(4);
            }
        }
        
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }
    public void ChangeSceneByIndex(int sceneIndex)
    {
        StartCoroutine(LoadLevelByIndex(sceneIndex));
    }


    IEnumerator LoadLevel(string sceneName)
    {
        loading = true;
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(sceneName);
        transitionAnim.SetTrigger("Start");
        loading = false;
    }
    IEnumerator LoadLevelByIndex(int sceneIndex)
    {
        loading = true;
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        transitionAnim.SetTrigger("Start");
        loading = false;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
