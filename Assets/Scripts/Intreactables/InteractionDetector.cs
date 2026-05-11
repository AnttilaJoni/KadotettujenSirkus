using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    public GameObject interactionIcon;
    public GameObject talkIcon;
    
    private InputSystem_Actions inputSystem_Actions;

    private void Awake()
    {
        inputSystem_Actions = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        inputSystem_Actions.Enable();
        inputSystem_Actions.Player.Interact.performed += OpenInteract;
    }
    private void OnDisable()
    {
        inputSystem_Actions.Disable();
        inputSystem_Actions.Player.Interact.performed -= OpenInteract;
    }

    void Start()
    {
        interactionIcon.SetActive(false);
        talkIcon.SetActive(false);

    }

    public void OpenInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactableInRange?.Interact();
            if(!interactableInRange.CanInteract())
            {
                talkIcon.SetActive(false);
                interactionIcon.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            if (collision.gameObject.CompareTag("NPC")) 
            {
                talkIcon.SetActive(true);
            }
            else 
            {
                interactionIcon.SetActive(true);
            }


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            talkIcon.SetActive(false);
            interactionIcon.SetActive(false);

            
        }
    }
}
