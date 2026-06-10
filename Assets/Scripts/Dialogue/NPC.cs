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
    public bool isVolto;
    public bool isReveal;
    public GameObject _npc;
    public GameObject _npc_dialogue_done;

    [SerializeField] private Animator anim;

    private bool dialogueEnded;
    private bool imgSet = false;
    public bool CanInteract()
    {
        return !isDialogueActive;
    }
    void Update()
    {
        if (isDialogueActive)
        {
            if(imgSet == false)
            {
                if(isBoss_1) 
                {
                    portraitImage_NPC.transform.position = new Vector2(290f, 280f);
                    //portraitImage_NPC.transform.PointsToPixels(new Vector2(290f, 280f)); 
                    imgSet = true;
                }
                else if(isBoss_2) 
                {
                    portraitImage_NPC.transform.position = new Vector3(324f, 310f, 0f);
                    imgSet = true;
                }
                else if(isBoss_3) 
                {
                    portraitImage_NPC.transform.position = new Vector3(240f, 310f, 0f);
                    imgSet = true;
                }
                else if(isBoss_4)
                {
                    portraitImage_NPC.transform.position = new Vector3(200f, 180f, 0f);
                    imgSet = true;
                }
                else if(isReveal)
                {
                    portraitImage_NPC.transform.position = new Vector3(200f, 180f, 0f);
                    imgSet = true;
                }
                else
                {
                    portraitImage_NPC.transform.position = new Vector3(300f, 310f, 0f);
                    imgSet = true;
                }
            }
            portraitImage_NPC.SetNativeSize();
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                NextLine();
            }
        }
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
        dialogueEnded = false;
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
            AudioManager.PlayVoice(dialogueData.voiceSounds_player[0], dialogueData.voicePitch);
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
        if(dialogueEnded == true) return;
        if (isBoss_1) {
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss1dialogue = true;
            GameObject.FindGameObjectWithTag("SaveController").GetComponent<SaveController>().SaveGame();
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().SavePlayerPosition();
            SceneController.instance.ChangeScene("Teemu2");
            dialogueEnded = true;
            imgSet = false;
            portraitImage_NPC.transform.position = new Vector3(300f, 310f, 0f);
        } 
        else if(isBoss_2)
        {
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss2dialogue = true;
            GameObject.FindGameObjectWithTag("SaveController").GetComponent<SaveController>().SaveGame();
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().SavePlayerPosition();
            SceneController.instance.ChangeScene("MemoryGame");
            dialogueEnded = true;
            imgSet = false;
            portraitImage_NPC.transform.position = new Vector3(300f, 310f, 0f);
        } 
        else if(isBoss_3)
        {
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss3dialogue = true;
            GameObject.FindGameObjectWithTag("SaveController").GetComponent<SaveController>().SaveGame();
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().SavePlayerPosition();
            SceneController.instance.ChangeScene("Teemu4");
            dialogueEnded = true;
            imgSet = false;
            portraitImage_NPC.transform.position = new Vector3(300f, 310f, 0f);
        } 
        else if(isBoss_4)
        {
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss4dialogue = true;
            GameObject.FindGameObjectWithTag("SaveController").GetComponent<SaveController>().SaveGame();
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().SavePlayerPosition();
            SceneController.instance.ChangeScene("Teemu3");
            dialogueEnded = true;
            imgSet = false;
            portraitImage_NPC.transform.position = new Vector3(300f, 310f, 0f);
        } 
        else if(isVolto)
        {
            //GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss4dialogue = true;
            if(_npc != null && _npc_dialogue_done != null)
            {
                GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().SetEndingState(2);
                StopAllCoroutines();
                isDialogueActive = false;
                dialogueText.SetText("");
                dialoguePanel.SetActive(false);
                PauseController.SetPause(false);
                dialogueIndex = 0;
                expressionIndex = 0;
                
                _npc.SetActive(false);
                _npc_dialogue_done.SetActive(true);
                dialogueEnded = false;
                dialogueBox.transform.rotation = Quaternion.identity;
                flipped = false;
                imgSet = false;
                portraitImage_NPC.transform.position = new Vector3(300f, 310f, 0f);
            }
        }
        else if(isReveal)
        {
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().boss4dialogue = true;
            if(_npc != null && _npc_dialogue_done != null)
            {
                StopAllCoroutines();
                isDialogueActive = false;
                dialogueText.SetText("");
                dialoguePanel.SetActive(false);
                PauseController.SetPause(false);
                dialogueIndex = 0;
                expressionIndex = 0;
                
                _npc.SetActive(false);
                _npc_dialogue_done.SetActive(true);
                dialogueEnded = false;
                anim.SetTrigger("Reveal");
                dialogueBox.transform.rotation = Quaternion.identity;
                flipped = false;
                imgSet = false;
                portraitImage_NPC.transform.position = new Vector3(300f, 310f, 0f);
            }
        }
        else
        {
            StopAllCoroutines();
            isDialogueActive = false;
            dialogueText.SetText("");
            dialoguePanel.SetActive(false);
            PauseController.SetPause(false);
            dialogueIndex = 0;
            expressionIndex = 0;
            dialogueEnded = false;
            if(_npc != null && _npc_dialogue_done != null)
            {
                _npc.SetActive(false);
                _npc_dialogue_done.SetActive(true);
            }
            dialogueBox.transform.rotation = Quaternion.identity;
            flipped = false;
            imgSet = false;
            portraitImage_NPC.transform.position = new Vector3(300f, 310f, 0f);
            
        }
    }
}