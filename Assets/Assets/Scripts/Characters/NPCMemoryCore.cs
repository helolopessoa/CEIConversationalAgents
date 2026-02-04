using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using System.Xml.Linq;

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
        npcMessage = $"Hello, I'm {npcName}.";
        conversationHistory = "";
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
        var behaviorPatternsText = "";
        if(npc.roleString == "Leader")
        {
            behaviorPatternsText = "You speak using informal futuristic language.";
        }
        else
        {
            behaviorPatternsText = "You speak using informal futuristic language.";
        }
        behaviorPatternsText += "Your current mood is being influenced by your emotions, so adapt your tone accordingly. You feel " + npc.humorState + ".";
        saveGameData.emotions.Add(npc.humorState);
        return behaviorPatternsText;
    } 
    public string GetBehaviorChangeDescription() => currentEmotionBehaviorText; //--> npc get
    public string GetCurrentLocationDescription() => currentLocation; //--> fixed value
    public string GetCurrentSituationDescription() => currentSituation; //--> alters with conversation history
    public string GetRelationshipToPlayerDescription() => @$"Your trust in {playerName} is {npc.currentTrust}, out of total of 1.0f"; //--> alters with conversation history
    public string GetConversationHistory() => conversationHistory; //--> alters with conversation history
    public string SetConversationHistory(string playerMessage, string npcMessage) => conversationHistory += "\n[PLAYER] " + playerMessage + $"\n[{npcName}] " + npcMessage; //--> alters with conversation history
    public void SetClassification(string newMessage, string classification) => saveGameData.classifications.Add(@$"{newMessage} -- {classification}"); //--> alters with conversation history
    public void SetResponseEmotion(string emotion) => saveGameData.emotions.Add(emotion); //--> alters with conversation history
    public string GetCultureDescription() => Culture.GetCulturePromptsDict()[npc.cultureString]; //--> npc get
    public string GetOppositeCultureDescription() => Culture.GetCulturePromptsDict()["VisionFrom" + npc.cultureString]; //--> npc get
    public SaveGameData GetSaveGameData()
    {
        saveGameData.playerName = playerName;
        saveGameData.npcName = npcName;
        saveGameData.npcRoleTitle = npc.roleString;
        saveGameData.npcCulture = npc.cultureString;
        saveGameData.npcPersonality = npc.personalityType.ToString();
        saveGameData.npcPersonality+= npc.personalityType ? " - Analytical–Reserved" : " - Expressive–Adaptive";
        saveGameData.chatSummary = conversationHistory;
        saveGameData.peaceTreatySigned = false;
        saveGameData.playTime = Time.time - saveGameData.playTime;
        return saveGameData;
    }
}
