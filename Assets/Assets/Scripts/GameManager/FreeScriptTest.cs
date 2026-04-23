// using UnityEngine;
// using System.Collections.Generic;
// using System.Collections;
// using Microsoft.VisualBasic;

// public class FreeScriptTest : MonoBehaviour
//  {
//     [SerializeField]
//     private List<ChatController> NPCchats = new List<ChatController>();

//     public void StartVSMTest()
//     {
//         StartCoroutine(StartQuestionaire());
//     }
//     public IEnumerator VSMTest()
//     {
//         foreach (var chat in NPCchats)
//         {
//             var n = 0;
//             while(n < 10)
//             {
//                 // Debug.Log("NPC: " + chat.npcMessage);
//                 // yield return StartCoroutine(SendQuestion(question, chat));
//                 yield return StartCoroutine(SendQuestion(question, chat));
//             }
             
//         }
//     }

//     private IEnumerator SendQuestion(string question, ChatController chat)
//     {
//         chat.onSendMessage(question);
//         yield return new WaitForSeconds(3f); // wait for NPC response
//     }

//     public IEnumerator StartQuestionaire()
//     {
//         var n = 0;
//         while(n < 15)
//         {
//             GameManager.Instance.StartNPCs();
//             yield return VSMTest();
//             GameManager.Instance.EndGameNPCs();
//             yield return new WaitForSeconds(10f); // wait for NPC response
//             n++;
//         }
//     }    


//  }