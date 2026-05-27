using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    public GameObject interactionIcon;
    public GameObject talkIcon;

    private bool inInteractRange;

    [Header ("References")]
    [SerializeField] private PlayerInputHandler playerInputHandler;
    
    //private InputSystem_Actions inputSystem_Actions;

    //private PlayerInput _playerInput;

    private void Awake()
    {
        //inputSystem_Actions = new InputSystem_Actions();
        //_playerInput = GetComponent<PlayerInput>();
    }
    void Start()
    {
        interactionIcon.SetActive(false);
        talkIcon.SetActive(false);
        inInteractRange = false;

    }
    void Update()
    {
        if(inInteractRange && playerInputHandler.interactAction.WasPressedThisFrame())
            {
                OpenInteract();
            }
    }
        
    /* private void OnEnable()
    {
        _playerInput.actions["Interact"].performed += DoInteract;
        //inputSystem_Actions.Enable();
        //inputSystem_Actions.Player.Interact.performed += OpenInteract;
    }
    private void OnDisable()
    {
        _playerInput.actions["Interact"].performed -= DoInteract;
        //inputSystem_Actions.Disable();
        //inputSystem_Actions.Player.Interact.performed -= OpenInteract;
    }
    public void DoInteract(InputAction.CallbackContext context)
    {
    Debug.Log("Interact");
    } */
    

    public void OpenInteract()
    {
        Debug.Log("Interact");
            interactableInRange?.Interact();
            if(!interactableInRange.CanInteract())
            {
                talkIcon.SetActive(false);
                interactionIcon.SetActive(false);
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            inInteractRange = true;
            if (collision.gameObject.CompareTag("NPC") || collision.gameObject.CompareTag("Minigame_1_start") 
                || collision.gameObject.CompareTag("Minigame_2_start") || collision.gameObject.CompareTag("Minigame_3_start")
                || collision.gameObject.CompareTag("Boss_start")) 
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
            inInteractRange = false;
            interactableInRange = null;
            talkIcon.SetActive(false);
            interactionIcon.SetActive(false);

            
        }
    }
}
