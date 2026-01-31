using UnityEngine;
using System.Text;
using System.IO;
// using System.Reflection;

public class SaveGameChat : MonoBehaviour
{
    public static void SaveGameData(SaveGameData data)
    {
        Debug.Log("[SaveGameChat] Endgame called. Saving chat logs.");
        string folderPath = "C:/Users/helopessoa/Documents/Mestrado/CEIConversationalAgents/Assets/Assets" + "/GameLogs";
        Directory.CreateDirectory(folderPath);


        string fileName = "game_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
        string fullPath = Path.Combine(folderPath, fileName);


        StringBuilder sb = new StringBuilder();
        // sb.AppendLine("Play Time: " + playTime);
        sb.AppendLine("Date: " + System.DateTime.Now);


        File.WriteAllText(fullPath, sb.ToString());


        Debug.Log("Saved game data at: " + fullPath);
}


    
}