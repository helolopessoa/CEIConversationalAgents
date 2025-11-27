using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Culture {

    // These six attributes are based on 
    // Geert Hofstede's culture model,
    // focusing on Boloni's interpretation
    // [Range(0f, 1f)]
    private float time;
    // [Range(0f, 1f)]
    private float wealth;
    // [Range(0f, 1f)]
    private float dignity;
    // [Range(0f, 1f)]
    private float politeness;
    // [Range(0f, 1f)]
    private float rationality;
    // [Range(0f, 1f)]
    private float collectivism;

    private float[] currentCulture = new float[6];

    public static List<string> GetCulturalAtrributesList()
    {
        return new List<string>()
        {
            "dignity",
            "collectivism",
            "trust_level",
            "wealth",
            "politeness"
        };
    }


    public static Dictionary<string, float[]> GetCulturesValueDict()
    {
        return new Dictionary<string, float[]>() {
            { "Ogre", new float[6] { 0.15f, 0.63f, 0.31f, 0.02f, 0.15f, 0.93f } },
            { "Traveller", new float[6] { 0.28f, 0.25f, 0.82f, 0.75f, 0.39f, 0.43f} },
            { "Ranger", new float[6] { 0.72f, 0.56f, 0.88f, 0.73f, 0.76f, 1f } },
            { "Adventurer", new float[6] { 0.83f, 0.24f, 0.65f, 0.58f, 0.25f, 0.77f} },
            { "Downside", new float[6] { 0.05f, 1f, 0.02f, 0.15f, 0.93f, 0.22f} },
        };
    }

    public static string[] Cultures = new string[] { "Ogre", "Traveller", "Ranger", "Adventurer", "Downside" };
        public Culture(float[] newCulture) {
        time = newCulture[0];
        wealth = newCulture[1];
        dignity = newCulture[2];
        politeness = newCulture[3];
        rationality = newCulture[4];
        collectivism = newCulture[5];

        InitializeCulture(newCulture);
    }

    void InitializeCulture(float[] newCulture) {
        for (int i = 0; i < currentCulture.Length; i++)
        {
            currentCulture[i] = newCulture[i];
        }
    }

    public void LoadCultureDict(Dictionary<string, float> dict) {
        dict["wealth"] = wealth;
        dict["dignity"] = dignity;
        dict["politeness"] = politeness;
        dict["rationality"] = rationality;
        dict["collectivism"] = collectivism;
    }

    public float[] GetCulture() {
        return currentCulture;
    }

    public float GetTime() {
        return time;
    }

    public float GetWealth() {
        return wealth;
    }

    public float GetDignity() {
        return dignity;
    }

    public float GetPoliteness() {
        return politeness;
    }

    public float GetRationality() {
        return rationality;
    }

    public float GetCollectivism() {
        return collectivism;
    }
}
