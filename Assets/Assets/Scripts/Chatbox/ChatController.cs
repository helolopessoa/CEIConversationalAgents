using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;  

public class ChatController : MonoBehaviour
{

    [HideInInspector]
    public string npcMessage;
    [SerializeField]
    private DialogueManager dm;

    [SerializeField]
    private ChatLogMessage chatLog;
    public InputActionAsset chatInputAsset; // ChatUIBox asset
    // private InputAction closeChatAction;
    private InputAction sendChatAction;

    private NPC npc;
    [Header("UI")]
    public GameObject chatCanvas;          // canvas to show for this NPC

    [Header("References")]
    public PlayerController2D player;      // assign in inspector (or auto-find)

    private bool chatOpen = false;

void Awake()
    {
        npc = this.GetComponent<NPC>();
        var chatUIMap = chatInputAsset.FindActionMap("ChatboxUI", true);
        sendChatAction = chatUIMap.FindAction("SendMessage", true);
        
    }

    private void Start()
    {
        Debug.Log("[ChatController] Starting ChatController for NPC: " + npc);
        npcMessage = npc.memoryCore.npcMessage;
        chatLog?.UpdateNpcMessage();
        if (chatCanvas != null)
            chatCanvas.SetActive(false);
    }
        private void OnEnable()
    {
        // enable chat UI actions (elas podem ficar sempre ligadas;
        // a gente só reage se o chat estiver aberto)
        sendChatAction.Enable();
        // closeChatAction.Enable();
        sendChatAction.performed  += OnSendChat;
        // closeChatAction.performed += OnCloseChat;
    }
    private void OnDisable()
    {
        sendChatAction.performed  -= OnSendChat;
        // closeChatAction.performed -= OnCloseChat;
        sendChatAction.Disable();
        // closeChatAction.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (player == null)
                player = other.GetComponent<PlayerController2D>();

            if (player != null)
                player.SetChat(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (chatOpen)
                CloseChat();

            if (player != null)
                player.ClearChat(this);
        }
    }

        private void OnSendChat(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (player != null && player.canMove == true) return;          // não está com chat aberto
        if (chatLog == null) return;

        // string text = chatInputField.text;
        // if (string.IsNullOrWhiteSpace(text)) return;

        // manda a mensagem pro NPC
        // currentChatZone.onSendMessage(text);

        // limpa input e mantém foco
        // chatInputField.text = string.Empty;
        // chatInputField.ActivateInputField();
        chatLog.SubmitSendButton();
    }

    public void OpenChat()
    {
        if (chatOpen) return;

        chatOpen = true;
        if (chatCanvas != null)
            chatCanvas.SetActive(true);
            chatLog.Init(this);

        // Now the EventSystem’s DefaultInputActions (UI map) takes over:
        // - Enter = Submit
        // - Arrows / Tab = Navigate
    }

    public void CloseChat()
    {
        if (!chatOpen) return;
        chatOpen = false;
        if (chatCanvas != null)
            chatCanvas.SetActive(false);
    }

    public void onSendMessage(string playerMessage)
    {
        npc.memoryCore.conversationHistory += $"\n-{playerMessage}";
        npcMessage = dm.getNpcMessage(playerMessage, npc);
        npc.memoryCore.conversationHistory += $"\n-{npcMessage}";
        chatLog?.UpdateNpcMessage();
    }



}
