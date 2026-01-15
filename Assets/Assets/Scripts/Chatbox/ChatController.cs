using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;  

public class ChatController : MonoBehaviour
{

    [HideInInspector]
    public string npcMessage;
    // public string userMessage;
    
    [SerializeField]
    private DialogueManager dm;

    [SerializeField]
    private ChatLogMessage chatLog;
    
    public InputActionAsset chatInputAsset; // ChatUIBox asset
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
        // StartCoroutine(ModelAPI.TestPing((resp) =>
        // {
        //     if (resp == null)
        //         Debug.LogError("Ping failed.");
        //     else
        //         Debug.Log("Ping succeeded. Server replied: " + resp);
        // }));
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
        sendChatAction.performed  += OnSendChat;
    }
    private void OnDisable()
    {
        sendChatAction.performed  -= OnSendChat;
        sendChatAction.Disable();
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
        chatLog.SubmitSendButton();
    }

    public void OpenChat()
    {
        if (chatOpen) return;

        chatOpen = true;
        if (chatCanvas != null)
            chatCanvas.SetActive(true);
            ChangeCanvasPortrait();
            chatLog.Init(this);
    }

    private void ChangeCanvasPortrait()
    {
        chatCanvas.transform.Find("ChatUI/Portrait/CharacterPortrait").GetComponent<UnityEngine.UI.Image>().sprite = npc.npcPortrait;
    }

    public void CloseChat()
    {
        if (!chatOpen) return;
        chatOpen = false;
        if (chatCanvas != null)
            chatCanvas.SetActive(false);
    }

    // public void onSendMessage(string userMessage)
    // {
    //     npc.memoryCore.conversationHistory += $"\n-{userMessage}";
    //     npcMessage = dm.getNpcMessage(userMessage, npc);
    //     npc.memoryCore.conversationHistory += $"\n-{npcMessage}";
    //     chatLog?.UpdateNpcMessage();
    // }

// THIS WILL CHANGE: COROUTINES NEED TO BE CONNECTED -> WORKFLOW!!
    public void onSendMessage(string userMessage)
    {
        // 1) show player line immediately
        // chatLog?.UpdateNpcMessage(userMessage);

        // 2) build prompt
        string dialoguePrompt = dm.BuildDialoguePrompt(userMessage, npc);
        string classificationPrompt = dm.BuildClassificationPrompt(userMessage);

        // 3) call Model asynchronously
        StartCoroutine(CallModelAndShowReply(dialoguePrompt, userMessage));
        StartCoroutine(CallModelClassification(classificationPrompt, userMessage));

    }

    private IEnumerator CallModelAndShowReply(string prompt, string playerMessage)
    {
        // Debug.Log("[ChatController] CallModelAndShowReply(prompt, playerMessage) called.");
        ModelResponse lr = null;
        // yield return ModelAPI.PostModelActionClassification(prompt, (resp) => lr = resp);
        yield return ModelAPI.PostModelAction(prompt, (resp) => lr = resp);

        if (lr == null)
        {
            npcMessage = "[Error] Model failure.";
            chatLog?.UpdateNpcMessage();
            yield break;
        }
        npcMessage = dm.GetNpcTextMessage(lr);
        npc.memoryCore.SetConversationHistory("\n" + npcMessage);
        // chatLog.AddNpcMessage(npcText);
        chatLog?.UpdateNpcMessage();
    }

    private IEnumerator CallModelClassification(string prompt, string playerMessage)
    {
        // Debug.Log("[ChatController] CallModelClassification(prompt, playerMessage) called.");
        ModelClassificationResponse lr = null;
        yield return ModelAPI.PostModelActionClassification(prompt, (resp) => lr = resp);

        if (lr == null)
        {
            npcMessage = "[Error] Model failure.";
            // chatLog?.UpdateNpcMessage();
            yield break;
        }
        else
        {
            Debug.Log("[ChatController] Classification response received.");
            Debug.Log(lr.result);
        }
        npc.DispatchPlayerState(lr.result);
        // npcMessage = dm.GetNpcTextMessage(lr);
        // npc.memoryCore.conversationHistory += $"\n-{playerMessage}" + $"\n-{npcMessage}";
        // // chatLog.AddNpcMessage(npcText);
        // chatLog?.UpdateNpcMessage();
    }

}



