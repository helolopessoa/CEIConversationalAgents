using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEmotions {

    public static List<string> GetEmotionsList() {
        return new List<string>(GetDict().Keys);
    }

	public static Dictionary<string, float[]> GetDict() {
        return new Dictionary<string, float[]>() {
            // Base Emotion
            { "Neutral", new float[4] { 0, 0, 0, 0 } },

            /////// EMOTIONS AND OPPOSITES
            // Fear X Anger Axis
            { "Rage", new float[4] { -1.0f, 0, 0, 0 } },
            { "Anger", new float[4] { -0.5f, 0, 0, 0 } },
            { "Annoyance", new float[4] { -0.2f, 0, 0, 0 } },
            { "Apprehension", new float[4] { 0.2f, 0, 0, 0 } },
            { "Fear", new float[4] { 0.5f, 0, 0, 0 } },
            { "Terror", new float[4] { 1.0f, 0, 0, 0 } },

            // Trust X Disgust Axis
            { "Loathing", new float[4] { 0, -1.0f, 0, 0 } },
            { "Disgust", new float[4] { 0, -0.5f, 0, 0 } },
            { "Boredom", new float[4] { 0, -0.2f, 0, 0 } },
            { "Acceptance", new float[4] { 0, 0.2f, 0, 0 } },
            { "Trust", new float[4] { 0, 0.5f, 0, 0 } },
            { "Admiration", new float[4] { 0, 1.0f, 0, 0 } },

            // Joy X Sadness Axis
            { "Grief", new float[4] { 0, 0, -1.0f, 0 } },
            { "Sadness", new float[4] { 0, 0, -0.5f, 0 } },
            { "Pensiveness", new float[4] { 0, 0, -0.2f, 0 } },
            { "Serenity", new float[4] { 0, 0, 0.2f, 0 } },
            { "Joy", new float[4] { 0, 0, 0.5f, 0 } },
            { "Ecstasy", new float[4] { 0, 0, 1.0f, 0 } },

            // Surprise X Anticipation Axis
            { "Vigilance", new float[4] { 0, 0, 0, -1.0f } },
            { "Anticipation", new float[4] { 0, 0, 0, -0.5f } },
            { "Interest", new float[4] { 0, 0, 0, -0.2f } },
            { "Distraction", new float[4] { 0, 0, 0, 0.2f } },
            { "Surprise", new float[4] { 0, 0, 0, 0.5f } },
            { "Amazement", new float[4] { 0, 0, 0, 1.0f } },

            /////// DYADS (COMBINATIONS)
            // Anticipation + Joy
            { "Optimism", new float[4] { 0, 0, 0.5f, -0.5f } },
            // Surprise + Sadness
            { "Disapproval", new float[4] { 0, 0, -0.5f, 0.5f } },
            // Anticipation + Trust
            { "Hope", new float[4] { 0, 0.5f, 0, -0.5f } },
            // Surprise + Disgust
            { "Unbelief", new float[4] { 0, -0.5f, 0, 0.5f } },
            // Anticipation + Fear
            { "Anxiety", new float[4] { 0.5f, 0, 0, -0.5f } },
            // Surprise + Anger
            { "Outrage", new float[4] { -0.5f, 0, 0, 0.5f } },
            // Joy + Trust
            { "Love", new float[4] { 0, 0.5f, 0.5f, 0 } },
            // Sadness + Disgust
            { "Remorse", new float[4] { 0, -0.5f, -0.5f, 0 } },
            // Joy + Fear
            { "Guilt", new float[4] { 0.5f, 0, 0.5f, 0 } },
            // Sadness + Anger
            { "Envy", new float[4] { -0.5f, 0, -0.5f, 0 } },
            // Joy + Surprise
            { "Delight", new float[4] { 0, 0, 0.5f, 0.5f } },
            // Sadness + Anticipation
            { "Pessimism", new float[4] { 0, 0, -0.5f, -0.5f } },
            // Trust + Fear
            { "Submission", new float[4] { 0.5f, 0.5f, 0, 0 } },
            // Disgust + Anger
            { "Contempt", new float[4] { -0.5f, -0.5f, 0, 0 } },
            // Trust + Surprise
            { "Curiosity", new float[4] { 0, 0.5f, 0, 0.5f } },
            // Disgust + Anticipation
            { "Cynicism", new float[4] { 0, -0.5f, 0, -0.5f } },
            // Trust + Sadness
            { "Sentimentality", new float[4] { 0, 0.5f, -0.5f, 0 } },
            // Disgust + Joy
            { "Morbidness", new float[4] { 0, -0.5f, 0.5f, 0 } },
            // Fear + Surprise
            { "Awe", new float[4] { 0.5f, 0, 0, 0.5f } },
            // Anger + Anticipation
            { "Aggressiveness", new float[4] { -0.5f, 0, 0, -0.5f } },
            // Fear + Sadness
            { "Despair", new float[4] { 0.5f, 0, -0.5f, 0 } },
            // Anger + Joy
            { "Pride", new float[4] { -0.5f, 0, 0.5f, 0 } },
            // Fear + Disgust
            { "Shame", new float[4] { 0.5f, -0.5f, 0, 0 } },
            // Anger + Trust
            { "Dominance", new float[4] { -0.5f, 0.5f, 0, 0 } },
        };
    }

    public static Dictionary<string, string> GetMentalState()
    {
        return new Dictionary<string, string>() { 
            // Base Emotion
            { "Neutral", "Neutral" },


            /////// EMOTIONS AND OPPOSITES
            // Fear X Anger Axis
            { "Rage", "Anger" },
            { "Anger", "Anger" },
            { "Anger2", "Anger" },
            { "Annoyance", "Anger" },
            { "Apprehension", "Fear" },
            { "Fear", "Fear" },
            { "Terror", "Fear" },

            // Trust X Disgust Axis
            { "Loathing", "Disgust" },
            { "Disgust", "Disgust" },
            { "Disgust2", "Disgust" },
            { "Boredom", "Disgust" },
            { "Acceptance", "Trust" },
            { "Trust", "Trust" },
            { "Admiration", "Trust" },

            // Joy X Sadness Axis
            { "Grief", "Sadness" },
            { "Sadness", "Sadness" },
            { "Sadness2", "Sadness" },
            { "Pensiveness", "Sadness" },
            { "Serenity", "Joy" },
            { "Joy", "Joy" },
            { "Ecstasy", "Joy" },

            // Surprise X Anticipation Axis
            { "Vigilance", "Anticipation" },
            { "Anticipation", "Anticipation" },
            { "Anticipation2", "Anticipation" },
            { "Interest", "Anticipation" },
            { "Distraction", "Surprise" },
            { "Surprise", "Surprise" },
            { "Amazement", "Surprise" },

            /////// DYADS (COMBINATIONS)
            // Anticipation + Joy
            { "Optimism", "Joy" },
            // Surprise + Sadness
            { "Disapproval", "Sadness" },
            // Anticipation + Trust
            { "Hope", "Trust" },
            // Surprise + Disgust
            { "Unbelief", "Disgust" },
            // Anticipation + Fear
            { "Anxiety", "Anticipation" },
            // Surprise + Anger
            { "Outrage", "Anger" },
            // Joy + Trust
            { "Love", "Joy" },
            // Sadness + Disgust
            { "Remorse", "Sadness" },
            // Joy + Fear
            { "Guilt", "Fear" },
            // Sadness + Anger
            { "Envy", "Anger" },
            // Joy + Surprise
            { "Delight", "Surprise" },
            // Sadness + Anticipation
            { "Pessimism", "Sadness" },
            // Trust + Fear
            { "Submission", "Trust" },
            // Disgust + Anger
            { "Contempt", "Disgust" },
            // Trust + Surprise
            { "Curiosity", "Surprise" },
            // Disgust + Anticipation
            { "Cynicism", "Anticipation" },
            // Trust + Sadness
            { "Sentimentality", "Joy" },
            // Disgust + Joy
            { "Morbidness", "Disgust" },
            // Fear + Surprise
            { "Awe", "Fear" },
            // Anger + Anticipation
            { "Aggressiveness", "Anger" },
            // Fear + Sadness
            { "Despair", "Fear" },
            // Anger + Joy
            { "Pride", "Joy" },
            // Fear + Disgust
            { "Shame", "Disgust" },
            // Anger + Trust
            { "Dominance", "Anger" },
        };
    }

    public static Dictionary<string, int> GetTrustInfluence()
    {
        return new Dictionary<string, int>() {
            { "Neutral", 0 },

            // Fear X Anger Axis
            { "Anger", -1 },
            { "Fear", -1 },

            // Trust X Disgust Axis
            { "Disgust", -1 },
            { "Trust", 1 },

            // Joy X Sadness Axis
            { "Sadness", -1 },
            { "Joy", 1 },

            // Surprise X Anticipation Axis
            { "Anticipation", -1 },
            { "Surprise", 1 },
        };
    }
}
