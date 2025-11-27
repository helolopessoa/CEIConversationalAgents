using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personality {

    // These four positions represent the 
    // 5 attributes of OCEAN's Personality Model
    // [0] Openness [Range(-1f, 1f)]
    // [1] Conscientiousness [Range(-1f, 1f)]
    // [2] Extraversion [Range(-1f, 1f)]
    // [3] Agreeableness [Range(-1f, 1f)]
    // [4] Neuroticism [Range(-1f, 1f)]
    float[] currentPersonality = new float[5];


    public static float[,] PositiveFactors = new float[5, 4] {
        { -1, 1, 1, -1 },
        { 0, 1, 0, 0 },
        { -1, 1, 1, 1 },
        { 0, 0, 1, 1 },
        { 1, -1, -1, 1 }
    };

    public static float[,] NegativeFactors = new float[5, 4]{
        { -1, -1, -1, 1 },
        { 0, 0, 1, 1 },
        { 0, 0, 1, -1 },
        { 0, -1, 0, 0 },
        { 1, 1, 1, -1 }
    };

    public Personality(float[] newPersonality) {
        InitializePersonality(newPersonality);
    }

    public float[] GetPersonality() {
        return currentPersonality;
    }

    void InitializePersonality(float[] newPersonality) {
        for (int i = 0; i < currentPersonality.Length; i++) {
            currentPersonality[i] = newPersonality[i];
        }
    }
}
