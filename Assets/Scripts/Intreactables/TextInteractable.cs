using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInteractable : MonoBehaviour, IInteractable
{
    public bool IsInteracted { get; private set;}
    public string InteractID { get; private set;}
    public GameObject dialogue;
    
    public Sprite interactedSprite;

    void Start()
    {
        InteractID ??= GlobalHelper.GenerateUniqueID(gameObject);
        dialogue.SetActive(false);
    }


    public bool CanInteract()
    {
        return !IsInteracted;
    }
    public void Interact()
    {
       if(!CanInteract()) return;
       OpenInteract();
    }


    private void OpenInteract()
    {
        SetOpened(true);
        
    }
    public void SetOpened(bool opened)
    {
        if(IsInteracted = opened)
        {
            GetComponent<SpriteRenderer>().sprite = interactedSprite;
            dialogue.SetActive(true);
        }
    }
}
