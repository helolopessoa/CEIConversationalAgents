// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using TMPro;

// public class LLMChatBox : MonoBehaviour
// {
//     public GameObject LLMChat;
//     public TMP_InputField messageInput;
//     public TMP_Text messageOutput;

//     public Transform chatContent;
//     [HideInInspector]
//     string response;
    
//     [HideInInspector]
//     NPC currentNPC;
//     LlamaResponse lr;
//     void Start()
//     {
//         messageInput.onEndEdit.AddListener(onSendMessage);
//         // messageInput.gameObject.SetActive(false); 
//     }
//     void Update()
//     {

//         // if (messageInput.isFocused && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
//         // {
//         //     Debug.Log("onSendMessage called from Update");
//         //     onSendMessage();
//         // }

//         if(!string.IsNullOrWhiteSpace(response))
//         {
//             messageOutput.text = response;
//         }
//         else
//         {
//             messageOutput.text = " Hi! I'm "  + currentNPC?.nameString + ". Nice to meet you!";
//         }
//     }

//     public void onNPCSelected(NPC npc)
//     {
//         currentNPC = npc;
//         // sendButton.onClick.AddListener(onSendMessage);
//         EventSystem.current.SetSelectedGameObject(messageInput.gameObject);
//         messageInput.ActivateInputField();
//     }

//     public void onSendMessage(string message)
//     {
//         messageInput.gameObject.SetActive(false); 
//         // Debug.Log(messageInput.isFocused);
//         // if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
//         // {
//         // string message = messageInput.text;
//         response = "Thinking...";
//         Debug.Log("onSendMessage");
//         if (!string.IsNullOrWhiteSpace(message))
//         {
//             lr = PostLlamaAction(message, currentNPC?.humorState, currentNPC?.nameString);
//             // messageInput.text = "";
//         }
//         Debug.Log(lr.choices[0].text);
//         response = lr.choices[0].text;
//             // this.currentNPC?.DispatchPlayerState(action.Value);
//         EventSystem.current.SetSelectedGameObject(messageInput.gameObject);
//         messageInput.ActivateInputField();
//         // }

//     }    


//     public LlamaResponse PostLlamaAction(string message, string humorState, string name)
//     {
//         return LlamaAPI.postLlamaAction(message, humorState, name);
//     }
// }