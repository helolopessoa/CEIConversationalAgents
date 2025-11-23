using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{

    [HideInInspector]
    public string npcMessage = "Hello, I'm Alice.";
    private LlamaResponse lr;

    private string conversationHistory = "";
    private string npcName = "Alice";
    public void getNpcData() //will receive type NPC npc
    {
        //Temporary hardcoded data for testing
        // npcName = "Alice";
        // npcRole = "Shopkeeper";
        // npcShortDescription = "A friendly shopkeeper who loves to chat with customers.";
        // personalityText = "Cheerful, talkative, helpful.";
        // cultureText = "Grew up in a small village, values community and tradition.";
        // behaviorPatternsText = "Always greets customers warmly, offers assistance proactively.";
        // currentLocation = "In her shop, surrounded by various goods.";
        // currentSituation = "It's a busy day at the market.";
        // relationshipToPlayer = "Regular customer, friendly rapport.";
        // currentEmotionLabel = "Happy";
        // currentEmotionBehaviorText = "Smiles often, uses upbeat language.";
        // minSentences = 2;
        // maxSentences = 5;
        // conversationHistory = "Player: Hi Alice! How's business today?\nAlice: Oh, hello! Business is great, thank you for asking! How can I help you today?";
    }
    public string getNpcMessage(string playerMessage)
    {
        var prompt = buildPrompt(playerMessage);
        lr = PostLlamaAction(prompt);
        npcMessage = getNpcTextMessage(lr);
        return npcMessage;
    }

    private string buildPrompt(string playerMessage)
    {
        this.conversationHistory += $"\n-{playerMessage}";
        // this.conversationHistory += $"\n[- PLAYER SAID:]{playerMessage}";
        var npcName = "Alice";
        var npcRole = "Pirate";
        var npcShortDescription = "A friendly pirate who is on the run.";
        var personalityText = "Cheerful, talkative, sly and cunning.";
        var cultureText = "Grew up in a small village, values community and tradition.";
        var behaviorPatternsText = "Always greets others playfully, is always attentive to the surroundings.";
        var currentLocation = "Against a wall, in a corner of the street.";
        var currentSituation = "Running from the police, hiding in this street.";
        var relationshipToPlayer = "Former acquaintance, seen around town and taverns.";
        var currentEmotionLabel = "Happy and worried";
        var currentEmotionBehaviorText = "Smiles often, uses upbeat language, talks like a pirate.";
        var minSentences = 2;
        var maxSentences = 5;
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
            "- Only output your own NPC response, nothing else.\n" +
            "- Do NOT use emojis or emoticons.\n" +
            "- Do NOT use - when talking, or ;.\n" +
            "- Do NOT use \n" +
            "- Do NOT use emojis or emoticons.\n" +
            "- Format text cleanly, no extra spaces or random newlines.\n\n" +
            "Conversation so far (summary):\n" +
            conversationHistory + "\n\n" +
            $"Your response as {npcName}, in first person, in one continuous answer:\n";
        // var fullPrompt = $@"
        //     System:
        //     You are a non-playable character in a game. You respond only as the NPC, never as the game engine, narrator or the player.

        //     [IDENTITY]
        //     Name: {npcName}
        //     Role: {npcRole}
        //     Short description: {npcShortDescription}

        //     [PERSONALITY]
        //     {personalityText}

        //     [CULTURE]
        //     {cultureText}

        //     [STABLE BEHAVIOR PATTERNS]
        //     {behaviorPatternsText}

        //     [CURRENT STATE]
        //     Location: {currentLocation}
        //     Time / situation: {currentSituation}
        //     Relationship to the player: {relationshipToPlayer}
        //     Current emotion: {currentEmotionLabel}
        //     How this emotion changes your behavior: {currentEmotionBehaviorText}

        //     [STYLE RULES]
        //     - Always stay in character as {npcName}.
        //     - Speak in the first person (""I"", ""me"", ""my"").
        //     - Adjust your tone according to the current emotion.
        //     - Keep answers between {minSentences} and {maxSentences} sentences.
        //     - Do not explain your internal traits or models.
        //     - Never say you are an AI or a language model.
        //     - If you refuse to answer, do it in-character.
        //     - Do NOT prefix your lines with your name (no ""{npcName}:"").
        //     - Do NOT write lines starting with ""Player:"" or ""User:"".
        //     - NEVER create dialogue for the player.
        //     - Do not invent player actions, player speech, or the player's thoughts.
        //     - Only output your own NPC response, nothing else.
        //     - Do NOT use emojis or emoticons (no 😀, 😂, 🙂, ❤️, etc.).
        //     - If you feel like using an emoji, describe the feeling in words instead.
        //     - Format text cleanly: no extra spaces, no random newlines.

        //     Conversation so far:
        //     {conversationHistory}

        //     User:
        //     {playerMessage}

        //     Assistant:
        //     ";
        return fullPrompt;
    }

    private string getNpcTextMessage(LlamaResponse lr)
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
        this.conversationHistory = this.conversationHistory + $"\n" + response;
        return response;
    }


    public LlamaResponse PostLlamaAction(string message)
    {
        return LlamaAPI.postLlamaAction(message);
    }
}