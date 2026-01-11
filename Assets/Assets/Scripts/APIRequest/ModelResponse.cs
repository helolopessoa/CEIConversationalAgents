[System.Serializable]
public class Choice
{
    public string text;
    public string finish_reason;
    public int index;
}

[System.Serializable]
public class Usage
{
    public int prompt_tokens;
    public int completion_tokens;
    public int total_tokens;
}

[System.Serializable]
public class ModelResponse
{
    public string id;
    public string @object; // 'object' is a reserved keyword in C#
    public long created;
    public string model;
    public Choice[] choices;
    public Usage usage;
}