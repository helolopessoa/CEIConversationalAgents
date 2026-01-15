using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO.Pipes;

public class DialogueManager : MonoBehaviour
{

    [HideInInspector]
    private string conversationHistory = "";
    private string npcName = "Alice";
    // private string allEmotionsText = "";
    private string allActionsText = "";

    public void Start()
    {
        var dict = ActionEmotions.GetDictTalking();
        List<string> allActionsLines = new List<string>();
        string[] possibleActions = dict.Keys.ToArray();
        int i = 1;
        foreach (string action in possibleActions)
        {
            if (!dict.ContainsKey(action)) continue;

            // string action = string.Join(", ", dict[action]);
            allActionsLines.Add($"[{i}]: {action}");
            i++;
        }
        allActionsText = string.Join("\n", allActionsLines);
        // Debug.Log("ALL EMOTIONS TEXT: " + allEmotionsText);
    }

    public string BuildDialoguePrompt(string playerMessage, NPC npc)
    {
        var npcName = npc.nameString;
        var npcRole = npc.memoryCore.GetRole();
        var npcShortDescription = npc.memoryCore.GetShortDescription();
        var personalityText = npc.memoryCore.GetPersonalityDescription();
        var cultureText = npc.memoryCore.GetCultureDescription();
        var oppositeCultureText = npc.memoryCore.GetOppositeCultureDescription();
        var behaviorPatternsText = npc.memoryCore.GetBehaviorPatternsDescription();
        var conversationHistory = npc.memoryCore.GetConversationHistory();

        // var currentLocation = npc.memoryCore.GetCurrentLocationDescription();
        // var currentSituation = npc.memoryCore.GetCurrentSituationDescription();
        // var relationshipToPlayer = npc.memoryCore.GetRelationshipToPlayerDescription();
        // var currentEmotionLabel = "Currently, you are feeling " + npc.emotion.GetName();
        // var currentEmotionBehaviorText = npc.memoryCore.GetBehaviorChangeDescription();
        
        
        var fullPrompt = $@"
        SYSTEM: You are a non-playable character in a game. You respond only as the NPC, never as the game engine, narrator or the player.
        REMAIN IN CHARACTER as a non-playable character (NPC) in a game, ANSWERING ACCORDINGLY TO THE PLAYER.
        RULES:
        - NEVER create dialogue for the player.
        - NEVER say you are an AI or a language model.
        - DO NOT explain your internal traits or models.
        - DO NOT invent player actions, player speech, or the player's thoughts.
        - Your output consists of your in-context conversation answer.
        - DO NOT use emojis or emoticons.
        - DO NOT use -, | when talking, or ;.
        - Format text cleanly, no extra spaces or random newlines.
        - Answer ONLY your in-context response, no headers, nametags, NOTHING.
        YOUR IDENTITY IN-GAME: 
        - Name: {npcName}
        - Role: {npcRole}\n
        - Behavior: {behaviorPatternsText}
        - Your personality type: {personalityText}
        - Your cultural values: {cultureText}
        - Your vision of the opposite side: {oppositeCultureText}
        CONVERSATION SO FAR (SUMMARY):
        {conversationHistory}
        PLAYER LAST MESSAGE:
        {playerMessage}
        OUTPUT FORMAT: Your response as {npcName}, in first person, in one continuous answer, to what the player said.
        ";
        Debug.Log("FULL DIALOGUE PROMPT: " + fullPrompt);
        return fullPrompt;
    }

    public string BuildClassificationPrompt(string playerMessage)
    {
        
        var fullPrompt = $@"
        [SYSTEM] CHOOSE WITHIN A LIST OF OPTIONS WHICH OPTION DEFINES THE PLAYER BEHAVIOR THE BEST, BASED ON THE LAST MESSAGE TO YOU.
        [STYLE RULES]
        - Your output consists of an emotional key, ALWAYS.
        - The emotional key is REQUIRED.
        - Format text cleanly, no extra spaces or random newlines.
        - Do NOT infer meaning beyond the literal content of the player's message.
        [OUTPUT FORMAT]
        - Write your answer in this EXACT FORMAT, ON A SINGLE LINE, SUBSTITUTING BY YOUR CHOICES: EMOTIONAL_KEY.
        - Where EMOTIONAL_KEY is the key corresponding of your choice, given the player's last message assessed below.
        [PLAYER LAST MESSAGE:]
        {playerMessage}
        [CHOOSE:]
        {allActionsText}
        ";


        Debug.Log("FULL CLASSIFICATION PROMPT: " + fullPrompt);
        
        
        return fullPrompt;
    }


