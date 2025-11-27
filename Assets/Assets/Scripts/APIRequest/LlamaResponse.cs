[System.Serializable]
public class LlamaChoice
{
    public string text;
    public string finish_reason;
    public int index;
}

[System.Serializable]
public class LlamaUsage
{
    public int prompt_tokens;
    public int completion_tokens;
    public int total_tokens;
}

[System.Serializable]
public class LlamaResponse
{
    public string id;
    public string @object; // 'object' is a reserved keyword in C#
    public long created;
    public string model;
    public LlamaChoice[] choices;
    public LlamaUsage usage;
}