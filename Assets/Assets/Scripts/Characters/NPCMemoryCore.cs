using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class NPCMemoryCore : MonoBehaviour
{
    [Header("Default values (editable in Inspector)")]
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

    void Awake()
    {
        npc = GetComponent<NPC>();
        // Debug.Log("NPC Found " + npc == null ? "null" : npc.name);
        npcMessage = $"Hello, I'm {npcName}.";
    }

    public string GetRole() => NPCRole.GetRolesDict()[npc.roleString]; //--> fixed value
    public string GetShortDescription() => npcShortDescription;
    public string GetPersonalityDescription() => Personality.GetPersonalityPromptsDict()[npc.personalityType ? 0 : 1]; //--> npc get
    public string GetBehaviorPatternsDescription()
    {
        var behaviorPatternsText = "";
        if(npc.roleString == "Leader")
        {
            behaviorPatternsText = "You speak using a slightly more formal language, but not much.";
        }
        else
        {
            behaviorPatternsText = "You speak using informal futuristic language.";
        }
        behaviorPatternsText += "Your current mood is being influenced by your emotions, so adapt your tone accordingly. You feel " + npc.GetCurrentEmotionString() + ".";
        return behaviorPatternsText;
    } 
    public string GetBehaviorChangeDescription() => currentEmotionBehaviorText; //--> npc get
    public string GetCurrentLocationDescription() => currentLocation; //--> fixed value
    public string GetCurrentSituationDescription() => currentSituation; //--> alters with conversation history
    public string GetRelationshipToPlayerDescription() => relationshipToPlayer; //--> alters with conversation history
    public string GetConversationHistory() => conversationHistory; //--> alters with conversation history
    public string SetConversationHistory(string newMessage) => this.conversationHistory += newMessage; //--> alters with conversation history
    /// <summary>
    /// //////////////
    /// </summary>
    /// <returns></returns>
    public string GetCultureDescription() => Culture.GetCulturePromptsDict()[npc.cultureString]; //--> npc get
    public string GetOppositeCultureDescription() => Culture.GetCulturePromptsDict()["VisionFrom" + npc.cultureString]; //--> npc get
}
