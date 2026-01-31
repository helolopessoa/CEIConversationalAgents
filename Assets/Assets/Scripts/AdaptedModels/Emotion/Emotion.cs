using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Emotion {

    // These four positions represent the 
    // 4 axis of Plutchik's Emotion Model
    // [0] AngerXFear [Range(-1f, 1f)]
    // [1] DisgustXTrust [Range(-1f, 1f)]
    // [2] SadnessXJoy [Range(-1f, 1f)]
    // [3] AntecipationXSurprise [Range(-1f, 1f)]
    
    float[] currentEmotion = new float[4];
    float[] influentEmotion = new float[4];

    // Represents the bios emotion
    float[] bios = new float[4];

    // Represents the linear velocity that makes
    // the current emotion go back to the bios emotion
    float bioTendency = 0.01f;

    // The maximum time to start changing the 
    float bioTendencyTime = 1.0f;

    // Represents the current computed time (based on DeltaTime)
    float currentTime = 0;


    public Emotion(float[] newEmotion) {
        InitializeEmotion(newEmotion);
        LoadBios();
        // GetFuzzyEmotion();
        SetMostInfluentEmotion();
    }


    public float[] GetEmotion() {
        return currentEmotion;
    }


    public float[] GetMostInfluentEmotion() {
       return influentEmotion;
    }

    /// <summary>
    /// Apply the initial emotion values to the bios.
    /// </summary>
    void LoadBios () {
        for (int i=0; i< currentEmotion.Length; i++) {
            bios[i] = currentEmotion[i];
        }
    }

    void InitializeEmotion(float[] newEmotion) {
        for (int i = 0; i < newEmotion.Length; i++) {
            currentEmotion[i] = newEmotion[i];
        }
    }

    public void AddEmotion(float[] otherEmotion)
    {
        for (int i = 0; i < otherEmotion.Length; i++) {
            currentEmotion[i] += otherEmotion[i];
        }
    }

    /// <summary>
    /// Verify if the current emotion is equals to bios.
    /// </summary>
    /// <returns><c>true</c>, if bios was ised, <c>false</c> otherwise.</returns>
    bool IsBios() {
        for (int i = 0; i < currentEmotion.Length; i++) {
            if (currentEmotion[i] != bios[i]) {
                return false;
            }
        }
        return true;
    }
	
	// Update is called once per frame
	public void UpdateEmotion (float dt) {
        currentTime += dt;

        // If time passes the biotendency max time,
        // make the current emotion linearly go back
        // to the bios
        if (currentTime > bioTendencyTime && !IsBios()) {

            for (int i = 0; i < currentEmotion.Length; i++) {
                if (currentEmotion[i] < bios[i])
                {
                    if (currentEmotion[i] + bioTendency >= bios[i]) {
                        currentEmotion[i] = bios[i];
                    }
                    else {
                        currentEmotion[i] += bioTendency;
                    }
                }
                else if (currentEmotion[i] > bios[i])
                {
                    if (currentEmotion[i] - bioTendency <= bios[i]) {
                        currentEmotion[i] = bios[i];
                    }
                    else {
                        currentEmotion[i] -= bioTendency;
                    }
                }
            }
            currentTime = 0;
        }

        // Clamp after update
        ClampCurrentEmotion();
        // PostFuzzyEmotion();
        SetMostInfluentEmotion();
    }

    public string GetName() {
        Dictionary<string, float[]> allEmotions = AllEmotions.GetDict();
        return allEmotions.FirstOrDefault(x => x.Value.SequenceEqual(currentEmotion)).Key;
    }


    public string GetMostInfluentName() {
       Dictionary<string, float[]> allEmotions = AllEmotions.GetDict();

       return allEmotions.FirstOrDefault(x => x.Value.SequenceEqual(influentEmotion)).Key;
    }

    // public string GetMentalStateName(string fuzzyNameEmotion)
    public string GetMentalStateName()
    {
        Dictionary<string, string> mentalStates = AllEmotions.GetMentalState();
        string name = GetMostInfluentName();
        return mentalStates[name];
        // Dictionary<string, string> mentalStates = AllEmotions.GetMentalState();
        // string name = GetFuzzyEmotion();
        // return mentalStates[fuzzyNameEmotion];
    }


    public static float[] GetRandomEmotion() {
        Dictionary<string, float[]> allEmotions = AllEmotions.GetDict();
        int dictSize = allEmotions.Count;
        int randomIndex = UnityEngine.Random.Range(0, dictSize);

        return allEmotions.ElementAt(randomIndex).Value;
    }


    public void ClampCurrentEmotion() {
        for (int i = 0; i < currentEmotion.Length; i++) {
            currentEmotion[i] = Mathf.Clamp(currentEmotion[i], -1.0f, 1.0f);
        }
    }


    public void ResetCurrentEmotion() {
        for (int i = 0; i < currentEmotion.Length; i++) {
            currentEmotion[i] = 0;
        }
    }

    /// <summary>
    /// Sets the most influent emotion.
    /// </summary>
    public void SetMostInfluentEmotion() {
        float biggestValue = Mathf.Abs(currentEmotion[0]);

        for (int i = 1; i < currentEmotion.Length; i++) {
            float currentValue = Mathf.Abs(currentEmotion[i]);
            influentEmotion[i] = 0;

            if (currentValue > biggestValue) {
                biggestValue = currentValue;
            }
        }

        for (int i = 0; i < currentEmotion.Length; i++) {
            float currentValue = Mathf.Abs(currentEmotion[i]);

            if ((biggestValue - currentValue) <= 0) {
                influentEmotion[i] = currentEmotion[i];
            }
        }

        int count = 0;
        for (int i = 0; i < influentEmotion.Length; i++) {
            float value = Mathf.Abs(influentEmotion[i]);
            if (value > 0) {
                count += 1;
            }
        }

        for (int i = 0; i < influentEmotion.Length; i++)
        {
            float value = Mathf.Abs(influentEmotion[i]);
            float sign = Mathf.Sign(influentEmotion[i]);

            if (count <= 1) {
                if (value <= 0.1f) {
                    influentEmotion[i] = 0;
                }
                else if (value <= 0.3f) {
                    influentEmotion[i] = 0.2f;
                }
                else if (value <= 0.5f) {
                    influentEmotion[i] = 0.5f;
                }
                else {
                    influentEmotion[i] = 1.0f;
                }
            }
            else if (count == 2) {
                if (value <= 0.5f) {
                    influentEmotion[i] = 0;
                }
                else {
                    influentEmotion[i] = 0.5f;
                }
            }
            else { 
                influentEmotion[i] = 0;
            }

            influentEmotion[i] = sign * influentEmotion[i];
        }
    }

}
