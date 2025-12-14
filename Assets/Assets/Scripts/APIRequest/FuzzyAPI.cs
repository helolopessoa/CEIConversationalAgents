using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class FuzzyAPI
{
    private static string apiURL = "http://127.0.0.1:11434";
    private static string apiFuzzyURL = apiURL + "/fuzzyapi";

    /// <summary>
    /// GET /fuzzyapi -> returns FuzzyResponse asynchronously.
    /// Usage: StartCoroutine(FuzzyAPI.GetFuzzyEmotionalResponse(OnFuzzyResponse));
    /// </summary>
    public static IEnumerator GetFuzzyEmotionalResponse(Action<FuzzyResponse> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(apiFuzzyURL))
        {
            www.timeout = 10;

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("[FUZZY] GET error: " + www.error);
                callback?.Invoke(null);
            }
            else
            {
                string json = www.downloadHandler.text;
                // Debug.Log("[FUZZY] GET response: " + json);
                FuzzyResponse resp = JsonUtility.FromJson<FuzzyResponse>(json);
                callback?.Invoke(resp);
            }
        }
    }

    /// <summary>
    /// POST /fuzzyapi with 4 emotion axes as x-www-form-urlencoded
    /// Usage: StartCoroutine(FuzzyAPI.PostFuzzyEmotionalInput(currentEmotion));
    /// </summary>
    public static IEnumerator PostFuzzyEmotionalInput(float[] currentEmotion, Action<bool> callback = null)
    {
        if (currentEmotion == null || currentEmotion.Length < 4)
        {
            Debug.LogError("[FUZZY] currentEmotion must have 4 values.");
            callback?.Invoke(false);
            yield break;
        }

        string[] emotionKeys = { "axeAF", "axeDT", "axeSJ", "axeAS" };

        WWWForm form = new WWWForm();
        for (int i = 0; i < 4; i++)
        {
            form.AddField(emotionKeys[i], currentEmotion[i].ToString(System.Globalization.CultureInfo.InvariantCulture));
        }

        using (UnityWebRequest www = UnityWebRequest.Post(apiFuzzyURL, form))
        {
            www.timeout = 10;

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("[FUZZY] POST error: " + www.error);
                callback?.Invoke(false);
            }
            else
            {
                // Optional: read server response if you want
                string responseString = www.downloadHandler.text;
                // Debug.Log("[FUZZY] POST response: " + responseString);
                callback?.Invoke(true);
            }
        }
    }
}



// using System.Collections;
// using UnityEngine;
// using System.Net;
// using System.IO;
// using System;
// using System.Text;



// public static class FuzzyAPI
// {
//     private static string apiURL = "http://localhost:11434";
//     private static string apiFuzzyURL = apiURL + "/fuzzyapi";
// // // export apiURL = "http://127.0.0.1:5000/"


//         public static FuzzyResponse getFuzzyEmotionalResponse()
//         {
//             HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiFuzzyURL);
//             request.Method = "GET";
//             HttpWebResponse response = (HttpWebResponse)request.GetResponse();
//             StreamReader reader = new StreamReader(response.GetResponseStream());
//             string json = reader.ReadToEnd();
//             return JsonUtility.FromJson<FuzzyResponse>(json);

//       }

//     public static void postFuzzyEmotionalInput(float[] currentEmotion)
//     {
//         string[] emotionKeys = { "axeAF", "axeDT", "axeSJ", "axeAS" };
//         HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiFuzzyURL);
//         var postData = "&" + emotionKeys[0] + "=" + Uri.EscapeDataString(currentEmotion[0].ToString());
//         for (var i=1 ; i<4 ; i++)
//         {
//             postData += "&"+ emotionKeys[i] + "=" + Uri.EscapeDataString(currentEmotion[i].ToString());
//         }
//         var data = Encoding.ASCII.GetBytes(postData);
//         request.Method = "POST";
//         request.ContentType = "application/x-www-form-urlencoded";
//         request.ContentLength = data.Length;

//         using (var stream = request.GetRequestStream())
//         {
//             stream.Write(data, 0, data.Length);
//         }

//         var response = (HttpWebResponse)request.GetResponse();

//         var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
//     }
// }
