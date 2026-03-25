using System.Collections;
using Microsoft.VisualBasic;
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
    private InputAction sendChatAction;
    private NPC npc;
    
    [Header("UI")]
    public GameObject chatUI;          // canvas to show for this NPC

    [Header("References")]
    public PlayerController2D player;      // assign in inspector (or auto-find)

    private bool chatOpen = false;
    void Awake()
    {
        npc = this.GetComponent<NPC>();
        var chatUIMap = chatInputAsset.FindActionMap("ChatboxUI", true);
        sendChatAction = chatUIMap.FindAction("SendMessage", true);
        // npcMessage = npc.memoryCore.npcMessage;
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
        if (chatUI != null)
            chatUI.SetActive(false);
        npcMessage = npc.memoryCore.npcMessage;
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
        if (chatUI != null)
            chatUI.SetActive(true);
            ChangeCanvasPortrait();
            chatLog.Init(this);
            chatLog.UpdateNpcMessage();
    }

    private void ChangeCanvasPortrait()
    {
        chatUI.transform.Find("Portrait/CharacterPortrait").GetComponent<UnityEngine.UI.Image>().sprite = npc.npcPortrait;
    }

    public void CloseChat()
    {
        if (!chatOpen) return;
        chatOpen = false;
        if (chatUI != null)
            chatUI.SetActive(false);
    }


    public void onSendMessage(string playerMessage)
    {
        // 1) show player line immediately
        // chatLog?.UpdateNpcMessage(userMessage);

        // 2) build prompt
        string classificationPrompt = dm.BuildClassificationPrompt(playerMessage);

        // 3) call Model asynchronously
        StartCoroutine(CallModelClassification(classificationPrompt, playerMessage));

    }

    private IEnumerator CallModelClassification(string prompt, string playerMessage = null)
    {
        Debug.Log("[ChatController] CallModelClassification(prompt, playerMessage) called.");
        Debug.Log("[ChatController] playerMessage = " + playerMessage);
        ModelClassificationResponse lr = null;
        string dialoguePrompt = "";
        yield return ModelAPI.PostModelActionClassification(prompt, (resp) => lr = resp);

        if (lr == null)
        {
            npcMessage = "[Error] Classification Model failure.";
            yield break;
        }
        else
        {
            Debug.Log("[ChatController] Classification response received.");
            Debug.Log(lr.result);
        }
        if(playerMessage != null)
        {
            npc.DispatchPlayerState(lr.result);
            Debug.Log("[ChatController] Dispatching player state: " + lr.result);
            npc.memoryCore.SetClassification(playerMessage, lr.result);{
            Debug.Log("[ChatController] Game testing base: " + GameManager.baselineTest);
            if (GameManager.baselineTest)
            {
                dialoguePrompt = dm.BuildBaselineDialoguePrompt(playerMessage, npc);
            }
            else
            {
                dialoguePrompt = dm.BuildDialoguePrompt(playerMessage, npc);                    
            }
            StartCoroutine(CallModelAndShowReply(dialoguePrompt, playerMessage));}
        }
        else
        {
            bool decision = false;
            if(npc.memoryCore.npcRole == "Downsides") 
            {
                bool.TryParse(lr.result.ToLower(), out decision);
                GameManager.SetDownsidePeaceTreatyResult(decision);
                npc.memoryCore.SetNPCDecision(decision);
            }
            else if(npc.memoryCore.npcRole == "Rangers") 
            {
                bool.TryParse(lr.result.ToLower(), out decision);
                GameManager.SetRangersPeaceTreatyResult(decision);
                // GameManager.Instance.successOnPeaceTreatyRangers.Value = decision;
                npc.memoryCore.SetNPCDecision(decision);
            }
            Debug.Log("[ChatController] Peace Treaty Result = " + decision);
            // if(GameManager.Instance.successOnPeaceTreaty != null) npc.memoryCore.SetGameResult(GameManager.Instance.successOnPeaceTreaty);
        }
    }

        private IEnumerator CallModelAndShowReply(string prompt, string playerMessage)
    {
        Debug.Log("[ChatController] CallModelAndShowReply(prompt, playerMessage) called.");
        ModelResponse lr = null;
        yield return ModelAPI.PostModelAction(prompt, (resp) => lr = resp);

        if (lr == null)
        {
            npcMessage = "[Error] Answer Model failure.";
            chatLog?.UpdateNpcMessage();
            yield break;
        }
        npcMessage = dm.GetNpcTextMessage(lr);
        npc.memoryCore.SetConversationHistory(playerMessage, npcMessage);
        npc.memoryCore.SetNPCConversationHistory(npcMessage);
        chatLog?.UpdateNpcMessage();
        
        if(npc.currentTrust >= 0.8f && npc.memoryCore.npcRole == "Leader")
        {
            var resultPrompt = dm.BuildResultPrompt(npcMessage);
            StartCoroutine(CallModelClassification(resultPrompt));
        }
    }
}



