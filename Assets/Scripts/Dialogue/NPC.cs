using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public GameObject dialogueBox;
    private bool flipped;
    public TMP_Text dialogueText, nameText, nameTextNPC;
    public Image portraitImage;
    public Image portraitImage_NPC;
    public Button closeDialogue;

    private int dialogueIndex;
    private int expressionIndex;
    private bool isTyping, isDialogueActive;

    public bool isBoss_1;
    public bool isBoss_2;
    public bool isBoss_3;
    public bool isBoss_4;

    public bool CanInteract()
    {
        return !isDialogueActive;
    }
    public void Interact()
    {
        if(dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
        {
            return;
        }
        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            dialogueIndex = 0;
            expressionIndex = 0;
            DetermineSpeaker();
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;   

        Button btn = closeDialogue.GetComponent<Button>();
		btn.onClick.AddListener(EndDialogue);

        dialoguePanel.SetActive(true);
        PauseController.SetPause(true);

        StartCoroutine(TypeLine());
    }

    public void DetermineSpeaker()
    {
        if(dialogueData.isPlayerLine.Length > dialogueIndex && dialogueData.isPlayerLine[dialogueIndex])
        {
            dialogueBox.transform.rotation = Quaternion.identity;
            flipped = false;

            portraitImage.GetComponent<Image>().color = new Color(1f,1f,1f,1.0f);
            portraitImage_NPC.GetComponent<Image>().color = new Color(1f,1f,1f,0.0f);

            nameText.SetText(dialogueData.playerName);
            nameTextNPC.SetText("");
            portraitImage.sprite = dialogueData.npcPortrait[expressionIndex];
        } 
        else
        {
            if (!flipped)
            {
                dialogueBox.transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
                flipped = true;
            }
            portraitImage.GetComponent<Image>().color = new Color(1f,1f,1f,0.0f);
            portraitImage_NPC.GetComponent<Image>().color = new Color(1f,1f,1f,1.0f);

            nameText.SetText("");
            nameTextNPC.SetText(dialogueData.npcName);
            portraitImage_NPC.sprite = dialogueData.npcPortrait[expressionIndex];
        }
    }

    void NextLine()
    {
        if(isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if(++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
            ++expressionIndex;

            DetermineSpeaker(); 
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            AudioManager.PlayVoice(dialogueData.voiceSound, dialogueData.voicePitch);
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
        if(dialogueData.autoProgressLines.Length == dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        if (isBoss_1)
        {
            SceneController.instance.ChangeScene("Teemu2");
        } 
        else if(isBoss_2)
        {
            SceneController.instance.ChangeScene("MemoryGame");
        } 
        else if(isBoss_3)
        {
            SceneController.instance.ChangeScene("Teemu4");
        } 
        else if(isBoss_4)
        {
            SceneController.instance.ChangeScene("Teemu3");
        } else
        {
            StopAllCoroutines();
            isDialogueActive = false;
            dialogueText.SetText("");
            dialoguePanel.SetActive(false);
            PauseController.SetPause(false);
            dialogueIndex = 0;
            expressionIndex = 0;
        }
    }
}