using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCMemoryCore : MonoBehaviour
{

    [Header("Default values (editable in Inspector)")]
    public string npcName;
    public string npcRole;
    public string npcShortDescription;
    public string personalityText;
    public string cultureText;
    public string behaviorPatternsText;
    public string currentLocation;
    public string currentSituation;
    public string relationshipToPlayer;
    public string currentEmotionLabel;
    public string currentEmotionBehaviorText;
    [HideInInspector]
    public string npcMessage;
    public string conversationHistory;

    // public string role = "Citizen";
    // public string shortDescription = "A helpful NPC.";
    // public string personalityDescription = "Friendly";
    // public string cultureDescription = "Local";
    // public string behaviorPatternsDescription = "Calm and helpful";
    // public string currentLocationDescription = "Town Square";
    // public string currentSituationDescription = "Idle";
    // public string relationshipToPlayerDescription = "Neutral";

    // Memory core reference (provides the GetXxx methods)

    void Awake()
    {
        npcMessage = $"Hello, I'm {npcName}.";
    }
    //     // Make sure we have a memoryCore: try to find one on the same GameObject, otherwise add a default.
    //     if (memoryCore == null)
    //     {
    //         memoryCore = GetComponent<NPCMemoryCore>();
    //         if (memoryCore == null)
    //         {
    //             memoryCore = gameObject.AddComponent<NPCMemoryCore>();
    //         }
    //     }

    //     // Ensure we have an Emotion instance (try to find a component first)
    //     if (emotion == null)
    //     {
    //         // if Emotion is a component in your project, replace with GetComponent<Emotion>()
    //         emotion = new Emotion();
    //     }

    //     RefreshDescriptions();
    // }

    // Public method to refresh the strings from the core/emotion
    // public void RefreshDescriptions()
    // {
    //     npcRole = memoryCore.GetRole();
    //     npcShortDescription = memoryCore.GetShortDescription();
    //     personalityText = memoryCore.GetPersonalityDescription();
    //     cultureText = memoryCore.GetCultureDescription();
    //     behaviorPatternsText = memoryCore.GetBehaviorPatternsDescription();
    //      = memoryCore.GetCurrentLocationDescription();
    //     currentSituation = memoryCore.GetCurrentSituationDescription();
    //     relationshipToPlayer = memoryCore.GetRelationshipToPlayerDescription();

    //     currentEmotionLabel = "Currently, you are feeling " + emotion.GetName();
    //     currentEmotionBehaviorText = emotion.GetBehaviorChangeDescription();
    // }

    public string GetRole() => npcRole;
    public string GetShortDescription() => npcShortDescription;
    public string GetPersonalityDescription() => personalityText;
    public string GetCultureDescription() => cultureText;
    public string GetBehaviorPatternsDescription() => behaviorPatternsText;
    public string GetBehaviorChangeDescription() => currentEmotionBehaviorText;
    public string GetCurrentLocationDescription() => currentLocation;
    public string GetCurrentSituationDescription() => currentSituation;
    public string GetRelationshipToPlayerDescription() => relationshipToPlayer;
}
