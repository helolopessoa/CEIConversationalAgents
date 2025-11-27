using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ChatLogMessage : MonoBehaviour
{
    [SerializeField] private TMP_InputField userInput;
    [SerializeField] private TMP_Text npcMessageOutput;
    [SerializeField] private Button sendButton;
    private ChatController npcChatMessageController;
    public void Init(ChatController controller)
    {
        npcChatMessageController = controller;
    }
    
    void Start()
    {
        // if (chatMessageController != null) chatMessageController.onSendMessage.AddListener(UpdateNpcMessage);
        if (npcMessageOutput != null) npcMessageOutput.text = npcChatMessageController.npcMessage;
        if (sendButton != null) sendButton.onClick.AddListener(SubmitSendButton);
        if (userInput != null && userInput.isFocused)
        {
            userInput.onSubmit.AddListener(SubmitUserMessage);
            userInput.ActivateInputField();
        }
    }
    public void SubmitUserMessage(string userMessage)
    {
        if (string.IsNullOrWhiteSpace(userMessage)) return;   

        // Debug.Log($"[PLAYER] {userMessage}");
        npcChatMessageController.onSendMessage(userMessage);
        userInput.text = string.Empty;
        userInput.ActivateInputField();
    }

    public void SubmitSendButton()
    {
        if (userInput == null) return;
        SubmitUserMessage(userInput.text);
    }

    public void UpdateNpcMessage()
    {
        if(npcChatMessageController == null) return;
        Debug.Log($"[ChatLogMessage] {npcChatMessageController.npcMessage}");
        if (npcMessageOutput != null) npcMessageOutput.text = npcChatMessageController.npcMessage;
    }


}