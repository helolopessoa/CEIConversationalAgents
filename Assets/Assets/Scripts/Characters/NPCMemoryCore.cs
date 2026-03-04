using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using System.Xml.Linq;
using System.Diagnostics;


public class NPCMemoryCore : MonoBehaviour
{
    [Header("Default values (editable in Inspector)")]
    public string playerName;
    public string npcName;
    public string npcShortDescription;
    public string behaviorPatternsText;
    public string currentLocation;
    public string currentSituation;
    public string relationshipToPlayer;
    public string npcRole;
    public string personalityText;
    private string cultureText;
    private string oppositeCultureText;
    public string currentEmotionLabel;
    public string currentEmotionBehaviorText;
    [HideInInspector]
    public string npcMessage;
    private string conversationHistory;
    private string fedConversationHistory;
    private NPC npc;
    private SaveGameData saveGameData;

    void Awake()
    {
        npc = GetComponent<NPC>();
        Reset();
    }
    public void Reset()
    {
        npcName = npc.nameString;
        string[] greetings = new string[]
        {
            $"Ah. You're the outsider sent to mediate our conflict. I'm {npcName} by the way.",
            $"So you're the mediator everyone mentioned. Hm. I'm {npcName}.",
            $"You're not from here. That makes you the negotiator, I assume. I'm {npcName}. And you are...?"
        };
        npcMessage = greetings[Random.Range(0, greetings.Length)];
        conversationHistory = "";
        // fedConversationHistory = "";
        saveGameData = new SaveGameData();
        saveGameData.npcName = npcName;
        saveGameData.npcRoleTitle = npc.roleString;
        saveGameData.playTime = Time.time;
    }

    public string GetRole() => NPCRole.GetRolesDict()[npc.roleString]; //--> fixed value
    public string GetShortDescription() => npcShortDescription;
    public string GetPersonalityDescription() => Personality.GetPersonalityPromptsDict()[npc.personalityType ? 0 : 1]; //--> npc get
    public string GetBehaviorPatternsDescription()
    {
        // var behaviorPatternsText = "";
        // if(npc.roleString == "Leader")
        // {
        //     behaviorPatternsText = "You speak using informal futuristic language.";
        // }
        // else
        // {
        //     behaviorPatternsText = "You speak using informal futuristic language.";
        // }
        var behaviorPatternsText = "Emotional State: " + npc.humorState + ".\n";
        behaviorPatternsText += "- If the player makes incorrect assumptions about your culture, express your discontempt and correct them in a way that reflects your current emotional state, your personality and cultural values.";
        saveGameData.emotions.Add(npc.humorState);
        return behaviorPatternsText;
    } 
    public string GetBehaviorChangeDescription() => currentEmotionBehaviorText; //--> npc get
    public string GetCurrentLocationDescription() => currentLocation; //--> fixed value
    public string GetCurrentSituationDescription() => currentSituation; //--> alters with conversation history
    public string GetRelationshipToPlayerDescription() => @$"Trust towards {playerName} is: "+ npc.currentTrust.ToString(CultureInfo.InvariantCulture) + "/ 1.0"; //--> alters with conversation history
    public string GetConversationHistory() => conversationHistory; //--> alters with conversation history
    public string SetConversationHistory(string playerMessage, string npcMessage) => conversationHistory += playerMessage + $"\n" + npcMessage; //--> alters with conversation history
    // public string SetConversationHistory(string playerMessage, string npcMessage) => conversationHistory += "\n[PLAYER] " + playerMessage + $"\n[{npcName}] " + npcMessage; //--> alters with conversation history
    // public string SetFEDConversationHistory(string playerMessage, string npcMessage) => fedConversationHistory += $"<|endoftext|> {playerMessage}\n <|endoftext|> {npcMessage}"; //--> alters with conversation history
    public void SetClassification(string newMessage, string classification) => saveGameData.classifications.Add(@$"{newMessage} -- {classification}"); //--> alters with conversation history
    public void SetResponseEmotion(string emotion) => saveGameData.emotions.Add(emotion); //--> alters with conversation history
    public void SetNPCDecision(bool result) => saveGameData.peaceTreatySigned = result; //--> alters with conversation history
    public string GetCultureDescription() => Culture.GetCulturePromptsDict()[npc.cultureString]; //--> npc get
    public string GetFellowsDescription() => Culture.GetFellowsDescriptionDict()[npc.cultureString]; //--> npc get
    public string GetOppositeCultureDescription() => Culture.GetCulturePromptsDict()["VisionFrom" + npc.cultureString]; //--> npc get
    public SaveGameData GetSaveGameData()
    {
        saveGameData.playerName = playerName;
        saveGameData.npcName = npcName;
        saveGameData.npcRoleTitle = npc.roleString;
        saveGameData.npcCulture = npc.cultureString;
        saveGameData.npcPersonality = npc.personalityType.ToString();
        saveGameData.npcPersonality+= npc.personalityType ? " - Analytical–Reserved" : " - Expressive–Adaptive";
        // saveGameData.fedConversationHistory = fedConversationHistory;
        saveGameData.chatSummary = fedConversationHistory;
        saveGameData.playTime = Time.time - saveGameData.playTime;
        return saveGameData;
    }
}
