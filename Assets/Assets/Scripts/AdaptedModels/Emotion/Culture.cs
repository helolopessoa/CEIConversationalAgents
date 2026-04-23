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
    public int cultureIndex;
    // public string cultureString;
    public static int rangerIndex = 2;
    public static int downsideIndex = 4;

    public static  string rangerDescription = 
    $@"Time orientation is high (0.72), strong focus on the future;
    Wealth orientation is moderately low (0.46), economic gain not that valuable ; 
    Dignity is very low (0.02), personal honor is not a priority; 
    Politeness is high (0.73), you speak polite and respectful;
    Rationality is high (0.76), logic mostly overcomes emotion; 
    Collectivism is very high (1.00), collective advantage prioritize";
    // $@"Your level of time orientation is high (0.72), you consider future outcomes and long-term planning important when making decisions.;
    // Your level of wealth orientation is moderately low (0.46), you recognize the importance of resources and economic stability, but you are willing to sacrifice finances in favor of other priorities.; 
    // Your level of dignity is very low (0.02), you place little importance on personal honor, reputation, or moral self-worth when interacting with others.; 
    // Your level of politeness is high (0.73), you generally communicate with courtesy, respect social rules, and avoid unnecessary offense.;
    // Your level of rationality is high (0.76), you rely on reason and structured thinking, while still allowing room for judgment beyond pure calculation.; 
    // Your level of collectivism is very high (1.00), you prioritize group welfare, shared identity, and collective responsibility over individual gain.";

    public static  string downsideDescription = 
    $@"Time orientation is very low (0.05), strong present focus;
    Wealth orientation is very high (1.00), economic gain prioritized; 
    Dignity is very high (0.88), strong honor values;
    Politeness is low (0.15), you speak direct and blunt, with street slangs; 
    Rationality is very high (0.93), logic-driven decisions;
    Collectivism is low (0.22), individual advantge prioritized";
    // $@"Your level of time orientation is very low (0.05), you strongly focus on the immediate present, rarely planning far ahead or valuing long-term consequences.;
    // Your level of wealth orientation is very high (1.00), material resources, possessions, and economic gain are central to your worldview and decision-making.; 
    // Your level of dignity is very high (0.88), you strongly value personal honor, integrity, and being worthy of respect.;
    // Your level of politeness is low (0.15), you tend to communicate bluntly and directly, with minimal concern for social niceties or etiquette.; 
    // Your level of rationality is very high (0.93), you prioritize logical reasoning, efficiency, and cost–benefit analysis over emotion or tradition.;
    // Your level of collectivism is low (0.22), you favor individual goals and personal advantage over group cohesion or shared responsibility..";

    public static string rangerPOVDescription = 
    $@"The Downsiders:
    - Time Orientation: Strong present focus
    - Wealth Orientation: Very high priority on economic gain
    - Dignity: High emphasis on personal honor
    - Politeness: Low, blunt and direct communication
    - Rationality: Strong cost–benefit calculation
    - Collectivism: Low, individual advantage prioritized
    They tend to approach negotiation transactionally, seeking immediate advantage and clear personal benefit.";
    // $@"They prioritize immediate gain over long-term stability. 
    // Economic advantage drives their decisions, often outweighing moral or relational considerations. 
    // They value personal honor but show little concern for courtesy or diplomacy. 
    // Their communication is blunt and transactional. 
    // They rely on rational cost–benefit analysis and prioritize individual advantage over collective welfare.";

    public static string downsidePOVDescription = 
    $@"The Rangers:
    - Time Orientation: Long-term focus
    - Wealth Orientation: Moderate priority
    - Dignity: Low emphasis on personal honor
    - Politeness: High, diplomatic communication
    - Rationality: Structured but socially balanced
    - Collectivism: High, group-oriented
    They tend to approach negotiation collaboratively and strategically.";
    // $@"The Rangers:
    // They prioritize long-term stability over immediate gain. 
    // Collective welfare guides their decisions, even at economic cost. 
    // Personal honor holds little weight compared to group cohesion. 
    // They communicate with courtesy and restraint. 
    // Their reasoning is structured but tempered by social judgment rather than strict calculation.";
            

    public static Dictionary<string, float[]> GetCulturesValueDict()
    {
        return new Dictionary<string, float[]>() {
            { "Ogre", new float[6] { 0.15f, 0.63f, 0.31f, 0.02f, 0.15f, 0.93f } },
            { "Traveller", new float[6] { 0.28f, 0.25f, 0.82f, 0.75f, 0.93f, 0.43f} },
            { "Ranger", new float[6] { 0.72f, 0.56f, 0.88f, 0.73f, 0.76f, 1f } },
            { "Adventurer", new float[6] { 0.83f, 0.24f, 0.65f, 0.58f, 0.25f, 0.77f} },
            { "Downside", new float[6] { 0.05f, 1f, 0.02f, 0.15f, 0.39f, 0.22f} },
        };
    }

    public static string[] Cultures = new string[] { "Ogre", "Traveller", "Ranger", "Adventurer", "Downside" };

    public static Dictionary<string, string> GetCulturePromptsDict()
    {
        // VisionFromDownsider -> A Downsider's POV of Ranger culture, and vice-versa
        return new Dictionary<string, string>() {
            // { "Ranger", $@"Your level of time orientation is high (0.72), you consider future outcomes and long-term planning important when making decisions.; Your level of wealth orientation is moderately high (0.56), you value resources and economic stability, but not at the expense of other principles.; Your level of dignity is very high (0.88), you strongly value personal honor, integrity, and being worthy of respect.; Your level of politeness is high (0.73), you generally communicate with courtesy, respect social rules, and avoid unnecessary offense.; Your level of rationality is high (0.76), you rely on reason and structured thinking, while still allowing room for judgment beyond pure calculation.; Your level of collectivism is very high (1.00), you prioritize group welfare, shared identity, and collective responsibility over individual gain."},
            { "Ranger", rangerDescription},
            // { "Downside",$@"Your level of time orientation is very low (0.05), you strongly focus on the immediate present, rarely planning far ahead or valuing long-term consequences.;Your level of wealth orientation is very high (1.00), material resources, possessions, and economic gain are central to your worldview and decision-making.; Your level of dignity is very low (0.02), you place little importance on personal honor, reputation, or moral self-worth when interacting with others.;Your level of politeness is low (0.15), you tend to communicate bluntly and directly, with minimal concern for social niceties or etiquette.; Your level of rationality is very high (0.93), you prioritize logical reasoning, efficiency, and cost–benefit analysis over emotion or tradition.;Your level of collectivism is low (0.22),  you favor individual goals and personal advantage over group cohesion or shared responsibility.."},
            { "Downside", downsideDescription},
            // { "VisionFromDownsider", $@"They consider future outcomes and long-term planning important when making decisions.; They value resources and economic stability, but not at the expense of other principles.; They strongly value personal honor, integrity, and being worthy of respect.; They generally communicate with courtesy, respect social rules, and avoid unnecessary offense.; They rely on reason and structured thinking, while still allowing room for judgment beyond pure calculation.; They prioritize group welfare, shared identity, and collective responsibility over individual gain."},
            { "VisionFromDownside", downsidePOVDescription},
            // { "VisionFromRanger", $@"They strongly focus on the immediate present, rarely planning far ahead or valuing long-term consequences.; Material resources, possessions, and economic gain are central to their worldview and decision-making.; They place little importance on personal honor, reputation, or moral self-worth when interacting with others.; They tend to communicate bluntly and directly, with minimal concern for social niceties or etiquette.; They prioritize logical reasoning, efficiency, and cost–benefit analysis over emotion or tradition.; They favor individual goals and personal advantage over group cohesion or shared responsibility."},
            { "VisionFromRanger", rangerPOVDescription},
        };
    }

    public static Dictionary<string, string> GetFellowsDescriptionDict()
    {
        // VisionFromDownsider -> A Downsider's POV of Ranger culture, and vice-versa
        return new Dictionary<string, string>() {
            { "Ranger", "Roxanne is the leader of the Rangers, that are in war with the Downsides. Yasmin is the woman civillian in favor of the peace treaty to be signed, and the war to end. Ursula is the other Ranger, she's against the peace"},
            { "Downside", "Alice is the leader of the Downsides, that are in war with the Rangers. Lorelai is the woman civillian in favor of the peace treaty to be signed, and the war to end. Tiana is the other Downside, she's against the peace"},
        };
    }


    private void InitializeCulture(float[] newCulture) {
        for (int i = 0; i < currentCulture.Length; i++)
        {
            currentCulture[i] = newCulture[i];
        }
    }


    public Culture(float[] newCulture) {
        time = newCulture[0];
        wealth = newCulture[1];
        dignity = newCulture[2];
        politeness = newCulture[3];
        rationality = newCulture[4];
        collectivism = newCulture[5];
        InitializeCulture(newCulture);
    }

    public void LoadCultureDict(Dictionary<string, float> dict) {
        dict["time"] = time;
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

    public float GetValueThroughKey(string key)
    {
        return key switch
        {
            "time" => time,
            "wealth" => wealth,
            "dignity" => dignity,
            "politeness" => politeness,
            "rationality" => rationality,
            "collectivism" => collectivism,
            _ => 1f
        };
    }

    // public static string GetPromptDescription(int cultureIndex, bool vision = false) {
    //     if (vision)
    //     {
    //         if (cultureIndex == rangerIndex) {
    //             return GetDownsiderOutsiderVisionPromptDescription();
    //         } else if (cultureIndex == downsiderIndex) {
    //             return GetRangerOutsiderVisionPromptDescription();
    //         } else {
    //             return "";
    //         }
            
    //     }
    //     if (cultureIndex == rangerIndex) {
    //         return GetRangerPromptDescription();
    //     } else if (cultureIndex == downsiderIndex) {
    //         return GetDownsiderPromptDescription();
    //     } else {
    //         return "";
    //     }
    // }

//This needs reviewing 
    // public static string GetRangerPromptDescription() {
    //     return $@"
    //     Your level of time orientation is high (0.72), you consider future outcomes and long-term planning important when making decisions.; 
    //     Your level of wealth orientation is moderately high (0.56), you value resources and economic stability, but not at the expense of other principles.; 
    //     Your level of dignity is very high (0.88), you strongly value personal honor, integrity, and being worthy of respect.; 
    //     Your level of politeness is high (0.73), you generally communicate with courtesy, respect social rules, and avoid unnecessary offense.; 
    //     Your level of rationality is high (0.76), you rely on reason and structured thinking, while still allowing room for judgment beyond pure calculation.;
    //     Your level of collectivism is very high (1.00), you prioritize group welfare, shared identity, and collective responsibility over individual gain.";
    // }

    // public static string GetDownsiderPromptDescription() {
    //     return $@"
    //     Your level of time orientation is very low (0.05), you strongly focus on the immediate present, rarely planning far ahead or valuing long-term consequences.;
    //     Your level of wealth orientation is very high (1.00), material resources, possessions, and economic gain are central to your worldview and decision-making.; 
    //     Your level of dignity is very low (0.02), you place little importance on personal honor, reputation, or moral self-worth when interacting with others.; 
    //     Your level of politeness is low (0.15), you tend to communicate bluntly and directly, with minimal concern for social niceties or etiquette.; 
    //     Your level of rationality is very high (0.93), you prioritize logical reasoning, efficiency, and cost–benefit analysis over emotion or tradition.; 
    //     Your level of collectivism is low (0.22),  you favor individual goals and personal advantage over group cohesion or shared responsibility..";
    // }

    // public static string GetRangerOutsiderVisionPromptDescription() {
    //     return $@"
    //     They consider future outcomes and long-term planning important when making decisions.; 
    //     They value resources and economic stability, but not at the expense of other principles.; 
    //     They strongly value personal honor, integrity, and being worthy of respect.; 
    //     They generally communicate with courtesy, respect social rules, and avoid unnecessary offense.; 
    //     They rely on reason and structured thinking, while still allowing room for judgment beyond pure calculation.;
    //     They prioritize group welfare, shared identity, and collective responsibility over individual gain.";
    // }
    
    // public static string GetDownsiderOutsiderVisionPromptDescription() {
    //     return $@"
    //     They strongly focus on the immediate present, rarely planning far ahead or valuing long-term consequences.;
    //     Material resources, possessions, and economic gain are central to their worldview and decision-making.; 
    //     They place little importance on personal honor, reputation, or moral self-worth when interacting with others.; 
    //     They tend to communicate bluntly and directly, with minimal concern for social niceties or etiquette.; 
    //     They prioritize logical reasoning, efficiency, and cost–benefit analysis over emotion or tradition.; 
    //     They favor individual goals and personal advantage over group cohesion or shared responsibility.";
    // }

}
