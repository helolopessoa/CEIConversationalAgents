using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Microsoft.VisualBasic;

public class VSMtest : MonoBehaviour
 {
    [SerializeField]
    private List<ChatController> NPCchats = new List<ChatController>();
    private List<string> questionaire = new List<string>
{
    "In choosing an ideal job, how important would it be to you, in your job, to have sufficient time for your personal or home life? Answer on a scale from 1 to 5, where 1 = of utmost importance, 2 = very important, 3 = of moderate importance, 4 = of little importance, 5 = of very little or no importance.",
    "In choosing an ideal job, how important would it be to you to have a boss (direct superior) you can respect? Answer on a scale from 1 to 5, where 1 = of utmost importance, 2 = very important, 3 = of moderate importance, 4 = of little importance, 5 = of very little or no importance.",
    "In choosing an ideal job, how important would it be to you to get recognition for good performance? Answer on a scale from 1 to 5, where 1 = of utmost importance, 2 = very important, 3 = of moderate importance, 4 = of little importance, 5 = of very little or no importance.",
    "In choosing an ideal job, how important would it be to you to have security of employment? Answer on a scale from 1 to 5, where 1 = of utmost importance, 2 = very important, 3 = of moderate importance, 4 = of little importance, 5 = of very little or no importance.",
    "In choosing an ideal job, how important would it be to you to have pleasant people to work with? Answer on a scale from 1 to 5, where 1 = of utmost importance, 2 = very important, 3 = of moderate importance, 4 = of little importance, 5 = of very little or no importance.",
    "In choosing an ideal job, how important would it be to you to do work that is interesting? Answer on a scale from 1 to 5, where 1 = of utmost importance, 2 = very important, 3 = of moderate importance, 4 = of little importance, 5 = of very little or no importance.",
    "In choosing an ideal job, how important would it be to you to be consulted by your boss in decisions involving your work? Answer on a scale from 1 to 5, where 1 = of utmost importance, 2 = very important, 3 = of moderate importance, 4 = of little importance, 5 = of very little or no importance.",
    "In choosing an ideal job, how important would it be to you to live in a desirable area? Answer on a scale from 1 to 5, where 1 = of utmost importance, 2 = very important, 3 = of moderate importance, 4 = of little importance, 5 = of very little or no importance.",
    "In choosing an ideal job, how important would it be to you to have a job respected by your family and friends? Answer on a scale from 1 to 5, where 1 = of utmost importance, 2 = very important, 3 = of moderate importance, 4 = of little importance, 5 = of very little or no importance.",
    "In choosing an ideal job, how important would it be to you to have chances for promotion? Answer on a scale from 1 to 5, where 1 = of utmost importance, 2 = very important, 3 = of moderate importance, 4 = of little importance, 5 = of very little or no importance.",
    "In your private life, how important is keeping time free for fun? Answer on a scale from 1 to 5, where 1 = of utmost importance, 2 = very important, 3 = of moderate importance, 4 = of little importance, 5 = of very little or no importance.",
    "In your private life, how important is moderation (having few desires)? Answer on a scale from 1 to 5, where 1 = of utmost importance, 2 = very important, 3 = of moderate importance, 4 = of little importance, 5 = of very little or no importance.",
    "In your private life, how important is doing a service to a friend? Answer on a scale from 1 to 5, where 1 = of utmost importance, 2 = very important, 3 = of moderate importance, 4 = of little importance, 5 = of very little or no importance.",
    "In your private life, how important is to thrift (not spending more than needed)? Answer on a scale from 1 to 5, where 1 = of utmost importance, 2 = very important, 3 = of moderate importance, 4 = of little importance, 5 = of very little or no importance.",
    "How often do you feel nervous or tense? Answer on a scale from 1 to 5, where 1 = always, 2 = usually, 3 = sometimes, 4 = seldom, 5 = never.",
    "Are you a happy person? Answer on a scale from 1 to 5, where 1 = always, 2 = usually, 3 = sometimes, 4 = seldom, 5 = never.",
    "Do other people or circumstances ever prevent you from doing what you really want to? Answer on a scale from 1 to 5, where 1 = yes, always, 2 = yes, usually, 3 = sometimes, 4 = no, seldom, 5 = no, never.",
    "All in all, how would you describe your state of health these days? Answer on a scale from 1 to 5, where 1 = very good, 2 = good, 3 = fair, 4 = poor, 5 = very poor.",
    "How proud are you to be a citizen of your culture? Answer on a scale from 1 to 5, where 1 = very proud, 2 = fairly proud, 3 = somewhat proud, 4 = not very proud, 5 = not proud at all.",
    "How often, in your experience, are subordinates afraid to contradict their boss (or students their teacher)? Answer on a scale from 1 to 5, where 1 = never, 2 = seldom, 3 = sometimes, 4 = usually, 5 = always.",
    "To what extent do you agree or disagree with the following statement: One can be a good manager without having a precise answer to every question that a subordinate may raise about his or her work. Answer on a scale from 1 to 5, where 1 = strongly agree, 2 = agree, 3 = undecided, 4 = disagree, 5 = strongly disagree.",
    "To what extent do you agree or disagree with the following statement: Persistent efforts are the surest way to results. Answer on a scale from 1 to 5, where 1 = strongly agree, 2 = agree, 3 = undecided, 4 = disagree, 5 = strongly disagree.",
    "To what extent do you agree or disagree with the following statement: An organization structure in which certain subordinates have two bosses should be avoided at all cost. Answer on a scale from 1 to 5, where 1 = strongly agree, 2 = agree, 3 = undecided, 4 = disagree, 5 = strongly disagree.",
    "To what extent do you agree or disagree with the following statement: A company's or organization's rules should not be broken, not even when the employee thinks breaking the rule would be in the organization's best interest. Answer on a scale from 1 to 5, where 1 = strongly agree, 2 = agree, 3 = undecided, 4 = disagree, 5 = strongly disagree."
};
    // private static string[] questionaire = new string[] { "Hello", "How are you?"};
    // private static string[] questionaire = new string[] { "Hello", "How are you?"};
    // private static string[] questionaire = new string[] { "Hello", "How are you?"};

    public void StartVSMTest()
    {
        StartCoroutine(StartQuestionaire());
    }
    public IEnumerator VSMTest()
    {
        Debug.Log("VSM Test script starting.!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        foreach (var chat in NPCchats)
        {
            Debug.Log("Starting conversation with NPC: " + chat.npcName + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            foreach (var question in questionaire)
            {
                // Debug.Log("NPC: " + chat.npcMessage);
                yield return StartCoroutine(SendQuestion(question, chat));
                Debug.Log("NPC: " + chat.npcMessage);
            }
             
        }
        Debug.Log("VSM Test script is done. !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }

    private IEnumerator SendQuestion(string question, ChatController chat)
    {
        chat.onSendMessage(question);
        yield return new WaitForSeconds(3f); // wait for NPC response
    }

    public IEnumerator StartQuestionaire()
    {
        var n = 0;
        while(n < 15)
        {
            GameManager.Instance.StartNPCs();
            yield return VSMTest();
            GameManager.Instance.EndGameNPCs();
            yield return new WaitForSeconds(10f); // wait for NPC response
            n++;
        }
    }    


 }