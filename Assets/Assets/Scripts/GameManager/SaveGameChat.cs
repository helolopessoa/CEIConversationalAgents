using UnityEngine;
using System.Text;
using System.IO;

public class SaveGameChat : MonoBehaviour
{
    private static string path = "C:/Users/helopessoa/Documents/Mestrado/CEIConversationalAgents/Assets/Assets/GameData";
    public static void SaveGameData(SaveGameData data)
    {
        Debug.Log("[SaveGameChat] Endgame called. Saving chat logs.");
        string folderPath = path + "/GameLogs/Baseline" + data.npcName;
        // string folderPath = path + "/GameLogs/Scaffolded" + data.npcName;
        Directory.CreateDirectory(folderPath);

        string fileName = "GAME_RESUME_" + data.npcName + "_" + data.npcRoleTitle + "_" + data.npcCulture + "_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
        string fullPath = Path.Combine(folderPath, fileName);

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
        sb.AppendLine("Conversation: " + data.chatSummary);
        
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

        public static void SaveGameChatData(SaveGameData data)
    {
        Debug.Log("[SaveGameChat] Endgame called. Saving chat logs.");
        string folderPath = path + "/GameLogs/ConversationsSummary/Baseline";
        // string folderPath = path + "/GameLogs/ConversationsSummary/Scaffoldeds";
        Directory.CreateDirectory(folderPath);

        string fileName = "CONVERSATION_RESUME_" + data.npcName + "_" + data.npcRoleTitle + "_" + data.npcCulture + "_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
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


}