using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{

    [HideInInspector]
    private string conversationHistory = "";
    private string npcName = "Alice";
    private string allEmotionsText = "";

    public void Start()
    {
        var dict = ActionEmotions.GetDict();
        List<string> allEmotionsLines = new List<string>();
        string[] possibleActions = dict.Keys.ToArray();
        int i = 1;
        foreach (string action in possibleActions)
        {
            if (!dict.ContainsKey(action)) continue;

            string emotions = string.Join(", ", dict[action]);
            allEmotionsLines.Add($"Option {i}: [{emotions}]");
            i++;
        }
        allEmotionsText = string.Join("\n", allEmotionsLines);
        // Debug.Log("ALL EMOTIONS TEXT: " + allEmotionsText);
    }

    public string BuildPrompt(string userMessage, NPC npc)
    {


        var npcName = npc.nameString;
        var npcRole = npc.memoryCore.GetRole();
        var npcShortDescription = npc.memoryCore.GetShortDescription();
        var personalityText = npc.memoryCore.GetPersonalityDescription();
        var cultureText = npc.memoryCore.GetCultureDescription();
        var behaviorPatternsText = npc.memoryCore.GetBehaviorPatternsDescription();
        var currentLocation = npc.memoryCore.GetCurrentLocationDescription();
        var currentSituation = npc.memoryCore.GetCurrentSituationDescription();
        var relationshipToPlayer = npc.memoryCore.GetRelationshipToPlayerDescription();
        var currentEmotionLabel = "Currently, you are feeling " + npc.emotion.GetName();
        var currentEmotionBehaviorText = npc.memoryCore.GetBehaviorChangeDescription();
        var fullPrompt =
            "System:\n" +
            "You are a non-playable character in a game. You respond only as the NPC, never as the game engine, narrator or the player." +
            "\n" +
            "[IDENTITY]\n" +
            $"Name: {npcName}\n" +
            $"Role: {npcRole}\n" +
            $"Short description: {npcShortDescription}\n" +
            "\n" +
            "[PERSONALITY]\n" +
            personalityText + "\n\n" +
            "[CULTURE]\n" +
            cultureText + "\n\n" +
            "[STABLE BEHAVIOR PATTERNS]\n" +
            behaviorPatternsText + "\n\n" +
            "[CURRENT STATE]\n" +
            $"Location: {currentLocation}\n" +
            $"Time / situation: {currentSituation}\n" +
            $"Relationship to the player: {relationshipToPlayer}\n" +
            $"Current emotion: {currentEmotionLabel}\n" +
            $"How this emotion changes your behavior: {currentEmotionBehaviorText}\n\n" +
            "[STYLE RULES]\n" +
            "- Always stay in character.\n" +
            "- Speak in the first person (\"I\", \"me\", \"my\").\n" +
            "- Do NOT prefix your lines with your name.\n" +
            "- Do NOT write lines starting with \"Player:\" or \"User:\".\n" +
            "- NEVER create dialogue for the player.\n" +
            "- If you refuse to answer, do it in-character.\n" +
            "- Never say you are an AI or a language model.\n" +
            "- Adjust your tone according to the current emotion.\n" +
            "- Do not explain your internal traits or models.\n" +
            "- Do not invent player actions, player speech, or the player's thoughts.\n" +
            // "- Only output your own NPC response, nothing else.\n" +
           " - Your output consists of an emotional index followed by your spoken dialogue.\n" +
            "- The emotional index is part of your response and is required.\n" +
            "- Do NOT use emojis or emoticons.\n" +
            "- Do NOT use - when talking, or ;.\n" +
            "- Format text cleanly, no extra spaces or random newlines.\n\n" +
            "Conversation so far (summary):\n" +
            npc.memoryCore.conversationHistory + "\n\n" +
            $"Your response as {npcName}, in first person, in one continuous answer:\n" +

            "[IMPORTANT OUTPUT FORMAT (MANDATORY):]\n\n" +
            "Before your spoken response, choose EXACTLY ONE emotional response option from the list below.\n" +
            "Write your answer in this exact format, on a SINGLE LINE:\n" +
            "[NUMBER] dialogue" + "\n" +
            // "Where NUMBER is the index of the chosen emotional response option, matching which emotional array you think fist the best given the last player message.\n" +
            "Where NUMBER is the index of the emotional response option that best matches the emotional impact of the player's last message.\n" +
            "Emotional response options:\n" +
            allEmotionsText + "\n\n" +
            "Do not explain your choice.\n"+ "Do not output anything before the bracket.\n" + "Do not use brackets elsewhere.\n" + 
            "You must follow this format exactly.";
            // $"Choose between the possible resultant emotions, how did the player make you feel with his response? :\n"
            // $"Given what the player did/said, which emotional response set best matches how you'd say you felt? :\n"
            // + allEmotionsText + "\n\n";

        Debug.Log("FULL PROMPT: " + fullPrompt);
        fullPrompt = "Return exactly this text: [1] test";
        return fullPrompt;
    }

    public string GetNpcTextMessage(LlamaResponse lr)
    {

        Debug.Log("GENERATED RESPONSE " + lr.id + " OF TYPE " + lr.@object);
        Debug.Log("LR CHOICES, TOTAL OF " + lr.choices.Length);
        foreach (var item in lr.choices)
        {
            Debug.Log("Text of Index " + item.index + ": "+ item.text);
            Debug.Log("Choice of Text Finish Reason: " + item.finish_reason);
            
        }
        Debug.Log("LLAMA USAGE: ");
        Debug.Log("Number of tokens processed from your input prompt - " + lr.usage.prompt_tokens);
        Debug.Log("Number of tokens generated by the model - " + lr.usage.completion_tokens);
        var response = lr.choices[0].text.Trim();
        // this.conversationHistory = this.conversationHistory + $"\n" + response;
        return response;
    }


    // public LlamaResponse PostLlamaAction(string prompt)
    // {
    //     LlamaResponse resp = null;
    //     StartCoroutine(LlamaAPI.PostLlamaAction(prompt, (response) =>
    //         {
    //             if (response != null)
    //                 resp = response;
    //             else
    //                 Debug.LogError("Llama API returned null response.");
    //         }));
    //     yield return resp;
    // }
}