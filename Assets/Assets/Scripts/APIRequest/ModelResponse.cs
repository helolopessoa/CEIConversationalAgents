//OPENAI API OUTPUT VERSION
using System;
using UnityEngine;


[Serializable]
public class ModelResponse
{
    public string id; 
    // public string @object; //LLAMA OUTPUT VERSION

    public long created;
    public string model;
    public Choice[] choices;
    public Usage usage;
}

[Serializable]
public class ChatCompletionMessage
{
    public string role;
    public string content;
}

[Serializable]
public class Choice
{
    public int index;
    public string finish_reason;
    public ChatCompletionMessage message; //LLAMA OUTPUT VERSION -> public string text; Rest is the same
}

[System.Serializable]
public class Usage
{
    public int prompt_tokens;
    public int completion_tokens;
    public int total_tokens;
}


[System.Serializable]
public class ModelClassificationResponse
{
    public string result;
}