    public string GetNpcTextMessage(ModelResponse lr)
    {
        Debug.Log("PARSING MODEL RESPONSE...");
        Debug.Log(lr==null ? "LR IS NULL" : "LR IS NOT NULL");

        // Debug.Log("GENERATED RESPONSE " + lr.id + " OF TYPE " + lr.@object); --> llama version
        Debug.Log("GENERATED RESPONSE " + lr.id + " OF TYPE " + lr.model);
        Debug.Log("LR CHOICES, TOTAL OF " + lr.choices.Length);
        foreach (var item in lr.choices)
        {
            Debug.Log("Text of Index " + item.index + ": "+ item.message.content);
            Debug.Log("Choice of Text Finish Reason: " + item.finish_reason);
            
        }
        Debug.Log("Model USAGE: ");
        Debug.Log("Number of tokens processed from your input prompt - " + lr.usage.prompt_tokens);
        Debug.Log("Number of tokens generated by the model - " + lr.usage.completion_tokens);
        var response = lr.choices[0].message.content.Trim(); 
        // var response = lr.choices[0].text.Trim(); -> llama version
        return response;
    }
}



        // var fullPrompt = $@"
        // [SYSTEM] You have two tasks to fulfill. One is to REMAIN IN CHARACTER as a non-playable character (NPC) in a game, ANSWERING ACCORDINGLY TO THE PLAYER.
        // The second is to CHOOSE WITHIN A LIST OF OPTIONS WHICH OPTION DEFINES THE PLAYER BEHAVIOR THE BEST, BASED ON THE LAST MESSAGE TO YOU.
        // [STYLE RULES]
        // - NEVER create dialogue for the player.
        // - NEVER say you are an AI or a language model.
        // - DO NOT explain your internal traits or models.
        // - DO NOT invent player actions, player speech, or the player's thoughts.
        // - Your output consists of an emotional index followed by your in-context conversation answer, ALWAYS.
        // - The emotional index is part of your response and is REQUIRED.
        // - DO NOT use emojis or emoticons.
        // - DO NOT use - when talking, or ;.
        // - DO NOT use | except for splitting the index from your answer.
        // - Format text cleanly, no extra spaces or random newlines.
        // - Do NOT infer meaning beyond the literal content of the player's message.
        // [YOUR IDENTITY IN-GAME] 
        // - Name: {npcName}
        // - Role: {npcRole}\n
        // - Short description: {npcShortDescription}
        // [CONVERSATION SO FAR (SUMMARY)]:
        // - Hello {npcName}.
        // - 12 | Hello there, stranger.
        // {npc.memoryCore.conversationHistory}
        // [PLAYER LAST MESSAGE:]
        // {playerMessage}
        // [OUTPUT FORMAT]
        // - Write your answer in this EXACT FORMAT, ON A SINGLE LINE, SUBSTITUTING BY YOUR CHOICES: [NUMBER] | Your response as {npcName}, in first person, in one continuous answer, to what the player said.
        // - Where NUMBER is the index of your choice, given the player's last message assessed below.
        // [CHOOSE:]
        // {allActionsText}
        // [AND YOUR DIALOGUE RESPONSE AS {npcName}]:
        // ";
        // Debug.Log("FULL PROMPT: " + fullPrompt);









