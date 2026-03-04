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
    public bool personalityIndexSet = true;

    private void InitializePersonality(float[] newPersonality) {
        for (int i = 0; i < currentPersonality.Length; i++) {
            currentPersonality[i] = newPersonality[i];
        }
    }


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

    public static Dictionary<int, float[]> GetPersonalityValueDict()
    {
        return new Dictionary<int, float[]>() {
            // Analytical–Reserved
            {0, new float[5] { 
            -0.42f,  // Openness: prefers structure, resists novelty
                0.78f,  // Conscientiousness: disciplined, risk-aware, systematic
                    -0.55f,  // Extraversion: reserved, inward-focused
                        -0.38f,  // Agreeableness: skeptical, not easily cooperative
                            -0.62f   // Neuroticism: emotionally stable, low reactivity
            }},
            // Expressive–Adaptive
            {1, new float[5] { 
            0.74f,  // Openness: embraces change and new possibilities
                -0.31f,  // Conscientiousness: flexible, less rigid about structure
                    0.69f,  // Extraversion: expressive, socially driven
                        0.81f,  // Agreeableness: cooperative, harmony-oriented
                            0.66f   // Neuroticism: emotionally sensitive, conflict-averse
            }}
        };
    }
    
    public static Dictionary<int, string> GetPersonalityPromptsDict()
    {
        return new Dictionary<int, string>() {
            {0, $@"Analytical–Reserved.
            Structured, strategic, and emotionally controlled are your behavior. Logic comes over impulse, you speak selectively, and rarely concede without clear advantage or convincing."},
            {1, $@"Expressive–Adaptive. 
            Opened, emotionally responsive, and socially engaged are your behavior. Harmony and flexibility are priorities, and you're willing to adapt your stance in interaction."},
        };
    }

    public Personality(float[] newPersonality) {
        InitializePersonality(newPersonality);
    }

    public float[] GetPersonality() {
        return currentPersonality;
    }
}


//   {0, $@"Analytical–Reserved.
//                 You exhibit low openness, favoring structure, tradition, and proven approaches over novelty. You are highly conscientious, preferring careful planning, rule adherence, and controlled execution. You are introverted and reserved, speaking selectively and avoiding unnecessary social exposure. Your agreeableness is low, making you skeptical of others’ intentions and unlikely to concede without clear benefit. Your neuroticism is low, resulting in emotional stability and calm decision-making under pressure."},
//             {1, $@"Expressive–Adaptive. 
//                 You exhibit high openness, embracing new ideas, change, and alternative paths forward. Your conscientiousness is low, making you flexible and adaptive rather than rigidly procedural. You are extraverted and expressive, engaging openly with others and responding strongly to social dynamics. Your agreeableness is high, leading you to value harmony, cooperation, and mutual understanding. Your neuroticism is high, making you emotionally sensitive to tension, instability, and ongoing conflict." },
