using UnityEngine;
using System.Text;
using System.IO;

public class SaveGameChat : MonoBehaviour
{
    // private static string path = "C:/Users/helopessoa/Documents/Mestrado/CEIConversationalAgents/Assets/Assets/GameData" + "/LLMAsPlayer";
        // private static string path = "C:/Users/helopessoa/Documents/Mestrado/CEIConversationalAgents/Assets/Assets/GameData" + "/Scripts/NeutralInquiry";
    // private static string path = "C:/Users/helopessoa/Documents/Mestrado/CEIConversationalAgents/Assets/Assets/GameData" + "/Scripts/Provocation";
    // private static string path = "C:/Users/helopessoa/Documents/Mestrado/CEIConversationalAgents/Assets/Assets/GameData" + "/Scripts/CulturalMisunderstanding";
    private static string path = "C:/Users/helopessoa/Documents/Mestrado/CEIConversationalAgents/Assets/Assets/GameData" + "/Scripts/Negotiation";
    // private static string path = "C:/Users/helopessoa/Documents/Mestrado/CEIConversationalAgents/Assets/Assets/GameData" + "/Scripts/CulturalProbes";

    public static void SaveGameData(SaveGameData data, bool baselineTests)
    {
        Debug.Log("[SaveGameChat] Endgame called. Saving chat logs.");
        string folderPath = "";
        if (baselineTests)
        {
            folderPath = path +"/Baseline/" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        }
        else
        {
            folderPath = path  +"/Scaffold/" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss");           
        }
        Directory.CreateDirectory(folderPath + "/GameLogs");

        string fileName = "GAME_RESUME_" + data.npcName + "_" + data.npcRoleTitle + "_" + data.npcCulture + "_" + ".txt";
        string fullPath = Path.Combine(folderPath  + "/GameLogs", fileName);

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Date: " + System.DateTime.Now);
        
        int minutes = Mathf.FloorToInt(data.playTime / 60f);
        int seconds = Mathf.FloorToInt(data.playTime % 60f);
        sb.AppendLine("Play Time: " + $"{minutes:D2} minutes and {seconds:D2} seconds");
        
        sb.AppendLine("Player: " + data.playerName);
        sb.AppendLine("NPC: " + data.npcName);
        sb.AppendLine("NPC Role: " + data.npcRoleTitle);
        sb.AppendLine("NPC Cuture: " + data.npcCulture);
        sb.AppendLine("NPC Personality Type: " + data.npcPersonality);
        sb.AppendLine("Conversation: \n" + data.chatSummary);
        
        sb.AppendLine("\n");
        
        sb.AppendLine("Classifications:");
        foreach (var classification in data.classifications)
        {
            sb.AppendLine(classification);
        }
        sb.AppendLine("\n");
        sb.AppendLine("Emotions:");
        foreach (var emotion in data.emotions)
        {
            sb.Append(emotion+", ");
        }
        sb.AppendLine("\nSuccess of Peace Treaty: " + data.peaceTreatySigned);

        File.WriteAllText(fullPath, sb.ToString());
        Debug.Log("Saved game data at: " + fullPath);
        SaveGameChatData(data, baselineTests, folderPath);
        SaveNPCGameChatData(data, baselineTests, folderPath);
    }

    // public static void SaveGameChatFEDsData(SaveGameData data)
    // {
    //     Debug.Log("[SaveGameChat] Endgame called. Saving chat logs.");
    //     string folderPath = path + "/GameLogs/FED/Baseline";
    //     // string folderPath = path + "/GameLogs/FED/Scaffolded";
    //     Directory.CreateDirectory(folderPath);

    //     string fileName = "FED_RESUME_" + data.npcName + "_" + data.npcRoleTitle + "_" + data.npcCulture + "_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
    //     string fullPath = Path.Combine(folderPath, fileName);

    //     StringBuilder sb = new StringBuilder();
    //     sb.AppendLine(data.fedConversationHistory);

    //     File.WriteAllText(fullPath, sb.ToString());
    //     Debug.Log("Saved game data at: " + fullPath);
    // }

    private static void SaveGameChatData(SaveGameData data, bool baselineTests, string folderPath)
    {
        folderPath += "/ConversationResume";
        Directory.CreateDirectory(folderPath);

        string fileName = "CONVERSATION_RESUME_" + data.npcName + "_" + data.npcRoleTitle + "_" + data.npcCulture + ".txt";
        string fullPath = Path.Combine(folderPath, fileName);

        StringBuilder sb = new StringBuilder();
        sb.AppendLine(data.chatSummary);
        // foreach (var classification in data.classifications)
        // {
        //     sb.AppendLine(classification);
        // }

        File.WriteAllText(fullPath, sb.ToString());
        Debug.Log("Saved game data at: " + fullPath);
    }

    private static void SaveNPCGameChatData(SaveGameData data, bool baselineTests, string folderPath)
    {
        folderPath += "/NPCResume";
        Directory.CreateDirectory(folderPath);

        string fileName = "NPC_RESUME_" + data.npcName + "_" + data.npcRoleTitle + "_" + data.npcCulture + ".txt";
        string fullPath = Path.Combine(folderPath, fileName);

        StringBuilder sb = new StringBuilder();
        sb.AppendLine(data.npcMessageHistory);
        // foreach (var classification in data.classifications)
        // {
        //     sb.AppendLine(classification);
        // }

        File.WriteAllText(fullPath, sb.ToString());
        Debug.Log("Saved game data at: " + fullPath);
    }    


}