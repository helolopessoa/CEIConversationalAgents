using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGameData
{
    public string npcName;
    public string npcRoleTitle;
    public string chatSummary;
    public List<string> emotions = new List<string>();
    public List<string> classifications = new List<string>();
    public bool peaceTreatySigned;
    public float playTime;

}