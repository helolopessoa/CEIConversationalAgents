using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{

    [HideInInspector]
    public Emotion emotion;

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
    private float neutralStateTimer = 0;
    private float stoppedStateTimer = 0;

    [HideInInspector]
    public string cultureString = "Traveller";
    [HideInInspector]
    public string humorState = "neutral";
    public string personalityString = "Type1";
    public string nameString;


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
    }

    void Start()
    {
        Debug.Log($"[NPC] Starting NPC: {memoryCore.npcName}");
        GenerateInitialEmotion();
        GenerateInitialPersonality();
        GenerateInitialCulture();
        prejudiceLevel = Random.Range(0f, 1f);

        culture.LoadCultureDict(cultureAttrs);
        humorState = emotion.GetName();
        UpdateCurrentState();

    }


    void Update()
    {

        float dt = Time.deltaTime;

        emotion?.UpdateEmotion(dt);

        // UpdateBehavior(dt);
        // UpdateCurrentState();

        cultureAttrs["trust_level"] = currentTrust;

        humorState = emotion?.GetName();

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

        for (int i = 0; i < newPersonality.Length; i++)
        {
            float rand = Random.Range(0f, 1f) * 10f;
            newPersonality[i] = Mathf.Round(rand) * 0.1f;
        }
        personality = new Personality(newPersonality);
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
            //newEmotion[i] = randomEmotion[i];
            newEmotion[i] = 0;
        }
        emotion = new Emotion(newEmotion);

    }

    // Generating RANDOM culture
    /// <summary>
    /// Generates the RANDOM initial culture.
    /// </summary>
    void GenerateInitialCulture()
    {
        float[] newCulture = new float[6];
        Dictionary<string, float[]> cultures = Culture.GetCulturesValueDict();
        int rand = Random.Range(0, 5);
        cultureString = Culture.Cultures[rand];
        // Debug.Log(cultureString);
        for (int i = 0; i < newCulture.Length; i++)
        {
            newCulture[i] = cultures[cultureString][i];
        }
        culture = new Culture(newCulture);
    }

    /// <summary>
    /// Updates NPC humor (aka their current state)
    /// </summary>
    void UpdateCurrentState()
    {
        Dictionary<string, int> trustInf = AllEmotions.GetTrustInfluence();
        string mentalStateName = emotion.GetMentalStateName();
        humorState = mentalStateName.ToLower();
    }

    /// <summary>
    /// Dispatchs the state of the player.
    /// </summary>
    /// <param name="playerState">Player current action state.</param>
    public void DispatchPlayerState(string playerState)
    {

        Dictionary<string, string[]> stateEmo = ActionEmotions.GetDict();
        Dictionary<string, string> stateAttrs = ActionEmotions.GetCultureAttributes();
        Dictionary<string, float[]> allEmo = AllEmotions.GetDict();

        string[] emotionsArray = stateEmo[playerState];
        string attrName = stateAttrs[playerState];
        float rat = 1 - culture.GetRationality();
        float attrValue = cultureAttrs[attrName];
        float result = Mathf.Sqrt(attrValue * rat);
        string resEmotion = emotionsArray[0];

        for (int i = 1; i < emotionBands.Length; i++)
        {
            if (result > emotionBands[i])
            {
                resEmotion = emotionsArray[i];
            }
        }

        UpdateEmotionByEvent(allEmo[resEmotion]);
        UpdateTrustLevel();
        UpdateCurrentState();
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

    }

}