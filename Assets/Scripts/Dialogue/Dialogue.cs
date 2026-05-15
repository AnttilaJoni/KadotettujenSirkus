using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    public GameObject dialogue;

    void Start()
    {
        dialogue.SetActive(true);
        textComponent.text = string.Empty;
        StartDialogue();
        
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index ++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            if(dialogue.CompareTag("NPC"))
            {
                LoadCombat1();
            }
            dialogue.SetActive(false);
        }
    }

    private void LoadMain()
    {
        SceneController.instance.ChangeSceneByIndex(1);
    }
    private void LoadCombat1()
    {
        SceneController.instance.ChangeSceneByIndex(2);
    }
    private void LoadCombat2()
    {
        SceneController.instance.ChangeSceneByIndex(3);
    }
    private void LoadBoss()
    {
        SceneController.instance.ChangeSceneByIndex(4);
    }
    
}
