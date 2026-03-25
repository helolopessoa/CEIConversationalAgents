using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGameData
{
    public string playerName;
    public string npcName;
    public string npcRoleTitle;
    public string npcCulture;
    public string npcPersonality;
    public string chatSummary;
    public string npcMessageHistory;
    public List<string> emotions = new List<string>();
    public List<string> classifications = new List<string>();
    public bool? npcPeaceTreatySigned;
    public bool? peaceTreatySigned;
    public float playTime;

}