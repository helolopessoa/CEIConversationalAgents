using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChatMessage : MonoBehaviour
{

    [HideInInspector]
    public string npcMessage = "Hello, I'm Alice.";
    [SerializeField]
    private DialogueManager dm;

    [SerializeField]
    private ChatLogMessage chatLog;
    public void onSendMessage(string playerMessage)
    {
        // npcMessage = "Thinking...";
        // Debug.Log("onSendMessage");
        npcMessage = dm.getNpcMessage(playerMessage);
        chatLog?.UpdateNpcMessage();
    }


}