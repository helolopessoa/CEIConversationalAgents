using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEmotions {

    public static Dictionary<string, string[]> GetDict()
    {
        return new Dictionary<string, string[]>() {

            {"is_attacking", new string[4] { "Rage", "Outrage", "Despair", "Terror" } },
            {"is_shooting", new string[4] { "Annoyance", "Pessimism", "Disapproval", "Apprehension" } },
            {"is_harming", new string[4] { "Anger", "Contempt", "Unbelief", "Fear" } },
            {"is_injured", new string[4] { "Anger", "Disapproval", "Pride", "Joy" } },

            {"is_giving_item", new string[4] { "Joy", "Optimism", "Hope", "Trust" } },
            {"is_stealing_item", new string[4] { "Sadness", "Shame", "Remorse", "Disgust" } },
            {"is_giving_money", new string[4] { "Admiration", "Love", "Sentimentality", "Ecstasy" } },
            {"is_stealing_money", new string[4] { "Grief", "Dominance", "Awe", "Loathing" } },

            {"is_social", new string[4] { "Distraction", "Anxiety", "Delight", "Interest" } },
            {"is_personal", new string[4] { "Anticipation", "Cynicism", "Curiosity", "Surprise" } },
            {"is_intimate", new string[4] { "Vigilance", "Aggressiveness", "Submission", "Amazement" } },

            {"is_talking_politely", new string[4] { "Boredom", "Envy", "Pride", "Serenity" } }, 
            {"is_not_talking_politely", new string[4] { "Pensiveness", "Guilt", "Morbidness", "Acceptance" } },
        };
    }

    public static Dictionary<string, string> GetCultureAttributes()
    {
        return new Dictionary<string, string>() {

            {"is_attacking", "dignity" },
            {"is_shooting", "collectivism" },
            {"is_harming", "collectivism" },
            {"is_injured", "trust_level" },

            {"is_giving_item", "wealth" },
            {"is_stealing_item", "wealth" },
            {"is_giving_money", "wealth" },
            {"is_stealing_money", "wealth" },

            {"is_social", "dignity" },
            {"is_personal", "dignity" },
            {"is_intimate", "dignity" },

            {"is_talking_politely", "politeness" },
            {"is_not_talking_politely", "politeness" },
        };
    }
}
