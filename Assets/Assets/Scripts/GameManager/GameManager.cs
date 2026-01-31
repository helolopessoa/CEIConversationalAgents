using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private GameObject MainMenuUI;    
    [SerializeField]
    private GameObject EndMenuUI;    
    [SerializeField]
    private GameObject FormMenuUI;    
    // public SaveGameChat gameData;
    public bool saveGame = false;
    public void StartGame()
    {
        MainMenuUI.SetActive(false);
        EndMenuUI.SetActive(true);
    }
    public void RestartGame()
    {
        saveGame = false;
        FormMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }
    public void EndGame()
    {
        saveGame = true;
        FormMenuUI.SetActive(true);
        EndMenuUI.SetActive(false);
    }
    public void SaveGameData(SaveGameData data)
    {
        // Implement saving logic here (e.g., write to file, PlayerPrefs, etc.)
        // Debug.Log("Game data saved for NPC: " + data.npcName);
        SaveGameChat.SaveGameData(data);
    }
    
}