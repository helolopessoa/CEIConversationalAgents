using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{

    // private GameManager gameManager;
    [HideInInspector]
    public Emotion emotion;
    public GameManager gameManager;

    [HideInInspector]
    public Culture culture;

    [HideInInspector]
    public NPCMemoryCore memoryCore;

    [HideInInspector]
    public Personality personality;
    float prejudiceLevel;


    private float maxTrust = 100;
    [HideInInspector]
    public float currentTrust = 0.5f;
    private string lastMentalState = "Neutral";
    private string fuzzyResponseEmotion;
    private float neutralStateTimer = 0;
    private float stoppedStateTimer = 0;

    [HideInInspector]
    public string cultureString;
    [HideInInspector]
    public string roleString = "ProPeace";
    [HideInInspector]
    public string humorState = "neutral";
    public string nameString;
    public Sprite npcPortrait;
    public bool DownsideXRanger = false; //Downsider if True, Ranger if False
    public bool Leader = false;
    public bool ProPeace = false;
    public bool AntiPeace = false;
    public bool personalityType = true; // Analytical-Reserved if True, Expressive-Adaptive if False
    Dictionary<string, float> cultureAttrs = new Dictionary<string, float>() {
        { "dignity", 0 },
        { "collectivism", 0 },
        { "wealth", 0 },
        { "politeness", 0 },
        { "rationatity", 0 },
        { "trust_level", 0 },
    };

    float[] emotionBands = new float[4] { 0, 0.2f, 0.5f, 0.7f };

    void Awake()
    {
        memoryCore = GetComponent<NPCMemoryCore>();
        gameManager = FindObjectOfType<GameManager>();
        npcPortrait = transform.Find("Portrait").GetComponent<SpriteRenderer>().sprite;
        // var portrait = transform.Find("Portrait");
        // if(portrait != null)
        // {
        //     var portrait1 = portrait.GetComponent<SpriteRenderer>();
        //     if(portrait1 == null)
        //     {
        //         Debug.LogWarning($"[NPC] NPC Portrait is null for NPC: {memoryCore.npcName}");
        //     }
        //     else{
        //         npcPortrait = portrait1.sprite;
        //         if(npcPortrait == null)
        //         {
        //             Debug.LogWarning($"[NPC] NPC Portrait sprite is null for NPC: {memoryCore.npcName}");
        //         }
        //     }
        // }else{
        //     Debug.LogWarning($"[NPC] NPC Portrait transform not found for NPC: {memoryCore.npcName}");
        // }
    }

    void Start()
    {
        memoryCore.StartTimer();
        Debug.Log($"[NPC] Starting NPC: {memoryCore.npcName}");
        GenerateInitialEmotion();
        // StartCoroutine(CallFuzzyModel());
        GenerateInitialPersonality();
        GenerateInitialCulture();
        prejudiceLevel = Random.Range(0f, 1f);
        humorState = emotion.GetName();
        UpdateCurrentState();
        memoryCore.npcName = nameString;
        roleString = Leader ? "Leader" : ProPeace ? "ProPeace" : AntiPeace ? "AntiPeace" : "ProPeace";
        // npcPortrait = transform.Find("Portrait").GetComponent<SpriteRenderer>().sprite;
    }


    void Update()
    {

        float dt = Time.deltaTime;

        emotion?.UpdateEmotion(dt);

        UpdateCurrentState();

        cultureAttrs["trust_level"] = currentTrust;
        humorState = emotion?.GetName();
        if (gameManager.saveGame)
        {
            gameManager.saveGame = false;
            memoryCore.EndTimer();
            gameManager.SaveGameData(memoryCore.GetSaveGameData());
        }

    }

    /// <summary>
    /// Updates the trust level value.
    /// </summary>
    void UpdateTrustLevel()
    {
        Dictionary<string, int> trustInf = AllEmotions.GetTrustInfluence();
        string mentalStateName = emotion.GetMentalStateName();
        int infValue = trustInf[mentalStateName];
        currentTrust = currentTrust + infValue * prejudiceLevel * (1 / maxTrust);
    }


    /// <summary>
    /// Generates the initial RANDOM personality.
    /// </summary>
    void GenerateInitialPersonality()
    {
        float[] newPersonality = new float[5];   
        newPersonality = Personality.GetPersonalityValueDict()[personalityType ? 0 : 1];
        personality = new Personality(newPersonality);
        personality.personalityIndexSet = personalityType;
        }

    /// <summary>
    /// Generates RANDOM bios emotion.
    /// </summary>
    void GenerateInitialEmotion()
    {

        float[] randomEmotion = Emotion.GetRandomEmotion();
        float[] newEmotion = new float[4];

        for (int i = 0; i < newEmotion.Length; i++)
        {
            newEmotion[i] = randomEmotion[i];
            // newEmotion[i] = 0;
        }
        emotion = new Emotion(newEmotion);

    }

    // Generating RANDOM culture
    /// <summary>
    /// Generates the RANDOM initial culture.
    /// </summary>
    void GenerateInitialCulture()
    {
        var cultureIndex = DownsideXRanger ? Culture.downsideIndex : Culture.rangerIndex;
        float[] newCulture = new float[6];
        Dictionary<string, float[]> cultures = Culture.GetCulturesValueDict();
        cultureString = Culture.Cultures[cultureIndex];
        // Debug.Log(cultureString);
        for (int i = 0; i < newCulture.Length; i++)
        {
            newCulture[i] = cultures[cultureString][i];
        }
        culture = new Culture(newCulture);
        culture.cultureIndex = cultureIndex;
        culture.LoadCultureDict(cultureAttrs);
    }

    /// <summary>
    /// Updates NPC humor (aka their current state)
    /// </summary>
    void UpdateCurrentState()
    {
        Dictionary<string, int> trustInf = AllEmotions.GetTrustInfluence();
        string mentalStateName = emotion.GetMentalStateName();
        // string mentalStateName = emotion.GetMentalStateName(fuzzyResponseEmotion);
        humorState = mentalStateName.ToLower();
        // Debug.Log($"[NPC] UpdateCurrentState: humorState -- {humorState}");

    }

    /// <summary>
    /// Dispatchs the state of the player.
    /// </summary>
    /// <param name="playerState">Player current action state.</param>
    public void DispatchPlayerState(string playerState)
    {
        Debug.Log($"[NPC] DispatchPlayerState: playerState -- {playerState}");
        Dictionary<string, string[]> stateEmo = ActionEmotions.GetDictTalking();
        Dictionary<string, string> stateAttrs = ActionEmotions.GetCultureAttributes();
        Dictionary<string, float[]> allEmo = AllEmotions.GetDict();
        stateEmo.TryGetValue(playerState, out string[] emotionsArray);
        string attrName = stateAttrs[playerState];
        float rat = 1 - culture.GetRationality();
        float attrValue = cultureAttrs[attrName];
        float result = Mathf.Sqrt(attrValue * rat);
        string resEmotion = emotionsArray[0]; // a starter


        for (int i = 1; i < emotionBands.Length; i++)
        {
            if (result > emotionBands[i]) // defining rationality for intensity of emotion felt
            {
                resEmotion = emotionsArray[i];
            }
        }
        UpdateEmotionByEvent(allEmo[resEmotion]);
    }

    /// <summary>
    /// Updates the emotion by event.
    /// </summary>
    /// <param name="eventEmotion">Event emotion.</param>
    void UpdateEmotionByEvent(float[] eventEmotion)
    {
        float[] newEmotion = new float[4];
        float[] p = personality.GetPersonality();
        float[,] pFactors = Personality.PositiveFactors;
        float[,] nFactors = Personality.NegativeFactors;

        // Generate new emotion based on Personality Traits and Factors
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (eventEmotion[i] > 0)
                    newEmotion[i] += eventEmotion[i] * p[j] * pFactors[j, i];
                else
                    newEmotion[i] += eventEmotion[i] * p[j] * nFactors[j, i];
            }
            newEmotion[i] = newEmotion[i] / 5;
        }

        //// Add new generated emotion
        emotion.AddEmotion(newEmotion);
        emotion.ClampCurrentEmotion();
        // StartCoroutine(CallFuzzyModel());
        UpdateCurrentState();
        UpdateTrustLevel();
    }



    
    
    public string GetCurrentEmotionString()
    {
        return emotion.GetName();
    }



    // private IEnumerator CallFuzzyModel()
    // {
    //     // Debug.Log("[ChatController] CallModelClassification(prompt, playerMessage) called.");
    //     FuzzyResponse fr = null;
    //     yield return FuzzyAPI.PostFuzzyEmotionalInput(emotion.GetEmotion(), (resp) => fr = resp);
    //     if (fr == null)
    //     {
    //         yield break;
    //     }
    //     else
    //     {
    //         Debug.Log("[NPC] Fuzzy response received.");
    //         Debug.Log(fr.emotion);
    //     }
    //     fuzzyResponseEmotion = fr.emotion;
    // }
}