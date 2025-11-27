using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Input Actions")]
    public InputActionAsset playerInputAsset; // PlayerGameplay asset
    public InputActionAsset chatInputAsset; // ChatUIBox asset

    private Rigidbody2D rb;
    private InputAction moveAction;
    private InputAction openChatAction;
    private InputAction closeChatAction;
    // private InputAction sendChatAction;
    public bool canMove { get; private set; } = true;
    private float moveInput;


    // current chat zone the player is inside
    private ChatController currentChatZone;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Find the map and actions by name (from your screenshot)
        var playerMap = playerInputAsset.FindActionMap("Player", true);
        var chatUIMap = chatInputAsset.FindActionMap("ChatboxUI", true);
        moveAction      = playerMap.FindAction("Move", true);
        openChatAction  = playerMap.FindAction("OpenChatbox", true);
        closeChatAction = chatUIMap.FindAction("CloseChat", true);
        
    }

    private void OnEnable()
    {
        moveAction.Enable();
        openChatAction.Enable();
        closeChatAction.Enable();

        moveAction.performed += OnMove;
        moveAction.canceled  += OnMove;   // so it resets to 0 on release
        openChatAction.performed += OnOpenChatbox;
        closeChatAction.performed += OnCloseChat;

    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled  -= OnMove;
        openChatAction.performed -= OnOpenChatbox;
        closeChatAction.performed -= OnCloseChat;

        moveAction.Disable();
        openChatAction.Disable();
        closeChatAction.Disable();
    }


    private void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<float>();
    }
    private void OnCloseChat(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (currentChatZone == null) return;

        currentChatZone.CloseChat();
        ResumeMovement();
    }


    private void FixedUpdate()
    {
        if (!canMove)
        {
            rb.linearVelocity = new Vector2(0f, 0f);
            return;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, 0f);
    }

    private void OnOpenChatbox(InputAction.CallbackContext ctx)
    {
        // Only open chat if we're inside some NPC zone
        if (currentChatZone != null)
        {
            canMove = false;                  // freeze player
            currentChatZone.OpenChat();       // this will enable the canvas
        }
    }

    // Called by ChatController when entering/exiting triggers
    public void SetChat(ChatController zone)
    {
        currentChatZone = zone;
    }

    public void ClearChat(ChatController zone)
    {
        if (currentChatZone == zone)
            currentChatZone = null;
    }

    // Called by ChatController when chat closes
    public void ResumeMovement()
    {
        canMove = true;
    }
}
