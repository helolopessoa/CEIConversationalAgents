using System.Collections;
using UnityEngine;
using System.Net;
using System.IO;
using System;
using System.Text;



public static class FuzzyAPI
{
    private static string apiURL = "http://localhost:11434";
    private static string apiFuzzyURL = apiURL + "/fuzzyapi";
// // export apiURL = "http://127.0.0.1:5000/"


        public static FuzzyResponse getFuzzyEmotionalResponse()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiFuzzyURL);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();
            return JsonUtility.FromJson<FuzzyResponse>(json);

      }

    public static void postFuzzyEmotionalInput(float[] currentEmotion)
    {
        string[] emotionKeys = { "axeAF", "axeDT", "axeSJ", "axeAS" };
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiFuzzyURL);
        var postData = "&" + emotionKeys[0] + "=" + Uri.EscapeDataString(currentEmotion[0].ToString());
        for (var i=1 ; i<4 ; i++)
        {
            postData += "&"+ emotionKeys[i] + "=" + Uri.EscapeDataString(currentEmotion[i].ToString());
        }
        var data = Encoding.ASCII.GetBytes(postData);
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;

        using (var stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }

        var response = (HttpWebResponse)request.GetResponse();

        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
    }
}
