using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Assets")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name Reference")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string interact = "Interact";

    public InputAction interactAction;

    public bool InteractTriggered { get; private set; }

    private void Awake()
    {
        InputActionMap mapReference = playerControls.FindActionMap(actionMapName);
        interactAction = mapReference.FindAction(interact);

        SubscribeActionValuesToInputEvents();
    }
    private void SubscribeActionValuesToInputEvents()
    {
        interactAction.performed += inputInfo => InteractTriggered = true;
        interactAction.canceled += inputInfo => InteractTriggered = false;
    }

    private void OnEnable()
    {
        playerControls.FindActionMap(actionMapName).Enable();
    }
    private void OnDisable()
    {
        playerControls.FindActionMap(actionMapName).Disable();
    }
}