using System.Collections;
using UnityEngine;
// using System.Net;
using System.IO;
using System;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;

public static class LlamaAPI
{
    private static string apiURL = "http://localhost:11434";
    private static string apiLlamaURL = apiURL + "/llamaapi";

    public static IEnumerator TestPing(Action<string> callback)
    {
        string url = apiURL + "/ping";
        Debug.Log("[PING] Starting ping to " + url);

        UnityWebRequest www = UnityWebRequest.Get(url);
        www.timeout = 5;

        yield return www.SendWebRequest();

        Debug.Log("[PING] Result = " + www.result + " | Error = " + www.error);

        if (www.result != UnityWebRequest.Result.Success)
        {
            callback?.Invoke(null);
        }
        else
        {
            callback?.Invoke(www.downloadHandler.text);
        }
    }


    public static IEnumerator PostLlamaAction(string prompt, Action<LlamaResponse> callback)
    {

        var body = new { prompt = prompt };
        // Debug.Log("[LlamaAPI] Sending prompt to LLaMA: " + prompt);
        string json = JsonConvert.SerializeObject(body);
        UnityWebRequest www = new UnityWebRequest(apiLlamaURL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        www.timeout = 300;  // aumenta o timeout
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error calling LLama: " + www.error);
            callback(null);
        }
        else
        {
            string jsonResponse = www.downloadHandler.text;
            LlamaResponse response = JsonUtility.FromJson<LlamaResponse>(jsonResponse);
            callback(response);
        }
        
    }
}