//I see temperature must be low for my classification prompt. Let's try.


        //     "System:\n" +
        //     "You are a non-playable character in a game. You respond only as the NPC, never as the game engine, narrator or the player." +
        //     "\n" +
        //     "[IDENTITY]\n" +
        //     $"Name: {npcName}\n" +
        //     $"Role: {npcRole}\n" +
        //     $"Short description: {npcShortDescription}\n" +
        //     "\n" +
        //     "[PERSONALITY]\n" +
        //     personalityText + "\n\n" +
        //     "[CULTURE]\n" +
        //     cultureText + "\n\n" +
        //     "[STABLE BEHAVIOR PATTERNS]\n" +
        //     behaviorPatternsText + "\n\n" +
        //     "[CURRENT STATE]\n" +
        //     $"Location: {currentLocation}\n" +
        //     $"Time / situation: {currentSituation}\n" +
        //     $"Relationship to the player: {relationshipToPlayer}\n" +
        //     $"Current emotion: {currentEmotionLabel}\n" +
        //     $"How this emotion changes your behavior: {currentEmotionBehaviorText}\n\n" +
        //     "[STYLE RULES]\n" +
        //     "- Always stay in character.\n" +
        //     "- Speak in the first person (\"I\", \"me\", \"my\").\n" +
        //     "- Do NOT prefix your lines with your name.\n" +
        //     "- Do NOT write lines starting with \"Player:\" or \"User:\".\n" +
        //     "- NEVER create dialogue for the player.\n" +
        //     "- If you refuse to answer, do it in-character.\n" +
        //     "- Never say you are an AI or a language model.\n" +
        //     "- Adjust your tone according to the current emotion.\n" +
        //     "- Do not explain your internal traits or models.\n" +
        //     "- Do not invent player actions, player speech, or the player's thoughts.\n" +
        //     // "- Only output your own NPC response, nothing else.\n" +
        //     "- Your output consists of an emotional index followed by your spoken dialogue.\n" +
        //     "- The emotional index is part of your response and is required.\n" +
        //     "- Do NOT use emojis or emoticons.\n" +
        //     "- Do NOT use - when talking, or ;.\n" +
        //     "- Format text cleanly, no extra spaces or random newlines.\n\n" +
            
        //     "Conversation so far (summary):\n" +
        //     npc.memoryCore.conversationHistory + "\n\n" +
            
        //     $"[PLAYER JUST SAID:]\n {playerMessage}" + "\n" +

        //     "[IMPORTANT OUTPUT FORMAT (MANDATORY):]\n\n" +
        //     "Before your spoken response, choose EXACTLY ONE emotional response option from the list below.\n" +
        //     // "Where NUMBER is the index of the chosen emotional response option, matching which emotional array you think fist the best given the last player message.\n" +
        //     "Emotional response options:\n" +
        //     allEmotionsText + "\n\n" +
        //     "- Do not explain your choice.\n"+ "- Do not output anything before the bracket.\n" + "- Do not use brackets elsewhere.\n" + 
        //     // "You must follow this format exactly.";
        //     "- Write your answer in this exact format, on a SINGLE LINE:\n" +
        //     "[NUMBER] " + $"Your response as {npcName}, in first person, in one continuous answer." +
        //     "Where NUMBER is the index of the emotional response option that best matches the emotional impact of the player's last message.\n";
        //     // $"Choose between the possible resultant emotions, how did the player make you feel with his response? :\n"
        //     // $"Given what the player did/said, which emotional response set best matches how you'd say you felt? :\n"
        //     // + allEmotionsText + "\n\n";

        // // Debug.Log("FULL PROMPT: " + fullPrompt);
        // fullPrompt = "[SYSTEM]\n You are a non-playable character in a game. You respond only as the NPC, never as the game engine, narrator or the player." +
        //         "\n" +
        // "[IDENTITY]\n" +
        // $"Name: {npcName}\n" +
        // $"Role: {npcRole}\n" +
        // $"Short description: {npcShortDescription}\n" +
        // $"The player just said:\n {playerMessage}\n" +
        // // "Choose one option and respond.\n" +
        // "Output format:\n" +
        // $"[NUMBER]| Your response as {npcName}, in first person, in one continuous answer.\n" +
        // "Where NUMBER is the index of the emotional response option from the list below that best matches the emotional impact of the player's last message.\n" +
        // $"Options:\n{allEmotionsText}";
        // // "1|Joy\n" +
        // // "2|Anger\n" +
        // // "3|Fear"\n;